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
    public class AnswersController : ApiController
    {
        private ptoEntities db = new ptoEntities();

        // GET: api/answers
        public IQueryable<answer> Getanswers()
        {
            return db.answers;
        }

        // GET: api/answers/5
        [ResponseType(typeof(answer))]
        public IHttpActionResult Getanswer(int id)
        {
            answer answer = db.answers.Find(id);
            if (answer == null)
            {
                return NotFound();
            }

            return Ok(answer);
        }

        // PUT: api/answers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putanswer(int id, answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answer.id)
            {
                return BadRequest();
            }

            db.Entry(answer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!answerExists(id))
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

        // POST: api/answers
        [ResponseType(typeof(answer))]
        public IHttpActionResult Postanswer(answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.answers.Add(answer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = answer.id }, answer);
        }

        // DELETE: api/answers/5
        [ResponseType(typeof(answer))]
        public IHttpActionResult Deleteanswer(int id)
        {
            answer answer = db.answers.Find(id);
            if (answer == null)
            {
                return NotFound();
            }

            db.answers.Remove(answer);
            db.SaveChanges();

            return Ok(answer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool answerExists(int id)
        {
            return db.answers.Count(e => e.id == id) > 0;
        }
    }
}