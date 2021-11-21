import { OptionCreateModel, OptionModel } from "./Option";

export interface QuestionModel {
  id: number;
  text: string;
  options?: Array<OptionModel>;
}

export interface QuestionCreateModel {
  text: string;
  options?: Array<OptionCreateModel>;
}
