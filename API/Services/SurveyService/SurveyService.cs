using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CloudStorage;
using AutoMapper;
using System.Linq;
using Domain.Models.Group;
using Domain.Models.Survey;
using Domain.Repositories.SurveyRepository;
using Domain.Models.Results;

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

        public async ValueTask<IEnumerable<SurveyListModel>> GetSurveysAsync()
        {
            var surveys = await surveyRepository.GetSurveysAsync();
            return mapper.Map<IEnumerable<SurveyListModel>>(surveys);
        }

        public async ValueTask<IEnumerable<QuestionDto>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var questions = await surveyRepository.GetSurveyQuestionsAsync(surveyId);
            return mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async ValueTask<IEnumerable<StudentModel>> GetSurveyStudentsAsync(Guid surveyId)
        {
            return await surveyRepository.GetSurveyStudentsAsync(surveyId);
        }

        public async ValueTask<IEnumerable<AnswerDto>> GetSurveyAnswersAsync(Guid surveyId)
        {
            var answers = await surveyRepository.GetSurveyAnswersAsync(surveyId);
            return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async ValueTask<IEnumerable<AnswerVisualizationModel>> GetSurveyAnswersVisualizationAsync(Guid surveyId)
        {
            var answers = await surveyRepository.GetSurveyAnswersAsync(surveyId);
            var questions = await surveyRepository.GetSurveyQuestionsAsync(surveyId);
            var visualization = questions.Where(q => q.Options.Count != 0).Select(q => new AnswerVisualizationModel
            {
                QuestionText = q.Text,
                Options = q.Options.Select(o => new OptionVisualizationModel
                {
                    OptionText = o.Text,
                    AnswersAmount = answers.Count(a => a.Option?.Text == o.Text),
                    IsCorrect = o.IsCorrect
                })
            });
            return visualization;
        }

        public async ValueTask<IEnumerable<CsvModel>> GetSurveyCsvAnswersAsync(Guid surveyId)
        {
            var answers = await surveyRepository.GetSurveyAnswersAsync(surveyId);
            return mapper.Map<IEnumerable<CsvModel>>(answers);
        }

        public async ValueTask<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            var answers = await surveyRepository.GetStudentAnswersAsync(surveyId, studentId);
            var val = mapper.Map<IEnumerable<AnswerDto>>(answers);
           return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async ValueTask<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto)
        {
            var newSurvey = await surveyRepository.AddSurveyAsync(mapper.Map<SurveyModel>(surveyDto));
            return mapper.Map<SurveyDto>(newSurvey);
        }

        public async ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened)
        {
            await surveyRepository.ChangeSurveyStatusAsync(surveyId, isOpened);
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
