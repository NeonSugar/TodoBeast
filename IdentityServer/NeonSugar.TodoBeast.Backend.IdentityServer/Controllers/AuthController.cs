using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeonSugar.TodoBeast.Backend.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonSugar.TodoBeast.Backend.IdentityServer.Controllers
{
	[ApiController]
	[Route("{controller}/{action}")]
	public class AuthController : ControllerBase
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IIdentityServerInteractionService _interactionService;

		public AuthController(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			IIdentityServerInteractionService interactionService
		)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_interactionService = interactionService;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest view)
		{
			if (ModelState.IsValid is false)
			{
				return BadRequest();
			}

			var user = await _userManager.FindByNameAsync(view.UserName);
			if (user is null)
			{
				ModelState.AddModelError(string.Empty, "User not found!");
				return BadRequest();
			}

			var result = await _signInManager.PasswordSignInAsync(view.UserName, view.Password, false, false);
			if (result.Succeeded)
			{
				return Ok();
			}
			ModelState.AddModelError(string.Empty, "Login error!");
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterRequest view)
		{
			if (ModelState.IsValid is false)
			{
				return Problem();
			}

			var user = new IdentityUser
			{
				UserName = view.UserName,
			};

			var result = await _userManager.CreateAsync(user, view.Password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user,false);
				return Ok();
			}
			ModelState.AddModelError(string.Empty, "Register error!");
			return BadRequest();
		}

		[HttpGet]
		public async Task<IActionResult> Logout(string logoutId)
		{
			await _signInManager.SignOutAsync();
			var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
			return Ok();
		}
	}
}
