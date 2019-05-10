using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pais>> GetAll()
        {
            return context.Paises.ToList();
        }

        [HttpGet("{id}", Name = "getPais")]
        public ActionResult Get(int id)
        {
            var pais = context.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                return new CreatedAtRouteResult("getPais", new { id = pais.Id }, pais);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
                return BadRequest();

            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);

            if (pais == null)
                return NotFound();

            context.Paises.Remove(pais);
            context.SaveChanges();
            return Ok(pais);
        }
    }
}