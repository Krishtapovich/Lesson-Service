import { OptionModel } from "./Option";
import { QuestionModel } from "./Question";

export interface AnswerModel {
  text?: string;
  imageUrl?: string;
  option?: OptionModel;
  question: QuestionModel;
}

export interface AnswerCsvModel {
  groupNumber: string;
  firstName: string;
  lastName: string;
  questionText: string;
  answerText: string;
}

export interface StudentCsvAnswerModel {
  questionText: string;
  answerText: string;
  isCorrect: boolean;
}
