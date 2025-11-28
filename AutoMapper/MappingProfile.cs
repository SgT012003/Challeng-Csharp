using AutoMapper;
using CarePlusApi.DTOs;
using CarePlusApi.Models;

namespace CarePlusApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Challenge Mappings
            CreateMap<CreateChallengeDto, Challenge>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<Challenge, ChallengeResponseDto>()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            // UserChallenge Mappings
            CreateMap<UserChallenge, UserChallengeResponseDto>()
                .ForMember(dest => dest.ChallengeTitulo, opt => opt.MapFrom(src => src.Challenge.Titulo));

            // Reward/Benefit Mappings
            CreateMap<Reward, BenefitResponseDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsClaimed, opt => opt.Ignore())
                .ForMember(dest => dest.ClaimedAt, opt => opt.Ignore());
        }
    }
}
