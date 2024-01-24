using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApi.Dtos;
using WebApi.Errors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    public class ClientController : BaseApiController
    {
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IGenericRepository<Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Client>>> GetClients()
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound(new CodeErrorResponse(404, "El cliente no existe"));
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            var result = await _clientRepository.Add(client);
            if (result == 0)
            {
                throw new Exception("No se inserto el cliente");
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, Client client)
        {
            client.Id = id;
            var result = await _clientRepository.Update(client);
            if (result == 0)
            {
                throw new Exception("No se actualizo el cliente");
            }

            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new Exception("No se encontro el cliente");
            }
            _clientRepository.DeleteEntity(client);

            return Ok(client);
        }
    }
}
