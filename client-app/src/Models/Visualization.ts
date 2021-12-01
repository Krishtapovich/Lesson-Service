export interface OptionVisualizationModel {
  optionText: string;
  answersAmount: number;
  iscorrect: boolean;
}

export interface AnswerVisualizationModel {
  questionText: string;
  options: Array<OptionVisualizationModel>;
}
