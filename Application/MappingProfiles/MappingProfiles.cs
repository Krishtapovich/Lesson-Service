using Domain.Models.Survey;

namespace Application.MappingProfiles
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();

            CreateMap<SurveyDto, Survey>();
            CreateMap<Survey, SurveyDto>();

            CreateMap<Answer, AnswerDto>().ForMember(a => a.ImageUrl, o => o.MapFrom(a => a.Image.Url));
        }
    }
}
