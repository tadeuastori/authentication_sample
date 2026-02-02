using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TRSB.Application.Dtos;
using TRSB.Application.Features.Users.Register;

public class RegisterModel : PageModel
{
    private readonly IMediator _mediator;

    public RegisterModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [BindProperty]
    public RegisterUserDto Model { get; set; } = new();

    public string? Error { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            await _mediator.Send(
                new RegisterUserCommand(
                    Model.UserName,
                    Model.Name,
                    Model.Email,
                    Model.Password));

            return RedirectToPage("/Login");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return Page();
        }
    }
}
