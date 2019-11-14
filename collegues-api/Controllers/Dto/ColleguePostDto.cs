using ColleguesApi.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ColleguesApi.Models
{
    public class ColleguePostDto
    {
        [MinLength(2, ErrorMessage = "La propriété nom ne respecte la taille imposée de 2 caractères minimum")]
        public string Nom { get; set; }

        [MinLength(2, ErrorMessage = "La propriété prénoms ne respecte la taille imposée de 2 caractères minimum")]
        public string Prenoms { get; set; }

        [MinLength(3, ErrorMessage = "La propriété email ne respecte la taille imposée de 3 caractères minimum")]
        [EmailAddress]
        public string Email { get; set; }

        [CustomValidation(typeof(AgeValidation), nameof(AgeValidation.AgeValidate))]
        public DateTime DateDeNaissance { get; set; }

        [RegularExpression(@"^(http.*$).*", ErrorMessage = "L'url doit commencer par http")]
        public Uri PhotoUrl { get; set; }

        public ColleguePostDto()
        {
        }

        public ColleguePostDto(string nom, string prenoms, string email, DateTime dateDeNaissance, Uri photoUrl)
        {
            this.Nom = nom;
            this.Prenoms = prenoms;
            this.Email = email;
            this.DateDeNaissance = dateDeNaissance;
            this.PhotoUrl = photoUrl;
        }
    }
}