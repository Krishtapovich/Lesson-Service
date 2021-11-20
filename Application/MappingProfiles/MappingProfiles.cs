using Domain.Models.Survey;

namespace Application.MappingProfiles
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<SurveyDto, Survey>();
            CreateMap<Survey, SurveyDto>();

            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();

            CreateMap<OptionDto, Option>();
            CreateMap<Option, OptionDto>();

            CreateMap<Answer, AnswerDto>().ForMember(a => a.ImageUrl, o => o.MapFrom(a => a.Image.Url));
        }
    }
}
