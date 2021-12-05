import { AnswerModel, AnswerCsvModel } from "@Models/Answer";
import { QuestionModel } from "@Models/Question";
import StudentModel from "@Models/Student";
import { SurveyCreateModel, SurveyListModel, SurveySendingModel } from "@Models/Survey";
import { AnswerVisualizationModel } from "@Models/Visualization";

import BaseService from "./Base";

class SurveyService extends BaseService {
  getSurveys() {
    return this.requests.get<Array<SurveyListModel>>("survey/surveys-list");
  }

  getSurveyQuestions(surveyId: string) {
    return this.requests.get<Array<QuestionModel>>("survey/survey-questions", { surveyId });
  }

  getSurveyStudents(surveyId: string) {
    return this.requests.get<Array<StudentModel>>("survey/survey-students", { surveyId });
  }

  getSurveyAnswers(surveyId: string) {
    return this.requests.get<Array<AnswerModel>>("survey/survey-answers", { surveyId });
  }

  getSurveyAnswersVisualization(surveyId: string) {
    return this.requests.get<Array<AnswerVisualizationModel>>("survey/survey-visualization", {
      surveyId
    });
  }

  getSurveyCsvAnswers(surveyId: string) {
    return this.requests.get<Array<AnswerCsvModel>>("survey/survey-csv-answers", { surveyId });
  }

  getStudentAnswers(surveyId: string, studentId: number) {
    return this.requests.get<Array<AnswerModel>>("survey/student-answers", {
      surveyId,
      studentId
    });
  }

  createSurvey(survey: SurveyCreateModel) {
    return this.requests.post<SurveyListModel>("survey/create-survey", survey);
  }

  sendSurveyToGroups(survey: SurveySendingModel) {
    this.requests.post<void>("survey/send-survey", survey);
  }

  closeSurvey(surveyId: string) {
    this.requests.put<void>("survey/close-survey", {}, { surveyId });
  }

  deleteSurvey(surveyId: string) {
    this.requests.delete<void>("survey/delete-survey", { surveyId });
  }
}

const surveyService = new SurveyService();

export default surveyService;
