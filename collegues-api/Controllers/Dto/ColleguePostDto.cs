using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace collegues_api.Models
{
    public class ColleguePostDto
    {
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Email { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string PhotoUrl { get; set; }

        public ColleguePostDto()
        {
        }

        public ColleguePostDto(string nom, string prenoms, string email, DateTime dateDeNaissance, string photoUrl)
        {
            this.Nom = nom;
            this.Prenoms = prenoms;
            this.Email = email;
            this.DateDeNaissance = dateDeNaissance;
            this.PhotoUrl = photoUrl;
        }
    }
}
