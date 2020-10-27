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
    public class tbl_GioiThieuController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_GioiThieu
        public IQueryable<tbl_GioiThieu> Gettbl_GioiThieu()
        {
            return db.tbl_GioiThieu;
        }

        // GET: api/tbl_GioiThieu/5
        [ResponseType(typeof(tbl_GioiThieu))]
        public IHttpActionResult Gettbl_GioiThieu(long id)
        {
            tbl_GioiThieu tbl_GioiThieu = db.tbl_GioiThieu.Find(id);
            if (tbl_GioiThieu == null)
            {
                return NotFound();
            }

            return Ok(tbl_GioiThieu);
        }

        // PUT: api/tbl_GioiThieu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_GioiThieu(long id, tbl_GioiThieu tbl_GioiThieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_GioiThieu.IDGioiThieu)
            {
                return BadRequest();
            }

            db.Entry(tbl_GioiThieu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_GioiThieuExists(id))
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

        // POST: api/tbl_GioiThieu
        [ResponseType(typeof(tbl_GioiThieu))]
        public IHttpActionResult Posttbl_GioiThieu(tbl_GioiThieu tbl_GioiThieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_GioiThieu.Add(tbl_GioiThieu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_GioiThieu.IDGioiThieu }, tbl_GioiThieu);
        }

        // DELETE: api/tbl_GioiThieu/5
        [ResponseType(typeof(tbl_GioiThieu))]
        public IHttpActionResult Deletetbl_GioiThieu(long id)
        {
            tbl_GioiThieu tbl_GioiThieu = db.tbl_GioiThieu.Find(id);
            if (tbl_GioiThieu == null)
            {
                return NotFound();
            }

            db.tbl_GioiThieu.Remove(tbl_GioiThieu);
            db.SaveChanges();

            return Ok(tbl_GioiThieu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_GioiThieuExists(long id)
        {
            return db.tbl_GioiThieu.Count(e => e.IDGioiThieu == id) > 0;
        }
    }
}