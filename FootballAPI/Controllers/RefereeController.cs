﻿using FootballAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        readonly FootballContext footballContext;
        public RefereeController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Referee>> Get()
        {
            return this.Ok(footballContext.Referees);
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public ActionResult GetById(int id)
        {
            var response = footballContext.Referees.Find(id);
            if (response == default)
                this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public ActionResult Post(Referee referee)
        {
            var response = footballContext.Referees.Add(referee).Entity;
            return this.CreatedAtAction("GetById", response.Id, response);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Referee referee)
        {
            if (footballContext.Referees.Find(id) == default)
                return this.NotFound();

            footballContext.Referees.Update(referee);        
            return this.Ok();
        }
    }
}
