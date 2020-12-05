using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WSFinal.Models;

namespace WSFinal.Controllers
{
    [RoutePrefix("api")]
    public class FinalsController : ApiController
    {
        
        private DataContext db = new DataContext();

        // GET: api/Finals
        public IQueryable<Final> GetFinals()
        {
            return db.Finals;
        }

        // GET: api/Finals/5
        [ResponseType(typeof(Final))]
        public IHttpActionResult GetFinal(int id)
        {
            Final final = db.Finals.Find(id);
            if (final == null)
            {
                return NotFound();
            }

            return Ok(final);
        }

        // PUT: api/Finals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFinal(int id, Final final)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != final.numero)
            {
                return BadRequest();
            }

            db.Entry(final).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Finals
        [ResponseType(typeof(Final))]
        public IHttpActionResult PostFinal(Final final)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Finals.Add(final);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = final.numero }, final);
        }
        [HttpGet]
        [Route("{id:int}")]
        public string Operacion(int id)
        {
            if (id < 0)
                return "Error";
            if (id == 0)
                return "Realizado por Enrique";
            return "Usted ingreso el numero " + id;
        }
        // DELETE: api/Finals/5
        [ResponseType(typeof(Final))]
        public IHttpActionResult DeleteFinal(int id)
        {
            Final final = db.Finals.Find(id);
            if (final == null)
            {
                return NotFound();
            }

            db.Finals.Remove(final);
            db.SaveChanges();

            return Ok(final);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinalExists(int id)
        {
            return db.Finals.Count(e => e.numero == id) > 0;
        }
    }
}