using System;
using System.Collections.Generic;
using System.Linq;
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
            .ForMember(d => d.Id, m => m.Ignore())
            .ForMember(d => d.Scores, m => m.Ignore())
            .ForMember(d => d.Events, m => m.Ignore())
            .ForMember(d => d.Status, m => m.Ignore())
            .ForMember(d => d.Team1Line1, m => m.Ignore())
            .ForMember(d => d.Team1Line2, m => m.Ignore())
            .ForMember(d => d.Team2Line1, m => m.Ignore())
            .ForMember(d => d.Team2Line2, m => m.Ignore());

        CreateMap<MatchBase, Entities.Match>(MemberList.None)
            .IncludeAllDerived()
            .AfterMap((s, d) =>
            {
                d.Team1Line1 = s.T1Name != null ? GetPlayerName(s.T1Name, 0) : d.Team1Line1;
                d.Team1Line2 = s.T1Name != null ? GetPlayerName(s.T1Name, 1) : d.Team1Line2;
                d.Team2Line1 = s.T2Name != null ? GetPlayerName(s.T2Name, 0) : d.Team2Line1;
                d.Team2Line2 = s.T2Name != null ? GetPlayerName(s.T2Name, 1) : d.Team2Line2;
            });

        CreateMap<MatchData, Entities.Match>(MemberList.Source);
        
        CreateMap<MatchUpdate, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status ?? ScoutMatchStatus.Undefined))
            .AfterMap((data, entity, context) =>
            {
                var scores = data.Scores?.ToList() ?? new List<Score>();
                foreach (var score in scores)
                {
                    var scoreType = (Entities.ScoreType) Enum.Parse(typeof(Entities.ScoreType), score.Type, true);
                    var scoreDb = entity.Scores.FirstOrDefault(es => es.Type == scoreType);
                    if (scoreDb != null)
                    {
                        context.Mapper.Map(score, scoreDb);
                    }
                    else
                    {
                        var newScore = context.Mapper.Map<Entities.Score>(score);
                        newScore.MatchId = entity.Id;
                        entity.Scores.Add(newScore);
                    }
                }
            });
        
        CreateMap<MatchUpdateDeltaUpdate, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status ?? ScoutMatchStatus.Undefined))
            .AfterMap((data, entity, context) =>
            {
                var scores = data.Scores?.ToList() ?? new List<Score>();
                foreach (var score in scores)
                {
                    var scoreType = (Entities.ScoreType) Enum.Parse(typeof(Entities.ScoreType), score.Type, true);
                    var scoreDb = entity.Scores.FirstOrDefault(es => es.Type == scoreType);
                    if (scoreDb != null)
                    {
                        context.Mapper.Map(score, scoreDb);
                    }
                    else
                    {
                        var newScore = context.Mapper.Map<Entities.Score>(score);
                        newScore.MatchId = entity.Id;
                        entity.Scores.Add(newScore);
                    }
                }
            });

        CreateMap<MatchUpdateDelta, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status ?? ScoutMatchStatus.Undefined));

        CreateMap<MatchListItem, Entities.Match>(MemberList.Source)
            .ForMember(d => d.Status, m => m.MapFrom(s => s.MatchStatus ?? ScoutMatchStatus.Undefined));
    }

    private string GetPlayerName(string name, long index)
    {
        if (name == null) return null;
        
        var splittedName = name.Split(",");
        return splittedName.Length >= 2 ? splittedName[index].Trim() : index == 0 ? name.Trim() : string.Empty;
    }
}