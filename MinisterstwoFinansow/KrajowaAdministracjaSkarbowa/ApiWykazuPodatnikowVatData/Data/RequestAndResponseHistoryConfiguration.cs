using ApiWykazuPodatnikowVatData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWykazuPodatnikowVatData.Data
{
    #region internal class RequestAndResponseHistoryConfiguration : IEntityTypeConfiguration<RequestAndResponseHistory>
    /// <summary>
    /// Klasa konfiguracji dodatkowych ustawień dla modelu encji RequestAndResponseHistory
    /// Additional settings configuration class for RequestAndResponseHistory entity model
    /// </summary>
    internal class RequestAndResponseHistoryConfiguration : IEntityTypeConfiguration<RequestAndResponseHistory>
    {
        /// <summary>
        /// Konfiguruj
        /// Configure
        /// </summary>
        /// <param name="entity">
        /// Kreator typów jednostek jako EntityTypeBuilder dla modelu RequestAndResponseHistory
        /// Entity type wizard as EntityTypeBuilder for the RequestAndResponseHistory model
        /// </param>
        public void Configure(EntityTypeBuilder<RequestAndResponseHistory> entity)
        {
            entity.HasIndex(e => e.Id)
                .HasDatabaseName("IX_RequestAndResponseHistoryId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasDatabaseName("IX_RequestAndResponseHistoryUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestUrl)
                .HasDatabaseName("IX_RequestAndResponseHistoryRequestUrl")
                .IsUnique(false);

            entity.HasIndex(e => e.ResponseStatusCode)
                .HasDatabaseName("IX_RequestAndResponseHistoryResponseStatusCode")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestId)
                .HasDatabaseName("IX_RequestAndResponseHistoryRequestId")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestDateTime)
                .HasDatabaseName("IX_RequestAndResponseHistoryRequestDateTime")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestDateTimeAsDateTime)
                .HasDatabaseName("IX_RequestAndResponseHistoryRequestDateTimeAsDateTime")
                .IsUnique(false);

            entity.HasIndex(e => e.ObjectMD5Hash)
                .HasDatabaseName("IX_RequestAndResponseHistoryRequestObjectMD5Hash")
                .IsUnique(true);

            entity.HasIndex(e => e.DateOfCreate)
                .HasDatabaseName("IX_RequestAndResponseHistoryDateOfCreate")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfModification)
                .HasDatabaseName("IX_RequestAndResponseHistoryDateOfModification")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
