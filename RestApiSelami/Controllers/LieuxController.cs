using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiSelami.LieuxData;
using RestApiSelami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiSelami.Controllers
{
    
    [ApiController]
    public class LieuxsController : ControllerBase
    {
        private ILieuxData _lieuxData;
        public LieuxsController(ILieuxData lieuxData)
        {
            _lieuxData = lieuxData;
        }

        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetLieuxs()
        {
            return Ok(_lieuxData.GetLieuxs());

        }
        [HttpGet]
        [Route("api/[controller]/{id}")]

        public IActionResult GetLieux(Guid id)
        {
            var lieux = _lieuxData.GetLieux(id);
            if (lieux != null)
            {
                return Ok(lieux);
            }
            return NotFound($"lieux with id : {id} was not found ");
        }

        [HttpPost]
        [Route("api/[controller]")]

        public IActionResult GetLieux(Lieux lieux)
        {
            _lieuxData.AddLieux(lieux);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + lieux.Id,
                lieux);

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]

        public IActionResult DeleteLieux(Guid Id)
        {
            var lieux = _lieuxData.GetLieux(Id);
            if (lieux != null)
            {
                _lieuxData.DeleteLieux(lieux);
                return Ok();

            }
            return NotFound($"lieux with id : {Id} was not found ");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]

        public IActionResult EditLieux(Guid Id, Lieux lieux)
        {
            var existingLieux = _lieuxData.GetLieux(Id);
            if (existingLieux != null)
            {
                lieux.Id = existingLieux.Id;
                _lieuxData.EditLieux(lieux);


            }
            return Ok(lieux);
        }


    }

}

