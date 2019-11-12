using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace collegues_api.Controllers.Dto
{
    public class ColleguePatchDto
    {
        public string Matricule { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }

        public ColleguePatchDto()
        {
        }

        public ColleguePatchDto(string matricule, string email, string photoUrl)
        {
            this.Matricule = matricule;
            this.Email = email;
            this.PhotoUrl = photoUrl;
        }
    }
}