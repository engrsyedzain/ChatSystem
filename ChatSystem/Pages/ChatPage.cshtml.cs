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

        [BindProperty]
        public List<SelectListItem> Users { get; set; }

        [BindProperty]
        public string MyUser { get; set; }
        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);
            Users = members.Where(m => m.Email != email).ToList()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .OrderBy(s => s.Text).ToList();

            Members = members.Where(m => m.Email != email).ToList();

            Member = members.SingleOrDefault(m => m.Email == email);

            MyUser = Member.Name;
        }

    }
}
