using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.App.ViewModels;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.App.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IClientsService _clientsService;
        private readonly IWorkersService _workersService;
        private readonly IFeedbacksService _feedbacksService;
        private static int _currentId = 0;
        private static int _countOfEntities = 20;

        public OrdersController(IOrdersService ordersService, 
            IClientsService clientsService,
            IWorkersService workersService,
            IFeedbacksService feedbacksService)
        {
            _ordersService = ordersService;
            _clientsService = clientsService;
            _workersService = workersService;
            _feedbacksService = feedbacksService;
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            return View(await _ordersService.Get(_countOfEntities, $"Orders{_countOfEntities}"));
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            return View("Get", await _ordersService.Get(_countOfEntities, $"Orders{_countOfEntities}"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View("Get", await _ordersService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetByCondition()
        {
            var feedbacks = await _feedbacksService.GetAll();
            var ordersIds = feedbacks.Where(f => f.Mark < 3).Select(f => f.OrderId).Distinct().OrderBy(o => o);

            var orders = new List<Order>();
            foreach (var id in ordersIds)
            {
                orders.Add(await _ordersService.GetById(id));
            }

            return View("Get", orders.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var clients = await _clientsService.GetAll();
            var workers = await _workersService.GetAll();

            return View(new OrderCreatedModel
            {
                Clients = clients.ToList(),
                Workers = workers.ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreatedModel orderCreated)
        {
            if (!ModelState.IsValid)
            {
                var clients = await _clientsService.GetAll();
                var workers = await _workersService.GetAll();

                return View(new OrderCreatedModel
                {
                    Clients = clients.ToList(),
                    Workers = workers.ToList(),
                });
            }

            await _ordersService.Create(new OrderCreated
            {
                DateTime = orderCreated.DateTime,
                ClientId = int.Parse(orderCreated.Client.Split(',')[0]),
                WorkerId = int.Parse(orderCreated.Worker.Split(',')[0]),
            });

            return View("Get", await _ordersService.Get(_countOfEntities, $"Orders{_countOfEntities}1"));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _ordersService.GetById(id);
            _currentId = id;

            var clients = await _clientsService.GetAll();
            var workers = await _workersService.GetAll();

            return View(new OrderUpdatedModel
            {
                Clients = clients.ToList(),
                Workers = workers.ToList(),
                OldOrder = entity,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderUpdatedModel orderUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _ordersService.GetById(_currentId);

                var clients = await _clientsService.GetAll();
                var workers = await _workersService.GetAll();

                return View(new OrderUpdatedModel
                {
                    Clients = clients.ToList(),
                    Workers = workers.ToList(),
                    OldOrder = entity,
                });
            }

            orderUpdated.Id = _currentId;
            await _ordersService.Update(new OrderUpdated
            {
                Id = _currentId,
                DateTime = orderUpdated.DateTime,
                ClientId = int.Parse(orderUpdated.Client.Split(',')[0]),
                WorkerId = int.Parse(orderUpdated.Worker.Split(',')[0]),
            });

            return View("Get", await _ordersService.Get(_countOfEntities, $"Orders{_countOfEntities}4"));
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
            await _ordersService.Delete(_currentId);

            return View("Get", await _ordersService.Get(_countOfEntities, $"Orders{_countOfEntities}2"));
        }
    }
}
