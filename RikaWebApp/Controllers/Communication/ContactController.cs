using System.Diagnostics;
using System.Text;
using Azure.Messaging.ServiceBus;
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
        public async Task<IActionResult> Index()
        {
            return View();
        }

		[HttpPost]
		[Route("/contact/submit")]
		public async Task<IActionResult> ContactSubmit([Bind(Prefix = "ContactForm")] ContactFormModel contactForm)
		{
			
			if (TryValidateModel(contactForm))
			{
				var caseId = new Guid();
				var emailRequest = new ContactEmailRequest() 
				{
					To = contactForm.Email,
					Subject = $"Thank you {contactForm.FullName} for contacting us regarding {contactForm.ContactService} || Support ticket number: {caseId}",
					HtmlBody = "",
					PlainText = $"We will get back to you in 24hrs regarding your question {contactForm.Message}"
				};

				using var httpClient = new HttpClient();
				var jsonContent = JsonConvert.SerializeObject(emailRequest);

				// using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
				// var response = await httpClient.PostAsync($"https://localhost:7212/create", content);
								
				try
				{
						await using var sbClient = new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnection"));
						ServiceBusSender sender = sbClient.CreateSender("email_request");
						await sender.SendMessageAsync(new ServiceBusMessage(jsonContent));                                                                              
						TempData["Success"] = "Thank you for your email";                                     
						TempData["Failed"] = null;
						Console.WriteLine(TempData["Success"]);
				}
				catch (Exception ex) 
				{ 
						Debug.WriteLine(ex.Message); 
						TempData["Failed"] = "Error sending email, please try again soon";
						TempData["Success"] = null;
						Console.WriteLine(TempData["Failed"]);
				}
			}
			return RedirectToAction("Index", "Contact");
		}
    }
}
