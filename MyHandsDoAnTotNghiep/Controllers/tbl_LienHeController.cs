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
using Model.EF;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class tbl_LienHeController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_LienHe
        public IQueryable<tbl_LienHe> Gettbl_LienHe()
        {
            return db.tbl_LienHe;
        }

        // GET: api/tbl_LienHe/5
        [ResponseType(typeof(tbl_LienHe))]
        public IHttpActionResult Gettbl_LienHe(long id)
        {
            tbl_LienHe tbl_LienHe = db.tbl_LienHe.Find(id);
            if (tbl_LienHe == null)
            {
                return NotFound();
            }

            return Ok(tbl_LienHe);
        }

        // PUT: api/tbl_LienHe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_LienHe(long id, tbl_LienHe tbl_LienHe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_LienHe.IDLienHe)
            {
                return BadRequest();
            }

            db.Entry(tbl_LienHe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_LienHeExists(id))
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

        // POST: api/tbl_LienHe
        [ResponseType(typeof(tbl_LienHe))]
        public IHttpActionResult Posttbl_LienHe(tbl_LienHe tbl_LienHe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_LienHe.Add(tbl_LienHe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_LienHe.IDLienHe }, tbl_LienHe);
        }

        // DELETE: api/tbl_LienHe/5
        [ResponseType(typeof(tbl_LienHe))]
        public IHttpActionResult Deletetbl_LienHe(long id)
        {
            tbl_LienHe tbl_LienHe = db.tbl_LienHe.Find(id);
            if (tbl_LienHe == null)
            {
                return NotFound();
            }

            db.tbl_LienHe.Remove(tbl_LienHe);
            db.SaveChanges();

            return Ok(tbl_LienHe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_LienHeExists(long id)
        {
            return db.tbl_LienHe.Count(e => e.IDLienHe == id) > 0;
        }
    }
}