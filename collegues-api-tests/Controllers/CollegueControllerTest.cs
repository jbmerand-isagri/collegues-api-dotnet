using collegues_api.Controllers;
using collegues_api.Models;
using collegues_api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace collegues_api_tests.Controllers
{
    [TestClass]
    public class CollegueControllerTest
    {
        [TestMethod]
        public void PostNewCollegue_WithAValidColleagueDto_ShouldReturnOkResult()
        {
            // Arrange
            var collegueService = Mock.Of<ICollegueService>();
            var collegueController = new CollegueController(collegueService);
            var collegueDto = GetAValidColleguePostDto();
            var collegue = GetAValidCollegue();
            Mock.Get(collegueService).Setup(c => c.AjouterUnCollegue(collegueDto)).Returns(collegue);

            // Act
            var result = collegueController.PostNewCollegue(collegueDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public void PostNewCollegue_WithAColleagueDtoWithAnInvalidNameOfOneCharacter_ShouldReturnBadRequestResult()
        {
            // Arrange
            var collegueService = Mock.Of<ICollegueService>();
            var collegueController = new CollegueController(collegueService);
            var collegueDto = GetAValidColleguePostDto();
            collegueDto.Nom = "o";
            var collegue = GetAValidCollegue();
            Mock.Get(collegueService).Setup(c => c.AjouterUnCollegue(collegueDto)).Returns(collegue);

            // Act
            var result = collegueController.PostNewCollegue(collegueDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        public ColleguePostDto GetAValidColleguePostDto()
        {
            return new ColleguePostDto("Doe", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), "https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg");
        }

        public Collegue GetAValidCollegue()
        {
            return new Collegue(Guid.NewGuid().ToString(), "Doe", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), "https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg");
        }
    }
}