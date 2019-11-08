using System.Collections.Generic;
using collegues_api.Models;

namespace collegues_api.Services
{
    public interface ICollegueService
    {
        IEnumerable<string> RechercherParNom(string nom);
        Collegue RechercherParMatricule(string matricule);
        Collegue AjouterUnCollegue(ColleguePostDto collegueAAjouter);
    }
}