using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KiKiRamene.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            try
            {
                string path = "history.txt";

                var text = System.IO.File.ReadLines(path);
                var infos = text.LastOrDefault()?.Split("-");
                ViewData["winner"] = infos[1];
                ViewData["date"] = infos[0];

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}