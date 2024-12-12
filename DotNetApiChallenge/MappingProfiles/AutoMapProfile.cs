using AutoMapper;
using DotNetApiChallenge.Dto;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.MappingProfiles
{
    public class AutoMapProfile : Profile
    {
        /// <summary>
        /// Mapping Definitions for Automapper 13
        /// </summary>
        public AutoMapProfile()
        {
            CreateMap<Color, ColorDto>();
            CreateMap<Person, PersonDto>();
            //The formember definition is probably not necessary with AutoMapper 13 but 
            //in the past I've always resolved nested dependencies with my own logic.
            CreateMap<Person, ComplexPersonDto>()
                .ForMember( 
                member => member.Color, 
                o => o.MapFrom( t => new ColorDto { Id = t.Color.Id, Name = t.Color.Name} )
                );
            CreateMap<Person, CustomSerializerPersonDto>()
                .ForMember(
                member => member.Color,
                o => o.MapFrom(t => new ColorDto { Id = t.Color.Id, Name = t.Color.Name })
                );
        }
    }
}
