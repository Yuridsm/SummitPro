﻿using AutoMapper;
using SummitPro.Core.Aggregate.Participant;

namespace SummitPro.Infrastructure.RepositoryInMemory.Models
{
    public class ParticipantModel
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public double ContributionValue { get; set; }
        public string BringDrink { get; set; } = string.Empty;
        public List<string> Items { get; set; } = new();
        public string Username { get; set; } = string.Empty;
    }

    public class ParticipantModelMapperProfile : Profile
    {
        public ParticipantModelMapperProfile()
        {
            CreateMap<Participant, ParticipantModel>()
                .ForMember(destination => destination.Name, map =>
                {
                    map.MapFrom(src => src.Name.Value);
                })
                .ForMember(destination => destination.ContributionValue, map =>
                {
                    map.MapFrom(src => src.ContributionValue.Value);
                })
                .ForMember(destination => destination.BringDrink, map =>
                {
                    map.MapFrom(src => src.BringDrink.ToString());
                })
                .ForMember(destination => destination.Username, map =>
                {
                    map.MapFrom(src => src.Username.Value);
                })
                .ForMember(destination => destination.Items, map =>
                {
                    map.MapFrom(src => src.Items);
                });

            CreateMap<ParticipantModel, Participant>()
                .ConstructUsing(src => Participant.FactoryMethod(
                    src.Name,
                    src.Username,
                    src.ContributionValue,
                    src.BringDrink == "True" ? true : false)
                .AddItems(src.Items));
        }
    }
}
