using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KiKiRamene.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        { }

        public void OnGet()
        {
            try
            {
                string path = "history.txt";
                var records = System.IO.File.ReadLines(path)?.Select(s =>
                {
                    var infos = s.Split("-");
                    return new Record
                    {
                        Date = DateTime.Parse(infos![0]),
                        Name = infos![1]
                    };
                });

                var current = records?.LastOrDefault();
                ViewData["current"] = current;
                ViewData["historique"] = (records?.Any() ?? false) ? records.Take(records.Count() - 1).Reverse().ToArray() : Array.Empty<Record>();

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}