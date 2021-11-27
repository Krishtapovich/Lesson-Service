using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CloudStorage;
using AutoMapper;
using Domain.Models.Survey;
using Domain.Repositories.SurveyRepository;

namespace API.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly ICloudStorage cloud;
        private readonly IMapper mapper;

        public SurveyService(ISurveyRepository surveyRepository, ICloudStorage cloud, IMapper mapper)
        {
            this.surveyRepository = surveyRepository;
            this.cloud = cloud;
            this.mapper = mapper;
        }

        public async ValueTask<IEnumerable<SurveyListModel>> GetSurveysAsync(int pageNumber, int pageSize)
        {
            var surveys = await surveyRepository.GetSurveysAsync(pageNumber, pageSize);
            return mapper.Map<IEnumerable<SurveyListModel>>(surveys);
        }

        public async ValueTask<IEnumerable<QuestionDto>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var questions = await surveyRepository.GetSurveyQuestionsAsync(surveyId);
            return mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async ValueTask<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            var answers = await surveyRepository.GetStudentAnswersAsync(surveyId, studentId);
            return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened)
        {
            await surveyRepository.ChangeSurveyStatusAsync(surveyId, isOpened);
        }

        public async ValueTask<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto)
        {
            var newSurvey = await surveyRepository.AddSurveyAsync(mapper.Map<SurveyModel>(surveyDto));
            return mapper.Map<SurveyDto>(newSurvey);
        }

        public async ValueTask DeleteSurveyAsync(Guid surveyId)
        {
            var images = await surveyRepository.GetAnswersImagesAsync(surveyId);
            foreach (var image in images)
            {
                await cloud.DeleteImageAsync(image.FileName);
            }
            await surveyRepository.DeleteSurveyAsync(surveyId);
        }
    }
}
