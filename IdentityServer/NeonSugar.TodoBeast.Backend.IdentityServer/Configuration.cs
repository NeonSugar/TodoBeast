using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.IdentityServer
{
	internal class Configuration
	{
		private const string _name = "TodoBeastWebApi";
		private const string _displayName = "Web API";

		internal static IEnumerable<ApiScope> ApiScopes =>
		new List<ApiScope>()
		{
			new ApiScope(_name, _displayName),
			new ApiScope("test", "Test")
		};

		internal static IEnumerable<IdentityResource> IdentityResources =>
		new List<IdentityResource>()
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile()
		};

		internal static IEnumerable<ApiResource> ApiResources =>
		new List<ApiResource>()
		{
			new ApiResource(_name, _displayName, 
							new [] { JwtClaimTypes.Name })
			{
				Scopes = { _name }
			}
		};

		internal static IEnumerable<Client> Clients =>
		new List<Client>()
		{
			new Client()
			{
				ClientId            = "todolist-web-api",
				ClientName          = "TodoList Web",
				AllowedGrantTypes   = GrantTypes.Code,
				RequireClientSecret = false,
				RequirePkce         = true,

				RedirectUris =
				{
					"http://localhost:7155/signin-oidc"
				},
				AllowedCorsOrigins =
				{
					"http://localhost:7155"
				},
				PostLogoutRedirectUris =
				{
					"http://localhost:7155/signout-oidc"
				},
				AllowedScopes =
				{
					_name,
					IdentityServerConstants.StandardScopes.OpenId,
					IdentityServerConstants.StandardScopes.Profile
				},

				AllowAccessTokensViaBrowser = true
			},
			new Client()
			{
				ClientId            = "test",
				ClientName          = "test",
				AllowedGrantTypes   = GrantTypes.ClientCredentials,
				RequireClientSecret = false,
				RequirePkce         = false,

				RedirectUris =
				{
					"http://localhost:7155/signin-oidc"
				},
				AllowedCorsOrigins =
				{
					"http://localhost:7155"
				},
				PostLogoutRedirectUris =
				{
					"http://localhost:7155/signout-oidc"
				},
				AllowedScopes =
				{
					"test",
					IdentityServerConstants.StandardScopes.Profile
				},

				AllowAccessTokensViaBrowser = true
			}
		};
	}
}
