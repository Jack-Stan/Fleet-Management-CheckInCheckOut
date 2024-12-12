using Xunit;
using Moq;
using BL.Interfaces;
using BL.Models;
using BL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BestuurderServiceTests
{
    private readonly Mock<IBestuurderRepository> _mockRepository;
    private readonly BestuurderService _service;

    public BestuurderServiceTests()
    {
        _mockRepository = new Mock<IBestuurderRepository>();
        _service = new BestuurderService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllBestuurdersAsync_ShouldReturnListOfBestuurders()
    {
        var mockData = new List<Bestuurder>
        {
            new Bestuurder { Naam = "naam",
            Geboortedatum = new DateTime(2024, 12, 10),
            RijbewijsNummer = "rijbewijsNummer",
            RijbewijsType = "rijbewijsType",
            Bedrijfsnaam = "bedrijfsnaam",
            BedrijfsBTW = "bedrijfsBTW"
            },
            new Bestuurder { Naam = "naam2",
            Geboortedatum = new DateTime(2024, 12, 11),
            RijbewijsNummer = "rijbewijsNummer2",
            RijbewijsType = "rijbewijsType2",
            Bedrijfsnaam = "bedrijfsnaam2",
            BedrijfsBTW = "bedrijfsBTW2"
            }
        };
        _mockRepository.Setup(repo => repo.GetAllBestuurdersAsync()).ReturnsAsync(mockData);
        var result = await _service.GetAllBestuurdersAsync();

        Assert.Equal(2, ((List<Bestuurder>)result).Count);
        _mockRepository.Verify(repo => repo.GetAllBestuurdersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetBestuurderAsync_ShouldReturnBestuurder_WhenIdExists()
    {
        var mockBestuurder = new Bestuurder {
            Naam = "naam",
            Geboortedatum = new DateTime(2024, 12, 10),
            RijbewijsNummer = "rijbewijsNummer",
            RijbewijsType = "rijbewijsType",
            Bedrijfsnaam = "bedrijfsnaam",
            BedrijfsBTW = "bedrijfsBTW"
        };
        _mockRepository.Setup(repo => repo.GetBestuurderByIdAsync(1)).ReturnsAsync(mockBestuurder);
        var result = await _service.GetBestuurderAsync(1);

        Assert.NotNull(result);
        Assert.Equal("naam", result.Naam);
        Assert.Equal(new DateTime(2024, 12, 10), result.Geboortedatum);
        Assert.Equal("rijbewijsNummer", result.RijbewijsNummer);
        Assert.Equal("rijbewijsType", result.RijbewijsType);
        Assert.Equal("bedrijfsnaam", result.Bedrijfsnaam);
        Assert.Equal("bedrijfsBTW", result.BedrijfsBTW);
        _mockRepository.Verify(repo => repo.GetBestuurderByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetBestuurderAsync_ShouldReturnNull_WhenIdJerrysNotExist()
    {
        _mockRepository.Setup(repo => repo.GetBestuurderByIdAsync(1)).ReturnsAsync((Bestuurder)null);
        var result = await _service.GetBestuurderAsync(1);

        Assert.Null(result);
        _mockRepository.Verify(repo => repo.GetBestuurderByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task AddBestuurderAsync_ShouldCallAddBestuurderOnRepository()
    {
        var newBestuurder = new Bestuurder {
            Naam = "naam3",
            Geboortedatum = new DateTime(2024, 12, 12),
            RijbewijsNummer = "rijbewijsNummer3",
            RijbewijsType = "rijbewijsType3",
            Bedrijfsnaam = "bedrijfsnaam3",
            BedrijfsBTW = "bedrijfsBTW3"
        };
        await _service.AddBestuurderAsync(newBestuurder);

        _mockRepository.Verify(repo => repo.AddBestuurderAsync(newBestuurder), Times.Once);
    }

    [Fact]
    public async Task UpdateBestuurderAsync_ShouldCallUpdateBestuurderOnRepository()
    {
        var updatedBestuurder = new Bestuurder {
            Naam = "naam",
            Geboortedatum = new DateTime(2024, 12, 10),
            RijbewijsNummer = "rijbewijsNummer",
            RijbewijsType = "rijbewijsType",
            Bedrijfsnaam = "bedrijfsnaam",
            BedrijfsBTW = "bedrijfsBTW"
        };
        await _service.UpdateBestuurderAsync(updatedBestuurder);

        _mockRepository.Verify(repo => repo.UpdateBestuurderAsync(updatedBestuurder), Times.Once);
    }

    [Fact]
    public async Task DeleteBestuurderAsync_ShouldCallDeleteBestuurderOnRepository()
    {
        var idToDelete = 1;
        await _service.DeleteBestuurderAsync(idToDelete);

        _mockRepository.Verify(repo => repo.DeleteBestuurderAsync(idToDelete), Times.Once);
    }
}
