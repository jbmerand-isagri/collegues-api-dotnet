using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Models;
using ColleguesApi.Repositories;
using ColleguesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ColleguesApi.Controllers
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
        public Task<IEnumerable<string>> GetMatriculeFromNameAsync(string nom)
        {
            return _collegueService.RechercherParNom(nom);
        }

        // GET collegues/MATRICULE
        [HttpGet("{matricule}")]
        public IActionResult GetCollegueFromMatriculeAsync(string matricule)
        {
            try
            {
                return Ok(_collegueService.RechercherParMatricule(matricule).Result);
            }
            catch (CollegueInvalideException)
            {
                return BadRequest();
            }
            catch (CollegueNonTrouveException)
            {
                return NotFound();
            }
            catch (ProblemeTechniqueException)
            {
                return BadRequest();
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
                    return Ok(_collegueService.AjouterUnCollegue(collegueDto).Result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (ProblemeTechniqueException)
            {
                return BadRequest();
            }
        }

        // PATCH collegues/MATRICULE
        [HttpPatch("{matricule}")]
        public IActionResult PatchColleagueAsync(string matricule, [FromBody]ColleguePatchDto collegueDto)
        {
            try
            {
                if (collegueDto != null)
                {
                    collegueDto.Matricule = matricule;
                    var collegue = _collegueService.RechercherParMatricule(matricule);
                    return Ok(_collegueService.ModifierCollegue(collegueDto).Result);
                }
                else
                {
                    throw new CollegueInvalideException();
                }
            }
            catch (CollegueNonTrouveException)
            {
                return NotFound();
            }
            catch (CollegueInvalideException)
            {
                return BadRequest();
            }
            catch (ProblemeTechniqueException)
            {
                return BadRequest();
            }
        }
    }
}