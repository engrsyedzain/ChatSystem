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

                var memberConnection = _context.MemberConnections.SingleOrDefault(m => m.MemberId == currentMember.MemberId);
                if (memberConnection == null)
                {
                    _context.MemberConnections.Add(new MemberConnection { MemberId = currentMember.MemberId });
                }

                _context.Invitations.Add(model);
                _context.SaveChanges();
            }
           

            return RedirectToPage();
        }

        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);          

            Members =  _context.Members.Where(m => m.Email != email).ToList();

            Member = _context.Members.SingleOrDefault(m => m.Email == email);
         
            

        }


        public IActionResult OnPostStatus(int memberId, int statusId)
        {
            string email = HttpContext.Session.GetString(Sessions.Member);
            Member = _context.Members.SingleOrDefault(m => m.Email == email);
            
            var invitation = _context.Invitations
               .FirstOrDefault(inv =>
               inv.SenderId == memberId && inv.ReceiverId == Member.MemberId);
            
            if (invitation != null)
            {
                invitation.StatusId = statusId;
                var memberConnection = _context.MemberConnections.SingleOrDefault(m => m.MemberId == Member.MemberId);
                if (memberConnection == null && statusId == 2)
                {
                    _context.MemberConnections.Add(new MemberConnection { MemberId = Member.MemberId });
                }
                _context.SaveChanges();
            }


            return RedirectToPage();
        }


        public bool HasInvitationSender(int memberId)
        {

            return _context.Invitations
                .Any(inv => inv.SenderId == memberId && inv.ReceiverId == Member.MemberId);
        }
        public bool HasInvitationReceiver(int memberId)
        {

            return _context.Invitations
                .Any(inv => inv.SenderId == Member.MemberId && inv.ReceiverId == memberId);
        }

        public bool GetInvitationSenderStatus(int memberId)
        {
            var invitation = _context.Invitations
                .FirstOrDefault(inv =>
                inv.SenderId == memberId && inv.ReceiverId == Member.MemberId);
            if (invitation != null)
            {
                return true;
            }
            return false;
        }

        public int? GetInvitationStatus(int memberId)
        {
            var invitation = _context.Invitations
                .FirstOrDefault(inv => 
                inv.SenderId == memberId && inv.ReceiverId == Member.MemberId || 
                inv.SenderId == Member.MemberId && inv.ReceiverId == memberId);
            if (invitation != null)
            {
                return invitation.StatusId;
            }
            return 0;
        }
    }
}
