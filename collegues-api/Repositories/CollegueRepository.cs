using collegues_api.Configurations;
using collegues_api.Controllers.Dto;
using collegues_api.Models;
using collegues_api.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace collegues_api.Repositories
{
    public class CollegueRepository : ICollegueRepository
    {
        private readonly ColleguesContext _colleguesContext;

        public CollegueRepository(ColleguesContext colleguesContext)
        {
            _colleguesContext = colleguesContext;
        }

        public void SaveColleague(Collegue collegue)
        {
            _colleguesContext.Collegues.Add(collegue);
            _colleguesContext.SaveChanges();
        }

        public void UpdateColleague(ColleguePatchDto collegueDto)
        {
            try
            {
                var collegue = _colleguesContext.Collegues.Single(c => string.Equals(c.Matricule, collegueDto.Matricule));
                if (collegueDto.Email != null)
                {
                    collegue.Email = collegueDto.Email;
                }
                if (collegueDto.PhotoUrl != null)
                {
                    collegue.PhotoUrl = collegueDto.PhotoUrl;
                }
                _colleguesContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new CollegueNonTrouveException();
            }
        }

        public IEnumerable<string> GetColleagueMatriculesByNom(string nom)
        {
            return _colleguesContext.Collegues.Where(c => string.Equals(c.Nom, nom)).Select(c => c.Matricule).ToList();
        }

        public Collegue FindColleagueByMatricule(string matricule)
        {
            try
            {
                return _colleguesContext.Collegues.Where(c => string.Equals(c.Matricule, matricule)).Single();
            }
            catch (ArgumentNullException)
            {
                throw new CollegueNonTrouveException("Erreur : aucun collègue trouvé pour ce matricule");
            }
            catch (InvalidOperationException e)
            {
                throw new ProblemeTechniqueException();
            }
        }
    }
}