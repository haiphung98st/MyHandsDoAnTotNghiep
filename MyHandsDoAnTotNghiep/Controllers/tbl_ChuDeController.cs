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
    public class tbl_ChuDeController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_ChuDe
        public IQueryable<tbl_ChuDe> Gettbl_ChuDe()
        {
            return db.tbl_ChuDe;
        }

        // GET: api/tbl_ChuDe/5
        [ResponseType(typeof(tbl_ChuDe))]
        public IHttpActionResult Gettbl_ChuDe(long id)
        {
            tbl_ChuDe tbl_ChuDe = db.tbl_ChuDe.Find(id);
            if (tbl_ChuDe == null)
            {
                return NotFound();
            }

            return Ok(tbl_ChuDe);
        }

        // PUT: api/tbl_ChuDe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ChuDe(long id, tbl_ChuDe tbl_ChuDe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ChuDe.IDChuDe)
            {
                return BadRequest();
            }

            db.Entry(tbl_ChuDe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ChuDeExists(id))
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

        // POST: api/tbl_ChuDe
        [ResponseType(typeof(tbl_ChuDe))]
        public IHttpActionResult Posttbl_ChuDe(tbl_ChuDe tbl_ChuDe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ChuDe.Add(tbl_ChuDe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_ChuDe.IDChuDe }, tbl_ChuDe);
        }

        // DELETE: api/tbl_ChuDe/5
        [ResponseType(typeof(tbl_ChuDe))]
        public IHttpActionResult Deletetbl_ChuDe(long id)
        {
            tbl_ChuDe tbl_ChuDe = db.tbl_ChuDe.Find(id);
            if (tbl_ChuDe == null)
            {
                return NotFound();
            }

            db.tbl_ChuDe.Remove(tbl_ChuDe);
            db.SaveChanges();

            return Ok(tbl_ChuDe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ChuDeExists(long id)
        {
            return db.tbl_ChuDe.Count(e => e.IDChuDe == id) > 0;
        }
    }
}