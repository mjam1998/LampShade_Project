﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/ToDo/{ToDoId}/Categories/{CategoryId}")]
    [ApiController]
    public class ToDoCategoryController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(int ToDoId,int CategoryId)
        {
            return Ok();
        }
    }
}
