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
                .HasName("IX_RequestAndResponseHistoryId")
                .IsUnique(true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())");

            entity.HasIndex(e => e.UniqueIdentifierOfTheLoggedInUser)
                .HasName("IX_RequestAndResponseHistoryUniqueIdentifierOfTheLoggedInUser")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestUrl)
                .HasName("IX_RequestAndResponseHistoryRequestUrl")
                .IsUnique(false);

            entity.HasIndex(e => e.ResponseStatusCode)
                .HasName("IX_RequestAndResponseHistoryResponseStatusCode")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestId)
                .HasName("IX_RequestAndResponseHistoryRequestId")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestDateTime)
                .HasName("IX_RequestAndResponseHistoryRequestDateTime")
                .IsUnique(false);

            entity.HasIndex(e => e.RequestDateTimeAsDateTime)
                .HasName("IX_RequestAndResponseHistoryRequestDateTimeAsDateTime")
                .IsUnique(false);

            entity.HasIndex(e => e.ObjectMD5Hash)
                .HasName("IX_RequestAndResponseHistoryRequestObjectMD5Hash")
                .IsUnique(true);

            entity.HasIndex(e => e.DateOfCreate)
                .HasName("IX_RequestAndResponseHistoryDateOfCreate")
                .IsUnique(false);

            entity.HasIndex(e => e.DateOfModification)
                .HasName("IX_RequestAndResponseHistoryDateOfModification")
                .IsUnique(false);

            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("(getdate())");
        }
    }
    #endregion
}
