using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TRSB.Application.Dtos;
using MediatR;

namespace TRSB.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IMediator _mediator;

        public LoginModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public LoginDto Input { get; set; } = new();

        public string? Error { get; set; }

        public async Task<IActionResult> OnPostLogin()
        {
            try
            {
                var user = await _mediator.Send(new LoginUserQuery(Input.LoginValue, Input.Password));

                if (user == null)
                {
                    Error = "Invalid credentials";
                    return Page();
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return RedirectToPage("/Secure");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return Page();
            }
        }
    }
}
