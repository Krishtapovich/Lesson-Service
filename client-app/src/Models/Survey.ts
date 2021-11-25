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

export interface SurveyToGroups {
  id: string;
  groups: Array<string>;
  openPeriod?: number;
}
