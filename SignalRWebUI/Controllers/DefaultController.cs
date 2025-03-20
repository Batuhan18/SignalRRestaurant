using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalR.DtoLayer.ContactDto;
using SignalRWebUI.Dtos.MessageDtos;
using System.Text;
using System.Text.Json.Nodes;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("https://localhost:7224/api/Contact");
            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            ////var values = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);
            //JsonObject item = JsonObject.Parse(jsonData);
            //ViewBag.location = values.Location;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7224/api/Contact");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JArray item = JArray.Parse(responseBody);
            ViewBag.location = item[0]["location"].ToString();
            return View();
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)

        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7224/api/Message", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
