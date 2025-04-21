using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _toDoRepository;

        public ToDoController(ToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        // GET: api/<ToDoController>
        [HttpGet]
        public IActionResult Get()
        {
            var toDo=_toDoRepository.GetAll();
            return Ok(toDo);
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result= _toDoRepository.Get(id);
            return Ok(result);
        }

        // POST api/<ToDoController>
        [HttpPost]
        public IActionResult Post([FromBody] AddToDoDto command)
        {
            var toDoId=_toDoRepository.Add(command);
            string url = Url.Action(nameof(Get), "ToDo", new { Id = toDoId }, Request.Scheme);
            return Created(url,true);
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ToDoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
