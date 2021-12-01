import AnswerModel from "@Models/Answer";
import { QuestionModel } from "@Models/Question";
import StudentModel from "@Models/Student";
import { SurveyCreateModel, SurveyFormModel, SurveyListModel, SurveySendingModel } from "@Models/Survey";
import { AnswerVisualizationModel } from "@Models/Visualization";
import surveyService from "@Services/Survey";
import { LOAD_TIME } from "@Utils/Theme";
import { makeAutoObservable, runInAction } from "mobx";
import uuid from "uuid";

export default class SurveyStore {
  surveys: Array<SurveyListModel> = [];
  surveyQuestions: Array<QuestionModel> = [];
  surveyStudents: Array<StudentModel> = [];
  answers: Array<AnswerModel> = [];
  currentSurvey?: SurveyListModel;
  visualization: Array<AnswerVisualizationModel> = [];

  isLoading = false;
  isQuestionsLoading = false;
  isAllAnswersLoading = false;
  isStudentAnswersLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  disposePreview() {
    this.surveyQuestions = [];
  }

  disposeResults() {
    this.answers = [];
    this.surveyStudents = [];
    this.surveyQuestions = [];
    this.visualization = [];
  }

  getSurveys() {
    if (!this.surveys.length) {
      this.isLoading = true;
      setTimeout(async () => {
        const surveys = await surveyService.getSurveys();
        runInAction(() => {
          this.surveys = surveys;
          this.isLoading = false;
        });
      }, LOAD_TIME);
    }
  }

  setSurvey(surveyId: string) {
    this.currentSurvey = this.surveys.find((s) => s.id === surveyId);
  }

  getSurveyQuestions(surveyId: string) {
    this.isQuestionsLoading = true;
    setTimeout(async () => {
      const questions = await surveyService.getSurveyQuestions(surveyId);
      runInAction(() => {
        this.surveyQuestions = questions;
        this.isQuestionsLoading = false;
      });
    }, LOAD_TIME);
  }

  getSurveyStudents(surveyId: string) {
    this.isLoading = true;
    setTimeout(async () => {
      const students = await surveyService.getSurveyStudents(surveyId);
      runInAction(() => {
        this.surveyStudents = students;
        this.isLoading = false;
      });
    }, LOAD_TIME);
  }

  getSurveyAnswers(surveyId: string) {
    this.isAllAnswersLoading = true;
    setTimeout(async () => {
      const answers = await surveyService.getSurveyAnswers(surveyId);
      const visualization = await surveyService.getSurveyAnswersVisualization(surveyId);
      runInAction(() => {
        this.answers = answers;
        this.visualization = visualization;
        this.isAllAnswersLoading = false;
      });
    }, LOAD_TIME);
  }

  getStudentAnswers(surveyId: string, studentId: number) {
    this.isStudentAnswersLoading = true;
    setTimeout(async () => {
      const answers = await surveyService.getStudentAnswers(surveyId, studentId);
      runInAction(() => {
        this.answers = answers;
        this.isStudentAnswersLoading = false;
      });
    }, LOAD_TIME);
  }

  async addSurvey(formSurvey: SurveyFormModel) {
    const survey: SurveyCreateModel = {
      id: uuid.v4().toString(),
      ...formSurvey
    };
    const newSurvey = await surveyService.createSurvey(survey);
    runInAction(() => (this.surveys = [newSurvey, ...this.surveys]));
  }

  sendSurvey(survey: SurveySendingModel) {
    this.surveys = this.surveys.map((s) => (s.id === survey.id ? { ...s, isClosed: false } : s));
    this.currentSurvey!.isClosed = false;
    surveyService.sendSurveyToGroups(survey);
  }

  closeSruvey(surveyId: string) {
    this.surveys = this.surveys.map((s) => (s.id === surveyId ? { ...s, isClosed: true } : s));
    if (this.currentSurvey) this.currentSurvey.isClosed = true;
    surveyService.closeSurvey(surveyId);
  }

  deleteSurvey(surveyId: string) {
    this.surveys = this.surveys.filter((s) => s.id !== surveyId);
    surveyService.deleteSurvey(surveyId);
    this.surveyQuestions = [];
  }
}
