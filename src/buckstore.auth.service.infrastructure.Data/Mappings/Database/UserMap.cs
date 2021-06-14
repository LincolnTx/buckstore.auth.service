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
			
			// descobrir pq usar index faz o campo retornar fazio
			/*builder.HasIndex("_email").IsUnique();
			builder.HasIndex("_cpf").IsUnique();*/

			builder.HasKey(user => user.Id);

			builder.Property<string>(user => user.Name)
				.HasField("_name")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("name")
				.IsRequired();
			
			builder.Property<string>(user => user.Surname)
				.HasField("_surname")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("surname")
				.IsRequired();
			
			builder.Property<string>(user => user.Email)
				.HasField("_email")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("email")
				.IsRequired();

			builder.Property<string>("_password")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("password");
			
			builder.Property<string>(user => user.CredCard)
				.HasField("_credCard")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("credCard");

			builder.Property<string>(user => user.Cpf)
				.HasField("_cpf")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("cpf");

			builder.Property<byte[]>("_passwordSalt")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("passwordSalt");

			builder.Property<int>(user => user.UserType)
				.HasField("_userType")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("userType")
				.IsRequired();

			builder.OwnsOne<Address>(user => user.Address, userAddress =>
			{
				userAddress.WithOwner();
			});
		}
	}
}