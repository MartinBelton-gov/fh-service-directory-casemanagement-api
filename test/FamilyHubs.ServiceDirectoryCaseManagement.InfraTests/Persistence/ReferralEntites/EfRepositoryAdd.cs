using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.UnitTests;

namespace FamilyHubs.ServiceDirectoryCaseManagement.InfraTests.Persistence.ReferralEntites;

public class WhenEfRepositoryAdd : BaseEfRepositoryTestFixture
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public async Task ThenAddsOrOpensReferral()
    {
        // Arrange
        var referralItem = _fixture.Create<Referral>();     
        ArgumentNullException.ThrowIfNull(referralItem, nameof(referralItem));

        var repository = GetRepository<Referral>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        // Act
        await repository.AddAsync(referralItem);

        var addedReferralItem = await repository.GetByIdAsync(referralItem.Id);
        ArgumentNullException.ThrowIfNull(addedReferralItem, nameof(addedReferralItem));

        await repository.SaveChangesAsync();

        // Assert
        Assert.Equal(referralItem, addedReferralItem);
        Assert.True(!string.IsNullOrEmpty(addedReferralItem.Id));
    }
    
}
