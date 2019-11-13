using System.Collections;
using System.Collections.Generic;
using collegues_api.Controllers.Dto;
using collegues_api.Models;

namespace collegues_api.Repositories
{
    public interface ICollegueRepository
    {
        Collegue FindColleagueByMatricule(string matricule);

        IEnumerable<string> GetColleagueMatriculesByNom(string nom);

        void SaveColleague(Collegue collegue);

        void UpdateColleague(ColleguePatchDto collegueDto);
    }
}