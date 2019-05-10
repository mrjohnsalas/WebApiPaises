using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/Pais/{PaisId}/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Provincia>> GetAll(int paisId)
        {
            return context.Provincias.Where(x => x.PaisId == paisId).ToList();
        }

        [HttpGet("{id}", Name = "getProvincia")]
        public ActionResult GetById(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id.Equals(id));

            if (provincia == null)
                return NotFound();

            return new ObjectResult(provincia);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Provincia provincia, int paisId)
        {
            provincia.PaisId = paisId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Provincias.Add(provincia);
            context.SaveChanges();

            return new CreatedAtRouteResult("getProvincia", new { id = provincia.Id }, provincia);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
                return BadRequest();

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id.Equals(id));

            if (provincia == null)
                return NotFound();

            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }
    }
}