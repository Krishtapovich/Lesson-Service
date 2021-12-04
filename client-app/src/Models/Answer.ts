import { QuestionModel } from "./Question";

export default interface AnswerModel {
  text?: string;
  imageUrl?: string;
  optionId?: number;
  question: QuestionModel;
}
