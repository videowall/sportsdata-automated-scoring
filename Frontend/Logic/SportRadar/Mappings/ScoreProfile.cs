using AutoMapper;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.Logic.SportRadar.Mappings;

internal sealed class ScoreProfile : Profile
{
    public ScoreProfile()
    {
        CreateMap<Score, Entities.Score>(MemberList.Source);
    }
}