import AnswerModel from "@Models/Answer";
import { SurveyCreateModel, SurveyModel, SurveyToGroups } from "@Models/Survey";

import BaseService from "./Base";

class SurveyService extends BaseService {
  getSurveys(pageNumber: number, pageSize: number) {
    return this.requests.get<Array<SurveyModel>>("instructor/surveys", { pageNumber, pageSize });
  }

  getStudentAnswers(surveyId: string, studentId: number) {
    return this.requests.get<Array<AnswerModel>>("instructor/student-answers", {
      surveyId,
      studentId
    });
  }

  createSurvey(survey: SurveyCreateModel) {
    return this.requests.post<SurveyModel>("instructor/create-survey", survey);
  }

  sendSurveyToGroup(survey: SurveyToGroups) {
    this.requests.post<void>("instructor/send-survey", survey);
  }

  closeSurvey(surveyId: string) {
    this.requests.put<void>("instructor/close-survey", {}, { surveyId });
  }

  deleteSurvey(surveyId: string) {
    this.requests.delete<void>("instructor/delete-survey", { surveyId });
  }
}

const surveyService = new SurveyService();

export default surveyService;
