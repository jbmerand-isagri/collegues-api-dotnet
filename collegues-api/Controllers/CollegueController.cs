using System;
using System.Collections.Generic;
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

        // GET: collegues?nom=xxx
        [HttpGet]
        public IEnumerable<string> GetMatriculeFromName(string nom)
        {
            return this._collegueService.RechercherParNom(nom);
        }

        // GET collegues/xxx
        [HttpGet("{matricule}")]
        public IActionResult GetCollegueFromMatricule(string matricule)
        {
            try
            { 
                return Ok(_collegueService.RechercherParMatricule(matricule));
            }
            catch(CollegueInvalideException) 
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
                return Ok(_collegueService.AjouterUnCollegue(collegueDto));
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
