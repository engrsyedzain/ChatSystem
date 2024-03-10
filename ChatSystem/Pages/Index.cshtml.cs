using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _context;
      
        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty]
        public string Error { get; set; }

        public IEnumerable<Member> Members { get; set; }

        public void OnGet()
        {
            Members = _context.Members.ToList();
        }


       


        public IActionResult OnPost()
        {
            
            var member = _context.Members.FirstOrDefault(m => m.Email == Email);
            if(member != null)
            {
                HttpContext.Session.SetString(Sessions.Member, member.Email);
                return RedirectToPage("/Members");
            }
            else
            {
                Error = "Email not correct!";
                return Page();
            }

        }
    }
}
