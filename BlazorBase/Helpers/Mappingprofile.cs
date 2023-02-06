using AutoMapper;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;

namespace BlazorBase.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Profil, ProfilDto>();
            CreateMap<Adresse, AdresseDto>();
            CreateMap<Personne, PersonneDto>();
            CreateMap<Candidature, CandidatureDto>();
        }
    }
}