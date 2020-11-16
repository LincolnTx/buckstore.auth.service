using buckstore.auth.service.domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace buckstore.auth.service.infrastructure.Data.Mappings.Database
{
    public class UserRefreshTokenMap : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshToken");

            builder.HasKey(authorization => authorization.Id);

            builder.Property<string>("_refreshToken")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("userRefreshToken");
        }
    }
}
