using HairdressingSalon.App.BLL.Interfaces;
using HairdressingSalon.App.DAL.DTO;
using HairdressingSalon.App.DAL.Models;
using HairdressingSalon.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairdressingSalon.API.Controllers
{
    [Authorize]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly IServicesService _servicesService;
        private readonly IServiceKindsService _serviceKindsService;

        private static int _countOfEntities = 20;


        public ClientsController(IClientsService clientsService, IServicesService servicesService, IServiceKindsService serviceKindsService)
        {
            _clientsService = clientsService;
            _servicesService = servicesService;
            _serviceKindsService = serviceKindsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clients = await _clientsService.Get(_countOfEntities, $"Clients{_countOfEntities}");
            var serviceKinds = await _serviceKindsService.GetAll();

            return Ok(new ClientsModel
            {
                Clients = clients.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _clientsService.GetById(id));
        }

        //хз что с этим методом. его у Влады нет
        [HttpGet("{count}")]
        public async Task<IActionResult> GetByCount(int entities_count)
        {
            _countOfEntities = entities_count;

            var clients = await _clientsService.Get(_countOfEntities, $"Clients{_countOfEntities}");
            var serviceKinds = await _serviceKindsService.GetAll();

            return Ok(new ClientsModel
            {
                Clients = clients.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientsService.GetAll();
            var serviceKinds = await _serviceKindsService.GetAll();

            return Ok(new ClientsModel
            {
                Clients = clients.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }

        // говно, которое я хз как менять
        [HttpGet("search")]
        public async Task<IActionResult> GetByCondition(string client_surname, string client_service_kind)
        {
            var list = await _clientsService.GetAll();

            client_surname ??= "1234567890";
            client_service_kind ??= "1234567890";

            var services = await _servicesService.GetAll();
            var clientsIds = services.Where(s => s.ServiceKind.Name.Equals(client_service_kind)).Select(s => s.Order.ClientId).Distinct().OrderBy(c => c);

            var clients = new List<Client>();
            foreach (var id in clientsIds)
            {
                clients.Add(await _clientsService.GetById(id));
            }

            var serviceKinds = await _serviceKindsService.GetAll();

            if (!clients.Any() && client_surname == "1234567890")
                return Ok(clients);

            if (!clients.Any() && client_surname != "1234567890")
                return Ok(clients);

            if (clients.Any() && client_surname != "1234567890")
                return Ok(clients);

            return Ok(clients);
        }

        //вроде верно. но у меня нет пременной
        //у Влады переменная вверху сделана
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreated clientCreated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _clientsService.Create(clientCreated);

            var clients = await _clientsService.Get(_countOfEntities, $"Clients{_countOfEntities}1");
            var serviceKinds = await _serviceKindsService.GetAll();

            return Ok(new ClientsModel
            {
                Clients = clients.ToList(),
                ServiceKinds = serviceKinds.ToList(),
            });
        }

        // вроде верно, опять траблы с переменной
        [HttpPut]
        public async Task<IActionResult> Update(ClientUpdated clientUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isExists = await _clientsService.Update(clientUpdated);

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();
        }

        // вроде верно . но у Влады id через guid указано
        // опять мэйби нужен счетчик??
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isExists = await _clientsService.Delete(id);

            if (!isExists)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
