using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace collegues_api.Models
{
    public class Collegue
    {
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Email { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string PhotoUrl { get; set; }

        public Collegue()
        {
        }

        public Collegue(string matricule, string nom, string prenoms, string email, DateTime dateDeNaissance, string photoUrl)
        {
            this.Matricule = matricule;
            this.Nom = nom;
            this.Prenoms = prenoms;
            this.Email = email;
            this.DateDeNaissance = dateDeNaissance;
            this.PhotoUrl = photoUrl;
        }
    }
}
