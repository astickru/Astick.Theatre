using Astick.Theatre.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Astick.Theatre.Pages.Account {
	public class ConfirmEmailModel : PageModel {
		private readonly UserManager<Cl_User> _userManager;

		public ConfirmEmailModel(UserManager<Cl_User> userManager) {
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync(string userId, string code) {
			if (userId == null || code == null) {
				return RedirectToPage("/Index");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) {
				throw new ApplicationException($"Unable to load user with ID '{userId}'.");
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (!result.Succeeded) {
				throw new ApplicationException($"Error confirming email for user with ID '{userId}':");
			}

			return Page();
		}
	}
}