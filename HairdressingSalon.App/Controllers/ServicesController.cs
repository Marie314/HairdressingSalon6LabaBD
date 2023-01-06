using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.App.ViewModels;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Flurl.Http;
using Flurl.Http.Configuration;
using System.Runtime.ConstrainedExecution;

namespace HairdressingSalon.App.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IFlurlClient _flurlClient;
        private readonly IFlurlClient _flurlClientServiceKinds;
        private readonly IFeedbacksService _feedbacksService;
        private readonly IOrdersService _ordersService;
        private static int _currentId;
        private static int _countOfEntities = 20;
        public ServicesController(IFlurlClientFactory flurlClientFactory, 
            IFeedbacksService feedbacksService,
            IOrdersService ordersService,
            IFlurlClientFactory flurlClientServiceKindsFactory)
        {
            _flurlClient = flurlClientFactory.Get("https://localhost:7080/api/services/");
            _feedbacksService = feedbacksService;
            _ordersService = ordersService;
            _flurlClientServiceKinds = flurlClientServiceKindsFactory.Get("https://localhost:7080/api/serviceKinds/");
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            return View(await _flurlClient.Request().GetJsonAsync<List<Service>>());

        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            return View("Get", await _flurlClient.Request($"/{entities_count}").GetJsonAsync<List<Service>>());

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View("Get", await _flurlClient.Request("all").GetJsonAsync<List<Service>>());
        }

        [HttpGet]
        public async Task<IActionResult> ClearForm()
        {
            Response.Cookies.Delete("service_year");

            return View("Get", await _flurlClient.Request().GetJsonAsync<List<Service>>());

        }

        // Владааааа это дичь
        [HttpGet]
        public async Task<IActionResult> GetByCondition(int service_year)
        {
            return View("Get", await _flurlClient.Request($"/search?service_year={service_year}").GetJsonAsync<List<Service>>());
        }

        [HttpGet]
        public IActionResult GetCountInDateRange()
        {
            return View();
        }

        // это тоже я хз
        [HttpGet]
        public async Task<IActionResult> GetCountInDateRangeAction(DateTime startDate, DateTime endDate)
        {
            var services = await _flurlClient.Request("all").GetJsonAsync<List<Service>>();
            int count = services.Where(s => s.Order.DateTime >= startDate && s.Order.DateTime <= endDate).Count();

            ViewData["count_by_date"] = count;

            return View("GetCountInDateRange");
        }

        // это тоже непонятно
        [HttpGet]
        public IActionResult GetInDateRangeByMark()
        {
            return View(new List<Service>());
        }

        // страшно
        [HttpGet]
        public async Task<IActionResult> GetInDateRangeByMarkAction(DateTime startDate, DateTime endDate)
        {
            var feedbacks = await _feedbacksService.GetAll();
            var ordersIds = feedbacks.Where(f => f.Mark >= 3).Select(f => f.OrderId).Distinct();

            var list = await _flurlClient.Request("all").GetJsonAsync<List<Service>>();
            var services = list.Where(s => s.Order.DateTime >= startDate && s.Order.DateTime <= endDate).ToList();
            var servicesIds = new List<int>();

            foreach (var ser in services)
            {
                if (!ordersIds.Contains(ser.OrderId))
                {
                    servicesIds.Add(ser.Id);
                }
            }

            foreach (var id in servicesIds)
            {
                services.Remove(services.First(s => s.Id == id));
            }

            return View("GetInDateRangeByMark", services);
        }

        //каааак
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var orders = await _ordersService.GetAll();
            var serviceKinds = await _flurlClient.Request("all").GetJsonAsync<List<ServiceKind>>();

            return View(new ServiceCreatedModel
            {
                Orders = orders.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }

        // аааааааааа
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreatedModel serviceCreated)
        {


            if (!ModelState.IsValid)
            {
                var orders = await _ordersService.GetAll();
                var serviceKinds = await _flurlClient.Request("all").GetJsonAsync<List<ServiceKind>>();

                return View(new ServiceCreatedModel
                {
                    Orders = orders.ToList(),
                    ServiceKinds = serviceKinds.ToList(),
                });
            }

            await _flurlClient.Request().PostJsonAsync(new ServiceCreated
            {
                Code = serviceCreated.Code,
                Price = serviceCreated.Price,
                OrderId = int.Parse(serviceCreated.Order.Split(',')[0]),
                ServiceKindId = int.Parse(serviceCreated.ServiceKind.Split(',')[0]),
            });

            return View("Get", await _flurlClient.Request($"/{_countOfEntities}").GetJsonAsync<List<Service>>());
        }


        //стремно
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var entity = await _flurlClient.Request($"/{id}").GetJsonAsync<Service>();
            var orders = await _ordersService.GetAll();
            var serviceKinds = await _flurlClient.Request("all").GetJsonAsync<List<ServiceKind>>();
            _currentId = id;

            return View(new ServiceUpdatedModel
            {
                Id = id,
                Price = entity.Price,
                Code = entity.Code,
                Orders = orders.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }


        //стремно
        [HttpPost]
        public async Task<IActionResult> Update(ServiceUpdatedModel serviceUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _flurlClient.Request($"/{_currentId}").GetJsonAsync<Service>();
                var orders = await _ordersService.GetAll();
                var serviceKinds = await _flurlClient.Request("all").GetJsonAsync<List<ServiceKind>>();

                return View(new ServiceUpdatedModel
                {
                    Id = _currentId,
                    Price = entity.Price,
                    Code = entity.Code,
                    Orders = orders.ToList(),
                    ServiceKinds = serviceKinds.ToList(),
                });
            }

            await _flurlClient.Request().PutJsonAsync(new ServiceUpdated
            {
                Id = _currentId,
                Price = serviceUpdated.Price,
                Code = serviceUpdated.Code,
                OrderId = int.Parse(serviceUpdated.Order.Split(',')[0]),
                ServiceKindId = int.Parse(serviceUpdated.ServiceKind.Split(',')[0]),
            });

            return View("Get", await _flurlClient.Request($"/{_countOfEntities}").GetJsonAsync<List<Service>>());
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _currentId = id;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm()
        {
            var result = await _flurlClient.Request($"/{_currentId}").DeleteAsync();

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return View();
            }

            return View("Get", await _flurlClient.Request().GetJsonAsync());

        }
    }
}
