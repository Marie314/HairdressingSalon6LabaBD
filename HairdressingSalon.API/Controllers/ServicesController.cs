using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.API.Controllers
{
    [Authorize]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        private readonly IOrdersService _ordersService;
        private readonly IServiceKindsService _serviceKindsService;
        private readonly IFeedbacksService _feedbacksService;
        private static int _currentId;
        private static int _countOfEntities = 20;

        public ServicesController(IServicesService servicesService,
            IOrdersService ordersService,
            IServiceKindsService serviceKindsService,
            IFeedbacksService feedbacksService)
        {
            _servicesService = servicesService;
            _ordersService = ordersService;
            _serviceKindsService = serviceKindsService;
            _feedbacksService = feedbacksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = await _servicesService.Get(_countOfEntities, $"Services{_countOfEntities}");

            return Ok(service);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _servicesService.GetById(id));
        }

        [HttpGet("{count}")]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            var services = await _servicesService.Get(_countOfEntities, $"Services{_countOfEntities}");

            return Ok(services);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var services = await _servicesService.GetAll();

            return Ok(services);
        }

        //тут хз
        [HttpGet("search")]
        public async Task<IActionResult> GetByCondition(int service_year)
        {
            var list = await _servicesService.GetAll();

            var services = list.Where(s => s.Order.DateTime.Year == service_year).OrderBy(s => s.Id).ToList();

            return Ok(services);
        }

        // хз что с этим делать и с классами ниже, где вьюхи

        [HttpGet("countInDateRange")]
        public async Task<IActionResult> GetCountInDateRange(DateTime startDate, DateTime endDate)
        {
            var services = await _servicesService.GetAll();
            int count = services.Where(s => s.Order.DateTime >= startDate && s.Order.DateTime <= endDate).Count();

            return Ok(count);
        }

        [HttpGet("getInDateRangeByMark")]
        public async Task<IActionResult> GetInDateRangeByMark(DateTime startDate, DateTime endDate)
        {
            var feedbacks = await _feedbacksService.GetAll();
            var ordersIds = feedbacks.Where(f => f.Mark >= 3).Select(f => f.OrderId).Distinct();

            var list = await _servicesService.GetAll();
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

            return Ok(services);
        }

        //переделывала метод, но сомительно
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreatedModel serviceCreated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _servicesService.Create(new ServiceCreated
            {
                Code = serviceCreated.Code,
                Price = serviceCreated.Price,
                OrderId = int.Parse(serviceCreated.Order.Split(',')[0]),
                ServiceKindId = int.Parse(serviceCreated.ServiceKind.Split(',')[0]),
            });

            var orders = await _ordersService.GetAll();
            var serviceKinds = await _serviceKindsService.GetAll();


            return Ok(new ServiceCreatedModel
            {
                Orders = orders.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });

        }

        //мэйби хуйня
        // норм
        [HttpPut]
        public async Task<IActionResult> Update(ServiceUpdatedModel serviceUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isExists = await _servicesService.Update(new ServiceUpdated
            {
                Id = _currentId,
                Price = serviceUpdated.Price,
                Code = serviceUpdated.Code,
                OrderId = int.Parse(serviceUpdated.Order.Split(',')[0]),
                ServiceKindId = int.Parse(serviceUpdated.ServiceKind.Split(',')[0]),
            });

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isExists = await _servicesService.Delete(id);

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
