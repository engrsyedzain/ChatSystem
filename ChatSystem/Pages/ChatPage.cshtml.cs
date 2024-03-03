using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatSystem.Pages
{
    public class ChatPageModel : PageModel
    {
        List<Member> members = new List<Member>
        {
            new Member { Id = 1001, Name = "Syed Zain", Email = "zain@domain.com" },
            new Member { Id = 1002, Name = "Shariq", Email = "shariq@domain.com" },
            new Member { Id = 1003, Name = "Talha", Email = "talha@domain.com" },
            new Member { Id = 1004, Name = "Shoaib", Email = "shoaib@domain.com" }
            // Add more members as needed
        };

        public Member Member { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public string SenderId { get; set; } 

        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);
            Member = members.SingleOrDefault(m => m.Email == email);

            Members = members.Where(m => m.Email != email).ToList();
            SenderId = Member?.Id.ToString() ?? "";


            //   Users = members.Where(m => m.Email != email).ToList()
            //.Select(a => new SelectListItem { Text = a.Name, Value = a.Name })
            //.OrderBy(s => s.Text).ToList();

            //   SenderId = Member.Id;
            //   HttpContext.Session.SetInt32(Sessions.MemberId, SenderId);

        }
    }
}
