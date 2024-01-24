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
    public class ProjectController : BaseApiController
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectController(IGenericRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Project>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound(new CodeErrorResponse(404, "El proyecto no existe"));
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post(Project project)
        {
            var result = await _projectRepository.Add(project);
            if (result == 0)
            {
                throw new Exception("No se inserto el proyecto");
            }

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, Project project)
        {
            project.Id = id;
            var result = await _projectRepository.Update(project);
            if (result == 0)
            {
                throw new Exception("No se actualizo el Proyecto");
            }

            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                throw new Exception("No se encontro el usuario");
            }
            _projectRepository.DeleteEntity(project);

            return Ok(project);
        }
    }
}
