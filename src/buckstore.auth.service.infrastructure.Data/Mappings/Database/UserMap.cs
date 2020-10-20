using buckstore.auth.service.domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.auth.service.infrastructure.Data.Mappings.Database
{
	public class UserMap : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");
			
			builder.HasIndex("_email").IsUnique();

			builder.HasKey(user => user.Id);

			builder.Property<string>("_name")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("name")
				.IsRequired();
			
			builder.Property<string>("_surname")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("surname")
				.IsRequired();
			
			builder.Property<string>("_email")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("email")
				.IsRequired();
			
			builder.Property<string>("_password")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("password")
				.IsRequired();
			
			builder.Property<string>("_credCard")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("credCard");
			
			builder.Property<string>("_cpf")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("cpf")
				.IsRequired();
			
			
			builder.Property<byte[]>("_passwordSalt")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("passwordSalt")
				.IsRequired();
			
			builder.OwnsOne<Address>(user => user.Address, userAddress =>
			{
				userAddress.WithOwner();
			});
		}
	}
}