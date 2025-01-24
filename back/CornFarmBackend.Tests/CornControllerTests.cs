using CornFarmBackend.Controllers;
using CornFarmBackend.Data;
using CornFarmBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class CornControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CornController _controller;

    public CornControllerTests()
    {
        // Use an in-memory database for testing
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _controller = new CornController(_context);
    }

    [Fact]
    public void BuyCorn_ReturnsBadRequest_WhenUsernameIsMissing()
    {
        // Arrange
        var request = new CornRequest { Username = "" };

        // Act
        var result =  _controller.BuyCorn(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Username is required.", badRequestResult.Value);
    }

    [Fact]
    public void BuyCorn_CreatesUser_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new CornRequest { Username = "newuser" };

        // Act
        var result =  _controller.BuyCorn(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        CornResponse returnValue = okResult.Value as dynamic;
        Assert.Equal("200 🌽", returnValue.message);
        Assert.Equal(1, returnValue.cornCount);
    }

    [Fact]
    public void BuyCorn_Returns429_WhenRequestIsTooSoon()
    {
        // Arrange
        var username = "user1";
        var user = new User { Username = username, CornBought = 0, LastTimeCornBought = DateTime.UtcNow };
        _context.Users.Add(user);
        _context.SaveChanges();
        var request = new CornRequest { Username = username };

        // Act
        var result =  _controller.BuyCorn(request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(429, objectResult.StatusCode);
        Assert.Equal("Too many requests. Wait a minute before buying more corn.", objectResult.Value);
    }


}
