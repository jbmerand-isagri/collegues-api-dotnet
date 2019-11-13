using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using collegues_api.Controllers.Dto;
using collegues_api.Models;
using collegues_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace collegues_api.Controllers
{
    [Route("collegues")]
    public class CollegueController : Controller
    {
        private readonly ICollegueService _collegueService;

        public CollegueController(ICollegueService collegueService)
        {
            _collegueService = collegueService;
        }

        // GET: collegues?nom=NOM
        [HttpGet]
        public IEnumerable<string> GetMatriculeFromName(string nom)
        {
            return this._collegueService.RechercherParNom(nom);
        }

        // GET collegues/MATRICULE
        [HttpGet("{matricule}")]
        public IActionResult GetCollegueFromMatricule(string matricule)
        {
            try
            {
                return Ok(_collegueService.RechercherParMatricule(matricule));
            }
            catch (CollegueInvalideException)
            {
                return NotFound();
            }
        }

        // POST collegues
        [HttpPost]
        public IActionResult PostNewCollegue([FromBody]ColleguePostDto collegueDto)
        {
            try
            {
                var context = new ValidationContext(collegueDto);
                ICollection<ValidationResult> result = null;
                var isValid = Validator.TryValidateObject(collegueDto, context, result, true);

                if (isValid)
                {
                    return Ok(_collegueService.AjouterUnCollegue(collegueDto));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PATCH collegues/MATRICULE
        [HttpPatch("{matricule}")]
        public IActionResult PatchColleague(string matricule, [FromBody]ColleguePatchDto collegueDto)
        {
            try
            {
                collegueDto.Matricule = matricule;
                var collegue = _collegueService.RechercherParMatricule(matricule);
                return Ok(_collegueService.ModifierCollegue(collegueDto));
            }
            catch (CollegueNonTrouveException e)
            {
                return NotFound(e.Message);
            }
            catch (CollegueInvalideException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est survenue");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}