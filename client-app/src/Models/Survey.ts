import { QuestionCreateModel } from "./Question";

export interface SurveyListModel {
  id: string;
  title: string;
  isClosed: boolean;
}

export interface SurveyFormModel {
  title: string;
  questions: Array<QuestionCreateModel>;
}

export interface SurveyCreateModel {
  id: string;
  title: string;
  questions: Array<QuestionCreateModel>;
}

export interface SurveySendingModel {
  id: string;
  groups: Array<string>;
  openPeriod?: number;
}
