import AnswerModel from "@Models/Answer";
import { QuestionModel } from "@Models/Question";
import { SurveyCreateModel, SurveyListModel, SurveySendingModel } from "@Models/Survey";

import BaseService from "./Base";

class SurveyService extends BaseService {
  getSurveys(pageNumber: number, pageSize: number) {
    return this.requests.get<Array<SurveyListModel>>("survey/surveys-list", {
      pageNumber,
      pageSize
    });
  }

  getSurveyQuestions(surveyId: string) {
    return this.requests.get<Array<QuestionModel>>("survey/survey-questions", { surveyId });
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
