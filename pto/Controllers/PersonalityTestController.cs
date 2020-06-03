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
    public class PersonalityTestController : ApiController
    {
        private ptoEntities db = new ptoEntities();

        // GET: api/PersonalityTest
        public IQueryable<personality_test> Getpersonality_test()
        {
            return db.personality_test;
        }

        // GET: api/PersonalityTest/5
        [ResponseType(typeof(personality_test))]
        public IHttpActionResult Getpersonality_test(int id)
        {
            personality_test personality_test = db.personality_test.Find(id);
            if (personality_test == null)
            {
                return NotFound();
            }

            return Ok(personality_test);
        }

        // PUT: api/PersonalityTest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpersonality_test(int id, personality_test personality_test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personality_test.id)
            {
                return BadRequest();
            }

            db.Entry(personality_test).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personality_testExists(id))
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

        // POST: api/PersonalityTest
        [ResponseType(typeof(personality_test))]
        public IHttpActionResult Postpersonality_test(personality_test personality_test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.personality_test.Add(personality_test);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personality_test.id }, personality_test);
        }

        // DELETE: api/PersonalityTest/5
        [ResponseType(typeof(personality_test))]
        public IHttpActionResult Deletepersonality_test(int id)
        {
            personality_test personality_test = db.personality_test.Find(id);
            if (personality_test == null)
            {
                return NotFound();
            }

            db.personality_test.Remove(personality_test);
            db.SaveChanges();

            return Ok(personality_test);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personality_testExists(int id)
        {
            return db.personality_test.Count(e => e.id == id) > 0;
        }
    }
}