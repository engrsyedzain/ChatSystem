using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatSystem.Pages
{
    public class InvitationModel : PageModel
    {
        public IEnumerable<Invitation> Invitations { get; set; }
        public void OnGet()
        {
        }
    }
}
