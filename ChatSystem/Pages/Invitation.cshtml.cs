using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.Pages
{
    public class InvitationModel : PageModel
    {
        private readonly AppDbContext _context;

        public InvitationModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invitation> Invitations { get; set; }
        public IEnumerable<Invitation> InvitationAccepted { get; set; }

        [BindProperty(SupportsGet = true)]
        public Member Member { get; set; }


        [BindProperty]
        public string Error { get; set; }

        public IActionResult OnPostInvite(int invitationId)
        {
            string email = HttpContext.Session.GetString(Sessions.Member);
            var currentMember = _context.Members.SingleOrDefault(m => m.Email == email);
            //var InviteMember = _context.Members.SingleOrDefault(m => m.MemberId == memberId);

            var invite = _context.Invitations.SingleOrDefault(i => i.InvitationId == invitationId);
            if (invite == null)
            {
                Error = "Invitation already approved";
            }
            else
            {
                invite.StatusId = 2;                
                _context.SaveChanges();
            }


            return RedirectToPage();
        }


        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);

            Member = _context.Members.SingleOrDefault(m => m.Email == email);
            Invitations = _context.Invitations
                .Where(m => m.ReceiverId == Member.MemberId && m.StatusId == 1)
                .Include(s => s.Sender);

            InvitationAccepted = _context.Invitations
                .Where(m => m.ReceiverId == Member.MemberId && m.StatusId != 1)
                .Include(s => s.Sender);
        }
    }
}
