using AutoMapper;
using ColleguesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColleguesApi.Configurations;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Repositories;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace ColleguesApi.Services
{
    public class CollegueService : ICollegueService
    {
        private readonly IMapper _mapper;
        private readonly ICollegueRepository _collegueRepository;

        public CollegueService(IMapper mapper, ICollegueRepository collegueRepository)
        {
            _mapper = mapper;
            _collegueRepository = collegueRepository;
        }

        public Task<IEnumerable<string>> RechercherParNom(string nom)
        {
            return _collegueRepository.GetColleagueMatriculesByNomAsync(nom);
        }

        public Task<Collegue> RechercherParMatricule(string matricule)
        {
            return _collegueRepository.FindColleagueByMatriculeAsync(matricule);
        }

        public async Task<Collegue> AjouterUnCollegue(ColleguePostDto collegueDto)
        {
            if (collegueDto != null)
            {
                var collegue = _mapper.Map<ColleguePostDto, Collegue>(collegueDto);
                collegue.Matricule = Guid.NewGuid().ToString();

                try
                {
                    return await Task.Run(() => _collegueRepository
                        .SaveColleagueAsync(collegue))
                        .ContinueWith(antecedent => RechercherParMatricule(collegue.Matricule).Result, TaskScheduler.Default)
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw new ProblemeTechniqueException();
                }
            }
            else
            {
                throw new CollegueInvalideException();
            }
        }

        public async Task<Collegue> ModifierCollegue(ColleguePatchDto collegueDto)
        {
            if (collegueDto == null)
            {
                throw new CollegueInvalideException();
            }

            await _collegueRepository.UpdateColleagueAsync(collegueDto).ConfigureAwait(false);
            return await RechercherParMatricule(collegueDto.Matricule).ConfigureAwait(false);
        }
    }
}