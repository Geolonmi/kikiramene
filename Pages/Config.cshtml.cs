using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace KiKiRamene.Pages
{
    public class ConfigModel : PageModel
    {

        private IConfiguration _configuration { get; }


        public ConfigModel(IConfiguration configuration)
        {
            _configuration= configuration;
        }

        private string[] participants = new string[] {
            "Damien",
            "Omar",
            "Hugues",
            "Julien",
            "Stéfan",
            "Fanny",
            "Fabien",
            "Séverine",
            "Romain",
            "Geoffroy",
            "Sylvain"
        };

        public DateTime Date { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Config? Config { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Config?.Secret != _configuration["PRIVATE_KEY"])
            {
                return Page();
            }

            if (Config != null)
            {
                try
                {
                    string path = "history.txt";

                    using (StreamWriter sw = System.IO.File.Exists(path) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                    {
                        sw.WriteLine(Config.Date + "-" + participants[new Random().Next(0, participants.Length)]);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }

            }

            return RedirectToPage("./Index");
        }
    }
    public class Config
    {
        public DateTime Date { get; set; }

        public string? Participant { get; set; }

        public string? Secret { get; set; }
    }
}
