using System.Collections.Generic;
using System.Threading.Tasks;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Models;

namespace ColleguesApi.Services
{
    public interface ICollegueService
    {
        Task<IEnumerable<string>> RechercherParNom(string nom);

        Task<Collegue> RechercherParMatricule(string matricule);

        Task<Collegue> AjouterUnCollegue(ColleguePostDto collegueAAjouter);

        Task<Collegue> ModifierCollegue(ColleguePatchDto collegueDto);
    }
}