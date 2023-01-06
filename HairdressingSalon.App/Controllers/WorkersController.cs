using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.App.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IWorkersService _workersService;
        private readonly IOrdersService _orderService;
        private static int _currentId;
        private static int _countOfEntities = 20;

        public WorkersController(IWorkersService workersService, IOrdersService orderService)
        {
            _workersService = workersService;
            _orderService = orderService;
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            ViewData["worker_surname"] = Request.Cookies["worker_surname"];

            return View(await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}"));
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            ViewData["worker_surname"] = Request.Cookies["worker_surname"];
            _countOfEntities = entities_count;

            return View("Get", await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ViewData["worker_surname"] = Request.Cookies["worker_surname"];

            return View("Get", await _workersService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> ClearForm()
        {
            Response.Cookies.Delete("worker_surname");

            return View("Get", await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}"));
        }

        [HttpGet]
        public async Task<IActionResult> GetByCondition(string worker_surname, DateTime worker_workdate)
        {
            var list = await _workersService.GetAll();

            worker_surname ??= "1234567890";

            if (worker_surname != "1234567890")
                Response.Cookies.Append("worker_surname", worker_surname);

            ViewData["worker_surname"] = Request.Cookies["worker_surname"];

            if (worker_workdate < new DateTime(2000, 1, 1))
                return View("Get", list.Where(w => w.Surname.Contains(worker_surname, StringComparison.OrdinalIgnoreCase)).ToList());

            var orders = await _orderService.GetAll();
            var workersIds = orders.Where(o => o.DateTime.Date.Equals(worker_workdate.Date)).Select(o => o.WorkerId).Distinct();

            var workers = new List<Worker>();
            foreach (var id in workersIds)
            {
                workers.Add(await _workersService.GetById(id));
            }

            if (worker_surname != "1234567890")
                return View("Get", workers.Where(w => w.Surname.Contains(worker_surname, StringComparison.OrdinalIgnoreCase)).ToList());

            return View("Get", workers.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkerCreated workerCreated)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _workersService.Create(workerCreated);

            return View("Get", await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}1"));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _workersService.GetById(id);
            _currentId = id;

            return View(new WorkerUpdated
            {
                Id = id,
                Surname = entity.Surname,
                Name = entity.Name,
                MiddleName = entity.MiddleName
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkerUpdated workerUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _workersService.GetById(_currentId);

                return View(new WorkerUpdated
                {
                    Id = _currentId,
                    Surname = entity.Surname,
                    Name = entity.Name,
                    MiddleName = entity.MiddleName
                });
            }

            workerUpdated.Id = _currentId;
            await _workersService.Update(workerUpdated);

            return View("Get", await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}4"));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _currentId = id;

            return View(await _workersService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm()
        {
            await _workersService.Delete(_currentId);

            return View("Get", await _workersService.Get(_countOfEntities, $"Workers{_countOfEntities}2"));
        }
    }
}
