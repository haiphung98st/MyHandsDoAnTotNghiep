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
    public class tbl_HoaDonController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_HoaDon
        public IQueryable<tbl_HoaDon> Gettbl_HoaDon()
        {
            return db.tbl_HoaDon;
        }

        // GET: api/tbl_HoaDon/5
        [ResponseType(typeof(tbl_HoaDon))]
        public IHttpActionResult Gettbl_HoaDon(long id)
        {
            tbl_HoaDon tbl_HoaDon = db.tbl_HoaDon.Find(id);
            if (tbl_HoaDon == null)
            {
                return NotFound();
            }

            return Ok(tbl_HoaDon);
        }

        // PUT: api/tbl_HoaDon/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_HoaDon(long id, tbl_HoaDon tbl_HoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_HoaDon.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_HoaDon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_HoaDonExists(id))
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

        // POST: api/tbl_HoaDon
        [ResponseType(typeof(tbl_HoaDon))]
        public IHttpActionResult Posttbl_HoaDon(tbl_HoaDon tbl_HoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_HoaDon.Add(tbl_HoaDon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_HoaDon.ID }, tbl_HoaDon);
        }

        // DELETE: api/tbl_HoaDon/5
        [ResponseType(typeof(tbl_HoaDon))]
        public IHttpActionResult Deletetbl_HoaDon(long id)
        {
            tbl_HoaDon tbl_HoaDon = db.tbl_HoaDon.Find(id);
            if (tbl_HoaDon == null)
            {
                return NotFound();
            }

            db.tbl_HoaDon.Remove(tbl_HoaDon);
            db.SaveChanges();

            return Ok(tbl_HoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_HoaDonExists(long id)
        {
            return db.tbl_HoaDon.Count(e => e.ID == id) > 0;
        }
    }
}