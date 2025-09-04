using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebsite_V1._0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Project> Projects { get; private set; } = new();

        public void OnGet()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, "data", "projects.json");
                if (System.IO.File.Exists(path))
                {
                    var json = System.IO.File.ReadAllText(path);
                    var items = JsonSerializer.Deserialize<List<Project>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (items is not null) Projects = items;
                }
                else
                {
                    // Fallback demo data if the JSON isn't there yet
                    Projects = new()
                    {
                        new Project
                        {
                            Title = "My Portfolio",
                            Description = "Personal website with glass UI and parallax background.",
                            Url = "/",
                            RepoUrl = "https://github.com/btitzer5/MyWebsite_V1.0",
                            ImageUrl = "/img/thumb-portfolio.png",
                            Tech = new[] { "ASP.NET Core", "Razor Pages", "CSS" }
                        }
                    };
                }
            }
            catch
            {
                // Keep empty on error
            }
        }
    }

    public class Project
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? RepoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string[]? Tech { get; set; }
    }
}
