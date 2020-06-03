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
using pto.Models;

namespace pto.Controllers
{
    public class PersonalitiesController : ApiController
    {
        private ptoEntities db = new ptoEntities();

        // GET: api/Personalities
        public IQueryable<personality> Getpersonalities()
        {
            return db.personalities;
        }

        // GET: api/Personalities/5
        [ResponseType(typeof(personality))]
        public IHttpActionResult Getpersonality(int id)
        {
            personality personality = db.personalities.Find(id);
            if (personality == null)
            {
                return NotFound();
            }

            return Ok(personality);
        }

        // PUT: api/Personalities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpersonality(int id, personality personality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personality.id)
            {
                return BadRequest();
            }

            db.Entry(personality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personalityExists(id))
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

        // POST: api/Personalities
        [ResponseType(typeof(personality))]
        public IHttpActionResult Postpersonality(personality personality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.personalities.Add(personality);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personality.id }, personality);
        }

        // DELETE: api/Personalities/5
        [ResponseType(typeof(personality))]
        public IHttpActionResult Deletepersonality(int id)
        {
            personality personality = db.personalities.Find(id);
            if (personality == null)
            {
                return NotFound();
            }

            db.personalities.Remove(personality);
            db.SaveChanges();

            return Ok(personality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personalityExists(int id)
        {
            return db.personalities.Count(e => e.id == id) > 0;
        }
    }
}