import { QuestionCreateModel, QuestionModel } from "./Question";

export interface SurveyModel {
  id: string;
  isClosed: boolean;
  questions: Array<QuestionModel>;
}

export interface SurveyCreateModel {
  id: string;
  questions: Array<QuestionCreateModel>;
}

export interface SurveyToGroupModel {
  id: string;
  groupNumber: number;
  openPeriod?: number;
}
