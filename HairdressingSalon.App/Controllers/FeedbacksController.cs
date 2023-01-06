using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.ViewModels;
using HairdressingSalon.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.App.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly IFeedbacksService _feedbacksService;
        private readonly IOrdersService _ordersService;
        private static int _currentId = 0;
        private static int _countOfEntities = 20;

        public FeedbacksController(IFeedbacksService feedbacksService,
            IOrdersService ordersService)
        {
            _feedbacksService = feedbacksService;
            _ordersService = ordersService;
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> Get()
        {
            return View(await _feedbacksService.Get(_countOfEntities, $"Feedbacks{_countOfEntities}"));
        }

        [HttpGet]
        [ResponseCache(Duration = 262)]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            return View("Get", await _feedbacksService.Get(_countOfEntities, $"Feedbacks{_countOfEntities}"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View("Get", await _feedbacksService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var orders = await _ordersService.GetAll();

            return View(new FeedbackCreatedModel
            {
                Orders = orders.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackCreatedModel feedbackCreated)
        {
            if (!ModelState.IsValid)
            {
                var orders = await _ordersService.GetAll();

                return View(new FeedbackCreatedModel
                {
                    Orders = orders.ToList()
                });
            }

            await _feedbacksService.Create(new FeedbackCreated
            {
                Text = feedbackCreated.Text,
                DateTime = DateTime.Now,
                Mark = feedbackCreated.Mark,
                OrderId = int.Parse(feedbackCreated.Order.Split(',')[0]),
            });

            return View("Get", await _feedbacksService.Get(_countOfEntities, $"Feedbacks{_countOfEntities}1"));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _feedbacksService.GetById(id);
            _currentId = id;

            return View(new FeedbackUpdated
            {
                Id = id,
                Text = entity.Text,
                Mark = entity.Mark
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(FeedbackUpdated feedbackUpdated)
        {
            if (!ModelState.IsValid)
            {
                var entity = await _feedbacksService.GetById(_currentId);

                return View(new FeedbackUpdated
                {
                    Id = _currentId,
                    Text = entity.Text,
                    Mark = entity.Mark
                });
            }

            feedbackUpdated.Id = _currentId;
            await _feedbacksService.Update(feedbackUpdated);

            return View("Get", await _feedbacksService.Get(_countOfEntities, $"Feedbacks{_countOfEntities}4"));
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
            await _feedbacksService.Delete(_currentId);

            return View("Get", await _feedbacksService.Get(_countOfEntities, $"Feedbacks{_countOfEntities}2"));
        }
    }
}
