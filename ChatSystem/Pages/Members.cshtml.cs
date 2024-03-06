using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.Pages
{
    public class MembersModel : PageModel
    {
        private readonly AppDbContext _context;

        public MembersModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public Member Member { get; set; }
        public IEnumerable<Member> Members { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int MemberId { get; set; }

        [BindProperty]
        public string Error { get; set; }

        public IActionResult OnPostInvite(int memberId)
        {
            string email = HttpContext.Session.GetString(Sessions.Member);
            var currentMember = _context.Members.SingleOrDefault(m => m.Email == email);
            //var InviteMember = _context.Members.SingleOrDefault(m => m.MemberId == memberId);

            var invite = _context.Invitations.SingleOrDefault(i => i.SenderId == currentMember.MemberId && i.ReceiverId == memberId);
            if (invite != null)
            {
                Error = "Invitation already sent";                
            }
            else
            {
                var model = new Invitation
                {
                    SenderId = currentMember.MemberId,
                    ReceiverId = memberId,
                    StatusId = 1,
                };

                _context.Invitations.Add(model);
                _context.SaveChanges();
            }
           

            return RedirectToPage();
        }

        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);          

            Members = _context.Members.Where(m => m.Email != email).ToList();

            Member = _context.Members.SingleOrDefault(m => m.Email == email);

            
        }
    }
}
