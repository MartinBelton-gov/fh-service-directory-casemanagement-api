using AutoFixture;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.UnitTests;

namespace FamilyHubs.ServiceDirectoryCaseManagement.InfraTests.Persistence.ReferralEntites;

public class WhenEfRepositoryDelete : BaseEfRepositoryTestFixture
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task ThenDeletesReferralAfterAddingIt()
    {
        // Arrange
        var referralItem = _fixture.Create<Referral>();
        ArgumentNullException.ThrowIfNull(referralItem, nameof(referralItem));
        var referralId = referralItem.Id;
        var repository = GetRepository<Referral>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        await repository.AddAsync(referralItem);

        // Act
        await repository.DeleteAsync(referralItem);

        // Assert
        Assert.DoesNotContain(await repository.ListAsync(),
            newOpenReferralOrganisation => newOpenReferralOrganisation.Id == referralId);
    }
}
