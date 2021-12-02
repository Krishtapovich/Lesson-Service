export interface OptionVisualizationModel {
  optionText: string;
  answersAmount: number;
  isCorrect: boolean;
}

export interface AnswerVisualizationModel {
  questionText: string;
  options: Array<OptionVisualizationModel>;
}
