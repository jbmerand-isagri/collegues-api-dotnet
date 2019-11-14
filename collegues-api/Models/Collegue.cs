using ColleguesApi.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ColleguesApi.Models
{
    [Table("COLLEGUES")]
    public class Collegue
    {
        [Key]
        public string Matricule { get; set; }

        [MaxLength(255), MinLength(2)]
        public string Nom { get; set; }

        [MaxLength(255), MinLength(2)]
        public string Prenoms { get; set; }

        [MaxLength(255), MinLength(3)]
        [EmailAddress]
        public string Email { get; set; }

        [CustomValidation(typeof(AgeValidation), nameof(AgeValidation.AgeValidate))]
        public DateTime DateDeNaissance { get; set; }

        [RegularExpression(@"^(http.*$).*")]
        public Uri PhotoUrl { get; set; }

        public Collegue()
        {
        }

        public Collegue(string matricule, string nom, string prenoms, string email, DateTime dateDeNaissance, Uri photoUrl)
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