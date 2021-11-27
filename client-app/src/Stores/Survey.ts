import AnswerModel from "@Models/Answer";
import { QuestionModel } from "@Models/Question";
import { SurveyCreateModel, SurveyListModel, SurveySendingModel } from "@Models/Survey";
import surveyService from "@Services/Survey";
import { makeAutoObservable, runInAction } from "mobx";
import uuid from "uuid";

export default class SurveyStore {
  surveys: Array<SurveyListModel> = [];
  surveyQuestions: Array<QuestionModel> = [];
  studentAnswers: Array<AnswerModel> = [];

  constructor() {
    makeAutoObservable(this);
  }

  async init() {
    const surveys = await surveyService.getSurveys(1, 20);
    runInAction(() => (this.surveys = surveys));
  }

  dispose() {
    this.surveys = [];
    this.studentAnswers = [];
    this.surveyQuestions = [];
  }

  async getSurveys(pageNumber: number, pageSize: number) {
    const surveys = await surveyService.getSurveys(pageNumber, pageSize);
    runInAction(() => this.surveys.concat(surveys));
  }

  async getSurveyQuestions(surveyId: string) {
    const questions = await surveyService.getSurveyQuestions(surveyId);
    console.log(questions);
    runInAction(() => (this.surveyQuestions = questions));
  }

  async getStudentAnswers(surveyId: string, studentId: number) {
    const answers = await surveyService.getStudentAnswers(surveyId, studentId);
    runInAction(() => (this.studentAnswers = answers));
  }

  async addSurvey(survey: SurveyCreateModel) {
    survey.id = uuid.v4.toString();
    survey.creationTime = new Date().toISOString();
    const newSurvey = await surveyService.createSurvey(survey);
    runInAction(() => this.surveys.push(newSurvey));
  }

  sendSurvey(survey: SurveySendingModel) {
    this.surveys = this.surveys.map((s) => (s.id === survey.id ? { ...s, isClosed: false } : s));
    surveyService.sendSurveyToGroups(survey);
  }

  closeSruvey(surveyId: string) {
    this.surveys = this.surveys.map((s) => (s.id === surveyId ? { ...s, isClosed: true } : s));
    surveyService.closeSurvey(surveyId);
  }

  deleteSurvey(surveyId: string) {
    this.surveys = this.surveys.filter((s) => s.id !== surveyId);
    surveyService.deleteSurvey(surveyId);
    this.surveyQuestions = [];
  }
}
