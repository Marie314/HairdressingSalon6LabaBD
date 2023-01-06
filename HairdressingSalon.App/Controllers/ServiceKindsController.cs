using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace HairdressingSalon.App.Controllers
{
    public class ServiceKindsController : Controller
    {
        private readonly IFlurlClient _flurlClient;
        private static int _currentId;
        private static int _countOfEntities = 20;

        public ServiceKindsController(IFlurlClientFactory flurlClientFactory)
        {
            _flurlClient = flurlClientFactory.Get("https://localhost:7080/api/serviceKinds/");
        }


        //тут нет фигни в моделях
        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            return View(await _flurlClient.Request().GetJsonAsync());
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            return View("Get", await _flurlClient.Request($"/{entities_count}").GetJsonAsync<List<ServiceKind>>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View("Get", await _flurlClient.Request("all").GetJsonAsync<List<ServiceKind>>());

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceKindCreated serviceKindCreated)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var res = await _flurlClient.Request().PostJsonAsync(serviceKindCreated);

            return View("Get", res);

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _flurlClient.Request($"/{id}").GetJsonAsync<ServiceKind>();
            _currentId = id;

            return View(new ServiceKindUpdated
            {
                Id = id,
                Name = entity.Name,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ServiceKindUpdated serviceKindUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _flurlClient.Request($"/{_currentId}").GetJsonAsync<ServiceKind>();

                return View(new ServiceKindUpdated
                {
                    Id = _currentId,
                    Name = entity.Name,
                    Description = entity.Description,
                    ImageUrl = entity.ImageUrl
                });
            }

            serviceKindUpdated.Id = _currentId;
            var result = await _flurlClient.Request().PutJsonAsync(serviceKindUpdated);

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return View();
            }

            return View("Get", await _flurlClient.Request().GetJsonAsync<List<ServiceKind>>());

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            _currentId = id;

            return View(await _flurlClient.Request($"/{_currentId}").GetJsonAsync<List<ServiceKind>>());

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm()
        {
            var result = await _flurlClient.Request($"/{_currentId}").DeleteAsync();

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return View();
            }

            return View("Get", await _flurlClient.Request().GetJsonAsync<List<ServiceKind>>());
        }
    }
}
