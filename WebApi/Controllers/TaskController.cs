using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApi.Dtos;
using WebApi.Errors;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    public class TaskController : BaseApiController
    {
        private readonly IGenericRepository<Core.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(IGenericRepository<Core.Entities.Task> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Core.Entities.Task>>> GetTasks()
        {
            var spec = new TaskWithProjectSpecification();
            var tasks = await _taskRepository.GetAllWithSpec(spec);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.Entities.Task>> GetTask(int id)
        {
            var spec = new TaskWithProjectSpecification(id);
            var task = await _taskRepository.GetByIdWithSpec(spec);

            if (task == null)
            {
                return NotFound(new CodeErrorResponse(404, "La tarea no existe"));
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Core.Entities.Task>> Post(Core.Entities.Task task)
        {
            var result = await _taskRepository.Add(task);
            if (result == 0)
            {
                throw new Exception("No se inserto la tarea");
            }

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Core.Entities.Task>> Put(int id, Core.Entities.Task task)
        {
            task.Id = id;
            var result = await _taskRepository.Update(task);
            if (result == 0)
            {
                throw new Exception("No se actualizo el usuario");
            }

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.Entities.Task>> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new Exception("No se encontro la tarea");
            }
            _taskRepository.DeleteEntity(task);

            return Ok(task);
        }
    }
}
