using AutoMapper;
using collegues_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using collegues_api.Configurations;
using collegues_api.Controllers.Dto;

namespace collegues_api.Services
{
    public class CollegueService : ICollegueService
    {
        private Dictionary<String, Collegue> data = new Dictionary<string, Collegue>();
        private readonly IMapper _mapper;
        private readonly ColleguesContext _colleguesContext;

        public CollegueService(IMapper mapper, ColleguesContext colleguesContext)
        {
            _mapper = mapper;
            _colleguesContext = colleguesContext;
        }

        public IEnumerable<string> RechercherParNom(string nom)
        {
            return from c in data where c.Value.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase) select c.Value.Matricule;
        }

        public Collegue RechercherParMatricule(string matricule)
        {
            if (data.ContainsKey(matricule))
            {
                return data[matricule];
            }
            else
            {
                throw new CollegueInvalideException("Erreur : aucun collègue trouvé pour ce matricule");
            }
        }

        public Collegue AjouterUnCollegue(ColleguePostDto collegueDto)
        {
            if (collegueDto != null)
            {
                var collegue = _mapper.Map<ColleguePostDto, Collegue>(collegueDto);
                collegue.Matricule = Guid.NewGuid().ToString();
                _colleguesContext.Collegues.Add(collegue);
                _colleguesContext.SaveChanges();

                return (from co in _colleguesContext.Collegues
                        where co.Matricule == collegue.Matricule
                        select co).Single();
            }
            else
            {
                throw new CollegueInvalideException("Erreur : impossible de récupérer de telles données");
            }
        }

        public Collegue ModifierEmail(ColleguePatchDto collegueDto)
        {
            if (collegueDto.Email != null && collegueDto.Email.Length >= 3 && collegueDto.Email.Contains("@"))
            {
                data[collegueDto.Matricule].Email = collegueDto.Email;
                return data[collegueDto.Matricule];
            }
            else
            {
                throw new CollegueInvalideException("Erreur : cet email ne respecte pas le format imposé");
            }
        }

        public Collegue ModifierPhotoUrl(ColleguePatchDto collegueDto)
        {
            if (collegueDto.PhotoUrl != null && collegueDto.PhotoUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                data[collegueDto.Matricule].PhotoUrl = collegueDto.PhotoUrl.ToLower();
                return data[collegueDto.Matricule];
            }
            else
            {
                throw new CollegueInvalideException("Erreur : cette url ne commence pas par http");
            }
        }

        public Collegue ModifierCollegue(ColleguePatchDto collegueDto)
        {
            try
            {
                data.ContainsKey(collegueDto.Matricule);
                if (collegueDto.Email != null)
                {
                    ModifierEmail(collegueDto);
                }
                if (collegueDto.PhotoUrl != null)
                {
                    ModifierPhotoUrl(collegueDto);
                }
            }
            catch(ArgumentNullException)
            {
                throw new CollegueNonTrouveException("Erreur : matricule non trouvé");
            }
            return data[collegueDto.Matricule];
        }

    }
}
