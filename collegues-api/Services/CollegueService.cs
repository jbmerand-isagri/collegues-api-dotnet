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

        public Task<Collegue> AjouterUnCollegue(ColleguePostDto collegueDto)
        {
            if (collegueDto != null)
            {
                var collegue = _mapper.Map<ColleguePostDto, Collegue>(collegueDto);
                collegue.Matricule = Guid.NewGuid().ToString();
                try
                {
                    _collegueRepository.SaveColleagueAsync(collegue);
                }
                catch (Exception)
                {
                    throw new ProblemeTechniqueException();
                }
                return RechercherParMatricule(collegue.Matricule);
            }
            else
            {
                throw new CollegueInvalideException();
            }
        }

        public Task<Collegue> ModifierCollegue(ColleguePatchDto collegueDto)
        {
            if (collegueDto == null)
            {
                throw new CollegueInvalideException();
            }

            try
            {
                _collegueRepository.UpdateColleagueAsync(collegueDto);
                return _collegueRepository.FindColleagueByMatriculeAsync(collegueDto.Matricule);
            }
            catch (ArgumentNullException)
            {
                throw new CollegueNonTrouveException();
            }
            catch (Exception e)
            {
                throw new ProblemeTechniqueException(e.Message);
            }
        }
    }
}