using Domain.Models.Survey;

namespace Application.MappingProfiles
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<QuestionDto, Question>();
            CreateMap<SurveyDto, Survey>();
        }
    }
}
