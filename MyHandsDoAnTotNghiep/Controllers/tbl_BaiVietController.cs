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
    public class tbl_BaiVietController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_BaiViet
        public IQueryable<tbl_BaiViet> Gettbl_BaiViet()
        {
            return db.tbl_BaiViet;
        }

        // GET: api/tbl_BaiViet/5
        [ResponseType(typeof(tbl_BaiViet))]
        public IHttpActionResult Gettbl_BaiViet(long id)
        {
            tbl_BaiViet tbl_BaiViet = db.tbl_BaiViet.Find(id);
            if (tbl_BaiViet == null)
            {
                return NotFound();
            }

            return Ok(tbl_BaiViet);
        }

        // PUT: api/tbl_BaiViet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_BaiViet(long id, tbl_BaiViet tbl_BaiViet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_BaiViet.IDBaiViet)
            {
                return BadRequest();
            }

            db.Entry(tbl_BaiViet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_BaiVietExists(id))
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

        // POST: api/tbl_BaiViet
        [ResponseType(typeof(tbl_BaiViet))]
        public IHttpActionResult Posttbl_BaiViet(tbl_BaiViet tbl_BaiViet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_BaiViet.Add(tbl_BaiViet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_BaiViet.IDBaiViet }, tbl_BaiViet);
        }

        // DELETE: api/tbl_BaiViet/5
        [ResponseType(typeof(tbl_BaiViet))]
        public IHttpActionResult Deletetbl_BaiViet(long id)
        {
            tbl_BaiViet tbl_BaiViet = db.tbl_BaiViet.Find(id);
            if (tbl_BaiViet == null)
            {
                return NotFound();
            }

            db.tbl_BaiViet.Remove(tbl_BaiViet);
            db.SaveChanges();

            return Ok(tbl_BaiViet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_BaiVietExists(long id)
        {
            return db.tbl_BaiViet.Count(e => e.IDBaiViet == id) > 0;
        }
    }
}