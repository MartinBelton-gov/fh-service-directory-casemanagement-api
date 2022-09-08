using AutoFixture;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.UnitTests;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectoryCaseManagement.InfraTests.Persistence.ReferralEntites;

public class WhenEfRepositoryUpdate : BaseEfRepositoryTestFixture
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task ThenUpdatesOpenReferralOrganisationAfterAddingIt()
    {
        // Arrange
        var referralItem = _fixture.Create<Referral>();
        ArgumentNullException.ThrowIfNull(referralItem, nameof(referralItem));

        var repository = GetRepository<Referral>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        await repository.AddAsync(referralItem);

        DbContext.Entry(referralItem).State = EntityState.Detached;             // detach the item so we get a different instance

        var addedReferral = await repository.GetByIdAsync(referralItem.Id); // fetch the OpenReferralOrganisation and update its name
        if (addedReferral == null)
        {
            Assert.NotNull(addedReferral);
            return;
        }

        Assert.NotSame(referralItem, addedReferral);

        // Act
        addedReferral.ServiceName = "Brum Council";
        await repository.UpdateAsync(addedReferral);
        var updatedOpenReferralOrganisation = await repository.GetByIdAsync(addedReferral.Id);

        // Assert
        Assert.NotNull(updatedOpenReferralOrganisation);
        Assert.NotEqual(referralItem.ServiceName, updatedOpenReferralOrganisation?.ServiceName);
        Assert.Equal(referralItem.Id, updatedOpenReferralOrganisation?.Id);
    }
}
