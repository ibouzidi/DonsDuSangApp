using DonsDuSangApp.Context.Models;
using System;
using Xunit;
using static DonsDuSangApp.Context.Models.Donneur;

public class DonneurServiceTests
{
    private readonly DonneurService _donneurService;

    public DonneurServiceTests()
    {
        _donneurService = new DonneurService();
    }

    [Fact]
    public void EstMajeur_DonneurA18Ans_RetourneVrai()
    {
        // Arrange
        var donneur = new Donneur
        {
            DateNaissance = DateOnly.FromDateTime(DateTime.Today.AddYears(-18))
        };

        // Act
        var result = _donneurService.EstMajeur(donneur);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void EstMajeur_DonneurA17Ans_RetourneFaux()
    {
        // Arrange
        var donneur = new Donneur
        {
            DateNaissance = DateOnly.FromDateTime(DateTime.Today.AddYears(-17).AddDays(-1))
        };

        // Act
        var result = _donneurService.EstMajeur(donneur);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EstMajeur_DateNaissanceNull_RetourneFaux()
    {
        // Arrange
        var donneur = new Donneur
        {
            DateNaissance = null
        };

        // Act
        var result = _donneurService.EstMajeur(donneur);

        // Assert
        Assert.False(result);
    }
}
