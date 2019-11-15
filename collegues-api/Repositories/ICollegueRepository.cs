using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Models;

namespace ColleguesApi.Repositories
{
    public interface ICollegueRepository
    {
        Task<Collegue> FindColleagueByMatriculeAsync(string matricule);

        Task<IEnumerable<string>> GetColleagueMatriculesByNomAsync(string nom);

        Task<int> SaveColleagueAsync(Collegue collegue);

        Task<int> UpdateColleagueAsync(ColleguePatchDto collegueDto);
    }
}