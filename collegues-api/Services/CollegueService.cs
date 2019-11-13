using AutoMapper;
using collegues_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using collegues_api.Configurations;
using collegues_api.Controllers.Dto;
using collegues_api.Repositories;

namespace collegues_api.Services
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

        public IEnumerable<string> RechercherParNom(string nom)
        {
            return _collegueRepository.GetColleagueMatriculesByNom(nom);
        }

        public Collegue RechercherParMatricule(string matricule)
        {
            return _collegueRepository.FindColleagueByMatricule(matricule);
        }

        public Collegue AjouterUnCollegue(ColleguePostDto collegueDto)
        {
            if (collegueDto != null)
            {
                var collegue = _mapper.Map<ColleguePostDto, Collegue>(collegueDto);
                collegue.Matricule = Guid.NewGuid().ToString();
                try
                {
                    _collegueRepository.SaveColleague(collegue);
                }
                catch (Exception e)
                {
                    throw new ProblemeTechniqueException(e.Message);
                }
                return RechercherParMatricule(collegue.Matricule);
            }
            else
            {
                throw new CollegueInvalideException("Erreur : impossible de récupérer de telles données");
            }
        }

        public Collegue ModifierCollegue(ColleguePatchDto collegueDto)
        {
            try
            {
                _collegueRepository.UpdateColleague(collegueDto);
                return _collegueRepository.FindColleagueByMatricule(collegueDto.Matricule);
            }
            catch (ArgumentNullException)
            {
                throw new CollegueNonTrouveException("Erreur : matricule non trouvé");
            }
            catch (Exception e)
            {
                throw new ProblemeTechniqueException(e.Message);
            }
        }
    }
}