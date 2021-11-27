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

            CreateMap<AnswerModel, AnswerDto>().ForMember(a => a.ImageUrl, o => o.MapFrom(a => a.Image.Url));
        }
    }
}
