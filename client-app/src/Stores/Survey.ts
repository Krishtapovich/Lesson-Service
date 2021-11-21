import AnswerModel from "@Models/Answer";
import { SurveyCreateModel, SurveyModel, SurveyToGroupModel } from "@Models/Survey";
import surveyService from "@Services/Survey";
import { makeAutoObservable, runInAction } from "mobx";
import uuid from "uuid";

export default class SurveyStore {
  surveys: Array<SurveyModel> = [];
  studentAnswers: Array<AnswerModel> = [];

  constructor() {
    makeAutoObservable(this);
  }

  async init() {
    const surveys = await surveyService.getSurveys(1, 1);
    runInAction(() => {
      this.surveys = surveys;
    });
  }

  dispose() {
    this.surveys = [];
    this.studentAnswers = [];
  }

  async getSurveys(pageNumber: number, pageSize: number) {
    const surveys = await surveyService.getSurveys(pageNumber, pageSize);
    runInAction(() => {
      this.surveys.concat(surveys);
    });
  }

  async getStudentAnswers(surveyId: string, studentId: number) {
    const answers = await surveyService.getStudentAnswers(surveyId, studentId);
    runInAction(() => {
      this.studentAnswers = answers;
    });
  }

  async addSurvey(survey: SurveyCreateModel) {
    survey.id = uuid.v4.toString();
    const newSurvey = await surveyService.createSurvey(survey);
    runInAction(() => {
      this.surveys.push(newSurvey);
    });
  }

  sendSurvey(survey: SurveyToGroupModel) {
    surveyService.sendSurveyToGroup(survey);
  }

  closeSruvey(surveyId: string) {
    this.surveys = this.surveys.map((s) => (s.id === surveyId ? { ...s, isClosed: true } : s));
    surveyService.closeSurvey(surveyId);
  }

  deleteSurvey(surveyId: string) {
    this.surveys = this.surveys.filter((s) => s.id !== surveyId);
    surveyService.deleteSurvey(surveyId);
  }
}
