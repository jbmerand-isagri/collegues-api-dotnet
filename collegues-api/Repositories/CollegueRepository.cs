using ColleguesApi.Configurations;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Models;
using ColleguesApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace ColleguesApi.Repositories
{
    public class CollegueRepository : ICollegueRepository
    {
        private readonly ColleguesContext _colleguesContext;

        public CollegueRepository(ColleguesContext colleguesContext)
        {
            _colleguesContext = colleguesContext;
        }

        public async void SaveColleagueAsync(Collegue collegue)
        {
            _colleguesContext.Collegues.Add(collegue);
            await _colleguesContext.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<StringComparison generates an InvalidOperationError from database>")]
        public async void UpdateColleagueAsync(ColleguePatchDto collegueDto)
        {
            if (collegueDto == null)
            {
                throw new CollegueInvalideException();
            }

            try
            {
                var collegue = _colleguesContext.Collegues
                    .FirstOrDefault(c => string.Equals(c.Matricule, collegueDto.Matricule));

                if (collegueDto.Email != null)
                {
                    collegue.Email = collegueDto.Email;
                }
                if (collegueDto.PhotoUrl != null)
                {
                    collegue.PhotoUrl = collegueDto.PhotoUrl;
                }
                await _colleguesContext.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw new CollegueNonTrouveException();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<StringComparison generates an InvalidOperationError from database>")]
        public async Task<IEnumerable<string>> GetColleagueMatriculesByNomAsync(string nom)
        {
            return await _colleguesContext.Collegues
                .Where(c => string.Equals(c.Nom, nom))
                .Select(c => c.Matricule)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<StringComparison generates an InvalidOperationError from database>")]
        public async Task<Collegue> FindColleagueByMatriculeAsync(string matricule)
        {
            try
            {
                if ((await _colleguesContext.Collegues.FirstOrDefaultAsync(c => c.Matricule.Equals(matricule)).ConfigureAwait(false)) != null)
                {
                    return await _colleguesContext.Collegues
                       .FirstOrDefaultAsync(c => c.Matricule.Equals(matricule))
                       .ConfigureAwait(false);
                }
                else
                {
                    throw new CollegueNonTrouveException();
                }
            }
            catch (ArgumentNullException)
            {
                throw new CollegueNonTrouveException();
            }
            catch (InvalidOperationException)
            {
                throw new ProblemeTechniqueException();
            }
        }
    }
}