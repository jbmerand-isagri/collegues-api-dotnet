using AutoMapper;
using ColleguesApi.Models;
using ColleguesApi.Repositories;
using ColleguesApi.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ColleguesApiTests.Services
{
    [TestClass]
    public class CollegueServiceTest
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<ColleguePostDto, Collegue>())
                .CreateMapper();
        }

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<TestMethod>")]
        public void RechercherParMatricule_WithOneColleagueWithThisMatricule_ShouldReturnOneColleague()
        {
            // Arrange
            var collegue = new Collegue(Guid.NewGuid().ToString(), "Dupuis", "Jean", "jean.dupuis@mail.com", new DateTime(1980, 1, 18), new Uri("https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"));

            var collegueRepositoryMock = Mock.Of<ICollegueRepository>();

            Mock.Get(collegueRepositoryMock)
                .Setup(c => c.FindColleagueByMatriculeAsync(collegue.Matricule))
                .Returns(Task.FromResult(collegue));

            var collegueService = new CollegueService(_mapper, collegueRepositoryMock);

            // Act
            var result = collegueService.RechercherParMatricule(collegue.Matricule);

            // Assert
            result.Result.Should().Be(collegue);
        }

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<TestMethod>")]
        public void RechercherParNom_WithTwoColleaguesWithThisName_ShouldReturnTwoMatricules()
        {
            // Arrange
            var collegue1 = new Collegue(Guid.NewGuid().ToString(), "Dupuis", "Jean", "jean.dupuis@mail.com", new DateTime(1980, 1, 18), new Uri("https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"));

            var collegue2 = new Collegue(Guid.NewGuid().ToString(), "Durand", "Bernard", "bernard.durand@mail.com", new DateTime(1982, 11, 23), new Uri("https://secure.i.telegraph.co.uk/multimedia/archive/03127/terry_crews_3127762b.jpg"));

            var collegue3 = new Collegue(Guid.NewGuid().ToString(), "Dupuis", "John", "john.doe@mail.com", new DateTime(1984, 8, 2), new Uri("https://secure.i.telegraph.co.uk/multimedia/archive/03029/Becks1_5_3029072b.jpg"));

            var collegueRepositoryMock = Mock.Of<ICollegueRepository>();

            Mock.Get(collegueRepositoryMock)
                .Setup(c => c.GetColleagueMatriculesByNomAsync("Dupuis"))
                .Returns(Task.FromResult((IEnumerable<string>)new List<string> { collegue1.Matricule, collegue3.Matricule }));

            var collegueService = new CollegueService(_mapper, collegueRepositoryMock);

            // Act
            var result = collegueService.RechercherParNom("Dupuis");

            // Assert
            result.Should().Equals(new List<string> { collegue1.Matricule, collegue3.Matricule });
        }

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<TestMethod>")]
        public void AjouterUnCollegue_WhenColleagueInfosAreValid_ShouldNotThrowException()
        {
            // Arrange
            var collegueDto = new ColleguePostDto("Dupuis", "Jean", "jean.dupuis@mail.com", new DateTime(1980, 1, 18), new Uri("https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"));

            var collegue = new Collegue(null, "Dupuis", "Jean", "jean.dupuis@mail.com", new DateTime(1980, 1, 18), new Uri("https://img.huffingtonpost.com/asset/5901e5881400002000a9c22f.jpeg?ops=scalefit_720_noupscale"));

            var collegueRepositoryMock = Mock.Of<ICollegueRepository>();

            var collegueServiceMock = Mock.Of<ICollegueService>();

            Mock.Get(collegueRepositoryMock)
                .Setup(c => c.SaveColleagueAsync(It.IsAny<Collegue>()))
                .Returns(Task.FromResult(1));

            Mock.Get(collegueServiceMock)
                .Setup(c => c.RechercherParMatricule(It.IsAny<string>()))
                .Returns(Task.FromResult(collegue));

            var collegueService = new CollegueService(_mapper, collegueRepositoryMock);

            // Act
            var result = collegueService.AjouterUnCollegue(collegueDto);

            // Assert (no exception)
        }
    }
}