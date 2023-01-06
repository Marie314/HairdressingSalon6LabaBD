using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.API.Controllers
{
    [Authorize]
    [Route("api/serviceKinds")]
    public class ServiceKindsController : ControllerBase
    {
        private readonly IServiceKindsService _serviceKindsService;

        private static int _countOfEntities = 20;

        public ServiceKindsController(IServiceKindsService serviceKindsService)
        {
            _serviceKindsService = serviceKindsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _serviceKindsService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var serviceKinds = await _serviceKindsService.Get(_countOfEntities, $"ServiceKinds{_countOfEntities}");

            return Ok(serviceKinds);
        }

        [HttpGet("{count}")]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            var serviceKinds = await _serviceKindsService.Get(_countOfEntities, $"ServiceKinds{_countOfEntities}");

            return Ok(serviceKinds);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var serviceKinds = await _serviceKindsService.GetAll();

            return Ok(serviceKinds);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceKindCreated serviceKindCreated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _serviceKindsService.Create(serviceKindCreated);

            var serviceKinds = await _serviceKindsService.Get(_countOfEntities, $"ServiceKinds{_countOfEntities}1");

            return Ok(serviceKinds);

        }

        [HttpPut]
        public async Task<IActionResult> Update(ServiceKindUpdated serviceKindUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isExists = await _serviceKindsService.Update(serviceKindUpdated);

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isExists = await _serviceKindsService.Delete(id);

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
