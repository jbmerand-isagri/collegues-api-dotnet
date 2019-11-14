using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColleguesApi.Controllers.Dto
{
    public class ColleguePatchDto
    {
        public string Matricule { get; set; }
        public string Email { get; set; }
        public Uri PhotoUrl { get; set; }

        public ColleguePatchDto()
        {
        }

        public ColleguePatchDto(string matricule, string email, Uri photoUrl)
        {
            Matricule = matricule;
            Email = email;
            PhotoUrl = photoUrl;
        }
    }
}