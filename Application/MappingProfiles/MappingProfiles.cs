using Domain.Models.Survey;

namespace Application.MappingProfiles
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<SurveyDto, SurveyModel>();
            CreateMap<SurveyModel, SurveyDto>();
            CreateMap<SurveyModel, SurveyListModel>();

            CreateMap<QuestionDto, QuestionModel>();
            CreateMap<QuestionModel, QuestionDto>();

            CreateMap<OptionDto, OptionModel>();
            CreateMap<OptionModel, OptionDto>();

            CreateMap<AnswerModel, AnswerDto>()
                .ForMember(a => a.ImageUrl, o => o.MapFrom(a => a.Image.Url))
                .ForMember(a => a.Question, o => o.MapFrom(a => a.QuestionMessage.Question));

            CreateMap<AnswerModel, CsvModel>()
                .ForMember(c => c.FirstName, o => o.MapFrom(a => a.QuestionMessage.Student.FirstName))
                .ForMember(c => c.LastName, o => o.MapFrom(a => a.QuestionMessage.Student.LastName))
                .ForMember(c => c.GroupNumber, o => o.MapFrom(a => a.QuestionMessage.Student.GroupNumber))
                .ForMember(c => c.QuestionText, o => o.MapFrom(a => a.QuestionMessage.Question.Text))
                .ForMember(c => c.AnswerText, o => o.MapFrom(a => a.Text ?? (a.Option != null ? a.Option.Text : a.Image.Url)));
        }
    }
}
