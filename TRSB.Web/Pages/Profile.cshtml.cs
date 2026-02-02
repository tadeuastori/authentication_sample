using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TRSB.Application.Dtos;
using TRSB.Application.Features.Users.GetProfile;

namespace TRSB.Web.Pages;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly IMediator _mediator;

    public ProfileModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [BindProperty]
    public UpdateProfileDto Model { get; set; } = new();

    public bool IsEditMode { get; set; }
    public string? Error { get; set; }
    public string? Success { get; set; }

    public async Task OnGetAsync()
    {
        IsEditMode = false;
        await LoadProfileAsync();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var userId = GetUserId();

            await _mediator.Send(new UpdateProfileCommand(
                userId,
                Model.UserName,
                Model.Email
            ));

            Success = "Profile has been updated";
            IsEditMode = false;

            await LoadProfileAsync(); 
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            IsEditMode = true;
        }

        return Page();
    }


    public async Task<IActionResult> OnPostEnableEdit()
    {
        IsEditMode = true;
        Success = string.Empty;
        Error = string.Empty;

        await LoadProfileAsync(); 

        return Page();
    }


    public async Task<IActionResult> OnPostCancelEdit()
    {
        IsEditMode = false;
        Success = string.Empty;
        Error = string.Empty;

        await LoadProfileAsync(); 

        return Page();
    }


    private Guid GetUserId()
    {
        return Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }

    private async Task LoadProfileAsync()
    {
        var userId = GetUserId();

        var profile = await _mediator.Send(new GetProfileQuery(userId));

        Model = new UpdateProfileDto
        {
            UserName = profile.UserName,
            Name = profile.Name,
            Email = profile.Email,
            Password = string.Empty
        };
    }

}
