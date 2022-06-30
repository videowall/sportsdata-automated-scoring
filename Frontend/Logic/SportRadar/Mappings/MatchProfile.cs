using AutoMapper;
using WBH.Livescoring.SportRadar;
using WBH.Livescoring.SportRadar.Bases;

namespace WBH.Livescoring.Frontend.Logic.SportRadar.Mappings;

internal sealed class MatchProfile: Profile
{
    public MatchProfile()
    {
        CreateMap<MatchIdBase, Entities.Match>(MemberList.None)
            .IncludeAllDerived()
            .ForMember(d => d.Id, m => m.Ignore());

        CreateMap<MatchBase, Entities.Match>(MemberList.None)
            .IncludeAllDerived()
            .ForMember(d => d.Team1Line1, m => m.MapFrom(s => GetPlayerName(s.T1Name, 0)))
            .ForMember(d => d.Team1Line2, m => m.MapFrom(s => GetPlayerName(s.T1Name, 1)))
            .ForMember(d => d.Team2Line1, m => m.MapFrom(s => GetPlayerName(s.T2Name, 0)))
            .ForMember(d => d.Team2Line2, m => m.MapFrom(s => GetPlayerName(s.T2Name, 1)));

        CreateMap<MatchData, Entities.Match>(MemberList.Source);
        
        CreateMap<MatchUpdate, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Scores, m => m.MapFrom(s => s.Scores))
            .AfterMap((s, d) =>
            {
                foreach (var score in d.Scores)
                {
                    score.MatchId = s.MatchId;
                }
            });
        
        CreateMap<MatchUpdateDeltaUpdate, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Scores, m => m.MapFrom(s => s.Scores))
            .AfterMap((s, d) =>
            {
                foreach (var score in d.Scores)
                {
                    score.MatchId = s.MatchId;
                }
            });

        CreateMap<MatchUpdateDelta, Entities.Match>(MemberList.Source);

    }

    private string GetPlayerName(string name, long index)
    {
        if (name == null) return null;
        
        var splittedName = name.Split(",");
        return splittedName.Length >= 2 ? splittedName[index] : index == 0 ? name : string.Empty;
    }
}