using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeonSugar.TodoBeast.Backend.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.IdentityServer.Contexts
{
	public class AuthDbContext : IdentityDbContext<IdentityUser>
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options)
		: base(options) 
		{
			//empty
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityUser>(entity => entity.ToTable(name: "Users"));
			builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
			builder.Entity<IdentityUserRole<string>>(entity =>
				entity.ToTable(name: "UserRoles"));
			builder.Entity<IdentityUserClaim<string>>(entity =>
				entity.ToTable(name: "UserClaim"));
			builder.Entity<IdentityUserLogin<string>>(entity =>
				entity.ToTable("UserLogins"));
			builder.Entity<IdentityUserToken<string>>(entity =>
				entity.ToTable("UserTokens"));
			builder.Entity<IdentityRoleClaim<string>>(entity =>
				entity.ToTable("RoleClaims"));

			//AutoMap(builder,
			//new List<Type>()
			//{ 
			//	typeof(IdentityUser),
			//	typeof(IdentityRole),
			//	typeof(IdentityUserRole<string>),
			//	typeof(IdentityUserClaim<string>),
			//	typeof(IdentityUserLogin<string>),
			//	typeof(IdentityUserToken<string>),
			//	typeof(IdentityRoleClaim<string>)
			//});
		}

		private void AutoMap(ModelBuilder builder, List<Type> types)
		{
			foreach (var type in types)
			{
				string tableName = (type.Name).Replace("Identity", "").Replace("<string>", "") + "s";
				builder.Entity(type, entity => entity.ToTable(tableName));
			}
		}
	}
}
