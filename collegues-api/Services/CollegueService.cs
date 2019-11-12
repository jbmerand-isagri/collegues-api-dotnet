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

        public CollegueService(IMapper mapper)
        {
            _mapper = mapper;
            var collegue1 = new Collegue(Guid.NewGuid().ToString(), "Dupuis", "Jean", "jean.dupuis@mail.com", new DateTime(1980, 1, 18), "https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale");
            var collegue2 = new Collegue(Guid.NewGuid().ToString(), "Durand", "Bernard", "bernard.durand@mail.com", new DateTime(1982, 11, 23), "https://secure.i.telegraph.co.uk/multimedia/archive/03127/terry_crews_3127762b.jpg");
            var collegue3 = new Collegue(Guid.NewGuid().ToString(), "Doe", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), "https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg");

            data.Add(collegue1.Matricule, collegue1);
            data.Add(collegue2.Matricule, collegue2);
            data.Add(collegue3.Matricule, collegue3);
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
                data.Add(collegue.Matricule, collegue);
                return collegue;
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
