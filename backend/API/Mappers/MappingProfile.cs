using AutoMapper;
using BL.Models;
using BL.Models.DTO;
using BL.Models.DTO.Input;
using BL.Models.DTO.Output;

namespace API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BestuurderInputDTO, Bestuurder>();
            CreateMap<Bestuurder, BestuurderOutputDTO>();

            CreateMap<VoertuigInputDTO, Voertuig>();
            CreateMap<Voertuig, VoertuigOutputDTO>();

            CreateMap<ReserveringInputDTO, Reservering>();
            CreateMap<Reservering, ReserveringOutputDTO>()
                .ForMember(dest => dest.CheckOutPictures, opt => opt.MapFrom(src => src.CheckOutPictures))
                .ForMember(dest => dest.CheckInPictures, opt => opt.MapFrom(src => src.CheckInPictures));

            CreateMap<CheckOutDTO, Reservering>()
                .ForMember(dest => dest.CheckOutState, opt => opt.MapFrom(src => src.CheckOutState))
                .ForMember(dest => dest.CheckOutPictures, opt => opt.Ignore());

            CreateMap<CheckInDTO, Reservering>()
                .ForMember(dest => dest.CheckInState, opt => opt.MapFrom(src => src.CheckInState))
                .ForMember(dest => dest.CheckInPictures, opt => opt.Ignore());
        }
    }
}
