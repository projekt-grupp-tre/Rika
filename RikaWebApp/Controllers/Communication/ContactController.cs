using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.Communication;


namespace RikaWebApp.Controllers
{
    public class ContactController(ILogger<ContactController> logger, IConfiguration configuration) : Controller
    {
        private readonly ILogger<ContactController> _logger = logger;
				private readonly IConfiguration _configuration = configuration;

				[HttpGet]
				[Route("/contact")]
        public IActionResult Index()
        {
            return View();
        }

				[HttpPost]
				[Route("/contact/submit")]
				public async Task<IActionResult> ContactSubmit([Bind(Prefix = "ContactForm")] ContactFormModel contactForm)
				{
						if (TryValidateModel(contactForm))
						{
								var model = new ContactSubmissionModel()
								{
										Name = contactForm.FullName,
										Email = contactForm.Email,
										Message = contactForm.Message,
										ServiceCategory = contactForm.ContactService!,
								};

								using var httpClient = new HttpClient();
								var jsonContent = JsonConvert.SerializeObject(model);
								using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
								var response = await httpClient.PostAsync($"https://localhost:1234/api/contact?key={_configuration["ApiKey"]}", content);

								if (response.IsSuccessStatusCode)
								{
										TempData["Success"] = "Email sent";
								}
								else
								{
										TempData["Failed"] = "Error";
								}
						}
						return RedirectToAction("Contact", "Contact");
				}
    }
}
