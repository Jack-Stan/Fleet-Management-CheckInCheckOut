using Xunit;
using Moq;
using BL.Services;
using BL.Interfaces;
using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReserveringServiceTests
{
    private readonly Mock<IReserveringRepository> _mockRepo;
    private readonly ReserveringService _service;

    public ReserveringServiceTests()
    {
        _mockRepo = new Mock<IReserveringRepository>();
        _service = new ReserveringService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllReserveringenAsync_ReturnsListOfReserveringen()
    {
        var mockData = new List<Reservering>
        {
            new Reservering { StartDatum = new DateTime(2024, 12, 10),
            EindDatum = new DateTime(2024, 12, 10),
            VoertuigId = 1,
            BestuurderId = 1,
            CheckOutState = "checkOutState",
            CheckInState = "checkInState"
            },
            new Reservering { StartDatum = new DateTime(2024, 12, 11),
            EindDatum = new DateTime(2024, 12, 11),
            VoertuigId = 2,
            BestuurderId = 2,
            CheckOutState = "checkOutState2",
            CheckInState = "checkInState2"
            }
        };
        _mockRepo.Setup(repo => repo.GetAllReserveringenAsync()).ReturnsAsync(mockData);
        var result = await _service.GetAllReserveringenAsync();

        Assert.NotNull(result);
        Assert.Equal(2, ((List<Reservering>)result).Count);
        _mockRepo.Verify(repo => repo.GetAllReserveringenAsync(), Times.Once);
    }

    [Fact]
    public async Task GetReserveringByIdAsync_ReturnsReservering_WhenIdExists()
    {
        var mockReservering = new Reservering {
            StartDatum = new DateTime(2024, 12, 10),
            EindDatum = new DateTime(2024, 12, 10),
            VoertuigId = 1,
            BestuurderId = 1,
            CheckOutState = "checkOutState",
            CheckInState = "checkInState"
        };
        _mockRepo.Setup(repo => repo.GetReserveringByIdAsync(1)).ReturnsAsync(mockReservering);
        var result = await _service.GetReserveringByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(new DateTime(2024, 12, 10), result.StartDatum);
        Assert.Equal(new DateTime(2024, 12, 10), result.EindDatum);
        Assert.Equal(1, result.VoertuigId);
        Assert.Equal(1, result.BestuurderId);
        Assert.Equal("checkOutState", result.CheckOutState);
        Assert.Equal("checkInState", result.CheckInState);
        _mockRepo.Verify(repo => repo.GetReserveringByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetReserveringByIdAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetReserveringByIdAsync(99)).ReturnsAsync((Reservering)null);
        var result = await _service.GetReserveringByIdAsync(99);

        Assert.Null(result);
        _mockRepo.Verify(repo => repo.GetReserveringByIdAsync(99), Times.Once);
    }

    [Fact]
    public async Task AddReserveringAsync_CallsRepositoryMethod()
    {
        var newReservering = new Reservering {
            StartDatum = new DateTime(2024, 12, 10),
            EindDatum = new DateTime(2024, 12, 10),
            VoertuigId = 1,
            BestuurderId = 1,
            CheckOutState = "checkOutState",
            CheckInState = "checkInState"
        };
        _mockRepo.Setup(repo => repo.AddReserveringAsync(newReservering)).Returns(Task.CompletedTask);
        await _service.AddReserveringAsync(newReservering);

        _mockRepo.Verify(repo => repo.AddReserveringAsync(newReservering), Times.Once);
    }

    [Fact]
    public async Task UpdateReserveringAsync_CallsRepositoryMethod()
    {
        var updatedReservering = new Reservering {
            StartDatum = new DateTime(2024, 12, 13),
            EindDatum = new DateTime(2024, 12, 13),
            VoertuigId = 3,
            BestuurderId = 3,
            CheckOutState = "checkOutState3",
            CheckInState = "checkInState3"
        };
        _mockRepo.Setup(repo => repo.UpdateReserveringAsync(updatedReservering)).Returns(Task.CompletedTask);
        await _service.UpdateReserveringAsync(updatedReservering);

        _mockRepo.Verify(repo => repo.UpdateReserveringAsync(updatedReservering), Times.Once);
    }

    [Fact]
    public async Task DeleteReserveringAsync_CallsRepositoryMethod()
    {
        _mockRepo.Setup(repo => repo.DeleteReserveringAsync(1)).Returns(Task.CompletedTask);
        await _service.DeleteReserveringAsync(1);

        _mockRepo.Verify(repo => repo.DeleteReserveringAsync(1), Times.Once);
    }
}
