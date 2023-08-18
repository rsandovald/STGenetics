using Models.DataEntities;
using Models.DTOs;
using AutoMapper;

namespace ApiSTGenetics.Automapper
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles()
        {
            CreateMap<AnimalBaseDto, Animal>().ReverseMap();
        }
    }
}
