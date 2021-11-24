import { QuestionCreateModel, QuestionModel } from "./Question";

export interface SurveyModel {
  id: string;
  isClosed: boolean;
  creationTime: Date;
  questions: Array<QuestionModel>;
}

export interface SurveyCreateModel {
  id: string;
  creationTime: string;
  questions: Array<QuestionCreateModel>;
}

export interface SurveyToGroupModel {
  id: string;
  groupNumber: number;
  openPeriod?: number;
}
