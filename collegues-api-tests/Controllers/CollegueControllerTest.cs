using ColleguesApi.Controllers;
using ColleguesApi.Models;
using ColleguesApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleguesApiTests.Controllers
{
    [TestClass]
    public class CollegueControllerTest
    {
        #region unit testing methods

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<TestMethod>")]
        public void PostNewCollegue_WithAValidColleagueDto_ShouldReturnOkResult()
        {
            // Arrange
            var collegueService = Mock.Of<ICollegueService>();

            using var collegueController = new CollegueController(collegueService);

            var collegueDto = GetAValidColleguePostDto();

            var collegue = GetAValidCollegue();

            Mock.Get(collegueService)
                .Setup(c => c.AjouterUnCollegue(collegueDto))
                .Returns(Task.FromResult(collegue));

            // Act
            var result = collegueController.PostNewCollegue(collegueDto);

            // Assert
            result.Should()
                .BeOfType<OkObjectResult>();
        }

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<TestMethod>")]
        public void PostNewCollegue_WithAColleagueDtoWithAnInvalidNameOfOneCharacter_ShouldReturnBadRequestResult()
        {
            // Arrange
            var collegueService = Mock.Of<ICollegueService>();

            using var collegueController = new CollegueController(collegueService);

            var collegueDto = GetAValidColleguePostDto();

            collegueDto.Nom = "o";

            var collegue = GetAValidCollegue();

            Mock.Get(collegueService)
                .Setup(c => c.AjouterUnCollegue(collegueDto))
                .Returns(Task.FromResult(collegue));

            // Act
            var result = collegueController.PostNewCollegue(collegueDto);

            // Assert
            result.Should()
                .BeOfType<BadRequestResult>();
        }

        #endregion unit testing methods

        #region non testing methods

        public static ColleguePostDto GetAValidColleguePostDto()
        {
            return new ColleguePostDto("Doe", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), new Uri("https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg"));
        }

        public static Collegue GetAValidCollegue()
        {
            return new Collegue(Guid.NewGuid().ToString(), "Doe", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), new Uri("https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg"));
        }

        #endregion non testing methods
    }
}