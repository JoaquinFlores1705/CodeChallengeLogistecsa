using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApi.Dtos;
using WebApi.Errors;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    public class CommentController : BaseApiController
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(IGenericRepository<Comment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Comment>>> GetComments()
        {
            var spec = new CommentWithUserAndTaskSpecification();
            var comments = await _commentRepository.GetAllWithSpec(spec);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var spec = new CommentWithUserAndTaskSpecification(id);
            var comment = await _commentRepository.GetByIdWithSpec(spec);

            if (comment == null)
            {
                return NotFound(new CodeErrorResponse(404, "El comentario no existe"));
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Post(Comment comment)
        {
            var result = await _commentRepository.Add(comment);
            if (result == 0)
            {
                throw new Exception("No se inserto el comentario");
            }

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> Put(int id, Comment comment)
        {
            comment.Id = id;
            var result = await _commentRepository.Update(comment);
            if (result == 0)
            {
                throw new Exception("No se actualizo el comentario");
            }

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.Entities.Task>> Delete(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                throw new Exception("No se encontro el comentario");
            }
            _commentRepository.DeleteEntity(comment);

            return Ok(comment);
        }
    }
}
