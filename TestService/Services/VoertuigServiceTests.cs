using Xunit;
using Moq;
using BL.Services;
using BL.Interfaces;
using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

public class VoertuigServiceTests
{
    private readonly Mock<IVoertuigRepository> _mockRepo;
    private readonly VoertuigService _service;

    public VoertuigServiceTests()
    {
        _mockRepo = new Mock<IVoertuigRepository>();
        _service = new VoertuigService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllVoertuigenAsync_ReturnsListOfVoertuigen()
    {
        var mockData = new List<Voertuig>
        {
            new Voertuig { Merk = "merk",
            Model = "model",
            ChasisNummer = "chasisNummer",
            Kenteken = "kenteken",
            VoertuigType = "voertuigType",
            BrandstofType = "brandstofType",
            Kleur = "kleur",
            AantalZitplaatsen = 5,
            AantalDeuren = 5
            },
            new Voertuig { Merk = "merk2",
            Model = "model2",
            ChasisNummer = "chasisNummer2",
            Kenteken = "kenteken2",
            VoertuigType = "voertuigType2",
            BrandstofType = "brandstofType2",
            Kleur = "kleur2",
            AantalZitplaatsen = 5,
            AantalDeuren = 5 }
        };
        _mockRepo.Setup(repo => repo.GetAllVoertuigenAsync()).ReturnsAsync(mockData);
        var result = await _service.GetAllVoertuigenAsync();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<Voertuig>)result).Count);
        _mockRepo.Verify(repo => repo.GetAllVoertuigenAsync(), Times.Once);
    }

    [Fact]
    public async Task GetVoertuigAsync_ReturnsVoertuig_WhenIdExists()
    {
        var mockVoertuig = new Voertuig {
            Merk = "merk",
            Model = "model",
            ChasisNummer = "chasisNummer",
            Kenteken = "kenteken",
            VoertuigType = "voertuigType",
            BrandstofType = "brandstofType",
            Kleur = "kleur",
            AantalZitplaatsen = 5,
            AantalDeuren = 5
        };
        _mockRepo.Setup(repo => repo.GetVoertuigByIdAsync(1)).ReturnsAsync(mockVoertuig);
        var result = await _service.GetVoertuigAsync(1);

        Assert.NotNull(result);
        Assert.Equal("merk", result.Merk);
        Assert.Equal("model", result.Model);
        Assert.Equal("chasisNummer", result.ChasisNummer);
        Assert.Equal("kenteken", result.Kenteken);
        Assert.Equal("voertuigType", result.VoertuigType);
        Assert.Equal("brandstofType", result.BrandstofType);
        Assert.Equal("kleur", result.Kleur);
        Assert.Equal(5, result.AantalZitplaatsen);
        Assert.Equal(5, result.AantalDeuren);
        _mockRepo.Verify(repo => repo.GetVoertuigByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetVoertuigAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetVoertuigByIdAsync(99)).ReturnsAsync((Voertuig)null);
        var result = await _service.GetVoertuigAsync(99);

        Assert.Null(result);
        _mockRepo.Verify(repo => repo.GetVoertuigByIdAsync(99), Times.Once);
    }

    [Fact]
    public async Task AddVoertuigAsync_CallsRepositoryMethod()
    {
        var newVoertuig = new Voertuig {
            Merk = "merk3",
            Model = "model3",
            ChasisNummer = "chasisNummer3",
            Kenteken = "kenteken3",
            VoertuigType = "voertuigType3",
            BrandstofType = "brandstofType3",
            Kleur = "kleur3",
            AantalZitplaatsen = 5,
            AantalDeuren = 5
        };
        _mockRepo.Setup(repo => repo.AddVoertuigAsync(newVoertuig)).Returns(Task.CompletedTask);
        await _service.AddVoertuigAsync(newVoertuig);

        _mockRepo.Verify(repo => repo.AddVoertuigAsync(newVoertuig), Times.Once);
    }

    [Fact]
    public async Task UpdateVoertuigen_CallsRepositoryMethod()
    {
        var updatedVoertuig = new Voertuig {
            Merk = "merk",
            Model = "model",
            ChasisNummer = "chasisNummer",
            Kenteken = "kenteken",
            VoertuigType = "voertuigType",
            BrandstofType = "brandstofType",
            Kleur = "kleur",
            AantalZitplaatsen = 5,
            AantalDeuren = 5
        };
        _mockRepo.Setup(repo => repo.UpdateVoertuigAsync(updatedVoertuig)).Returns(Task.CompletedTask);
        await _service.UpdateVoertuigen(updatedVoertuig);

        _mockRepo.Verify(repo => repo.UpdateVoertuigAsync(updatedVoertuig), Times.Once);
    }

    [Fact]
    public async Task DeleteVoertuigen_CallsRepositoryMethod()
    {
        _mockRepo.Setup(repo => repo.DeleteVoertuigAsync(1)).Returns(Task.CompletedTask);
        await _service.DeleteVoertuigen(1);

        _mockRepo.Verify(repo => repo.DeleteVoertuigAsync(1), Times.Once);
    }
}
