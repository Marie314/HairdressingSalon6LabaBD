using Flurl.Http;
using Flurl.Http.Configuration;
using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.App.ViewModels;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Net;

namespace HairdressingSalon.App.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IFlurlClient _flurlClient;
        private static int _currentId;

        public ClientsController(IFlurlClientFactory flurlClientFactory)
        {
            _flurlClient = flurlClientFactory.Get("https://localhost:7080/api/clients/");
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            ViewData["client_surname"] = Request.Cookies["client_surname"];
            ViewData["client_service_kind"] = Request.Cookies["client_service_kind"];

            return View(await _flurlClient.Request().GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            ViewData["client_surname"] = Request.Cookies["client_surname"];
            ViewData["client_service_kind"] = Request.Cookies["client_service_kind"];

            return View("Get", await _flurlClient.Request($"/{entities_count}").GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ViewData["client_surname"] = Request.Cookies["client_surname"];
            ViewData["client_service_kind"] = Request.Cookies["client_service_kind"];

            return View("Get", await _flurlClient.Request("all").GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        public async Task<IActionResult> ClearForm()
        {
            Response.Cookies.Delete("client_surname");
            Response.Cookies.Delete("client_service_kind");

            return View("Get", await _flurlClient.Request().GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetByCondition(string client_surname, string client_service_kind)
        {
            client_surname ??= "1234567890";
            client_service_kind ??= "1234567890";

            if (client_surname != "1234567890")
                Response.Cookies.Append("client_surname", client_surname);
            if (client_service_kind != "1234567890")
                Response.Cookies.Append("client_service_kind", client_service_kind);

            ViewData["client_surname"] = Request.Cookies["client_surname"];
            ViewData["client_service_kind"] = Request.Cookies["client_service_kind"];

            return View("Get", await _flurlClient.Request($"search?client_surname={client_surname}&client_service_kind={client_service_kind}").GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreated clientCreated)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var res = await _flurlClient.Request().PostJsonAsync(clientCreated);

            return View("Get", res);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _flurlClient.Request($"/{id}").GetJsonAsync<Client>();
            _currentId = id;

            return View(new ClientUpdated
            {
                Id = id,
                Surname = entity.Surname,
                Name = entity.Name,
                MiddleName = entity.MiddleName,
                Address = entity.Address,
                Telephone = entity.Telephone
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClientUpdated clientUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _flurlClient.Request($"/{_currentId}").GetJsonAsync<Client>();

                return View(new ClientUpdated
                {
                    Id = _currentId,
                    Surname = entity.Surname,
                    Name = entity.Name,
                    MiddleName = entity.MiddleName,
                    Address = entity.Address,
                    Telephone = entity.Telephone
                });
            }

            clientUpdated.Id = _currentId;
            var result = await _flurlClient.Request().PutJsonAsync(clientUpdated);

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return View();
            }

            return View("Get", await _flurlClient.Request().GetJsonAsync<ClientsModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _currentId = id;

            return View(await _flurlClient.Request($"/{_currentId}").GetJsonAsync<Client>());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm()
        {
            var result = await _flurlClient.Request($"/{_currentId}").DeleteAsync();

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return View();
            }

            return View("Get", await _flurlClient.Request().GetJsonAsync<ClientsModel>());
        }
    }
}
