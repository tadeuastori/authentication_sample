using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TRSB.Web.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {
        public void OnGet() { }
    }
}
