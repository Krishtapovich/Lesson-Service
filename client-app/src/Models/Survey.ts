import { QuestionCreateModel, QuestionModel } from "./Question";

export interface SurveyListModel {
  id: string;
  isClosed: boolean;
  creationTime: Date;
}

export interface SurveyCreateModel {
  id: string;
  creationTime: string;
  questions: Array<QuestionCreateModel>;
}

export interface SurveySendingModel {
  id: string;
  groups: Array<string>;
  openPeriod?: number;
}
