using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        List<Member> members = new List<Member>
        {
            new Member { Id = 1001, Name = "Syed Zain", Email = "zain@domain.com" },
            new Member { Id = 1002, Name = "Shariq", Email = "shariq@domain.com" },
            new Member { Id = 1003, Name = "Talha", Email = "talha@domain.com" },
            new Member { Id = 1004, Name = "Shoaib", Email = "shoaib@domain.com" }
            // Add more members as needed
        };
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty]
        public string Error { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var member = members.FirstOrDefault(m => m.Email == Email);
            if(member != null)
            {
                HttpContext.Session.SetString(Sessions.Member, member.Email);
                return RedirectToPage("/ChatPage");
            }
            else
            {
                Error = "Email not correct!";
                return Page();
            }

        }
    }
}
