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
    public class tbl_TaiKhoanController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_TaiKhoan
        public IQueryable<tbl_TaiKhoan> Gettbl_TaiKhoan()
        {
            return db.tbl_TaiKhoan;
        }

        // GET: api/tbl_TaiKhoan/5
        [ResponseType(typeof(tbl_TaiKhoan))]
        public IHttpActionResult Gettbl_TaiKhoan(long id)
        {
            tbl_TaiKhoan tbl_TaiKhoan = db.tbl_TaiKhoan.Find(id);
            if (tbl_TaiKhoan == null)
            {
                return NotFound();
            }

            return Ok(tbl_TaiKhoan);
        }

        // PUT: api/tbl_TaiKhoan/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_TaiKhoan(long id, tbl_TaiKhoan tbl_TaiKhoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_TaiKhoan.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_TaiKhoan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_TaiKhoanExists(id))
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

        // POST: api/tbl_TaiKhoan
        [ResponseType(typeof(tbl_TaiKhoan))]
        public IHttpActionResult Posttbl_TaiKhoan(tbl_TaiKhoan tbl_TaiKhoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_TaiKhoan.Add(tbl_TaiKhoan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_TaiKhoan.ID }, tbl_TaiKhoan);
        }

        // DELETE: api/tbl_TaiKhoan/5
        [ResponseType(typeof(tbl_TaiKhoan))]
        public IHttpActionResult Deletetbl_TaiKhoan(long id)
        {
            tbl_TaiKhoan tbl_TaiKhoan = db.tbl_TaiKhoan.Find(id);
            if (tbl_TaiKhoan == null)
            {
                return NotFound();
            }

            db.tbl_TaiKhoan.Remove(tbl_TaiKhoan);
            db.SaveChanges();

            return Ok(tbl_TaiKhoan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_TaiKhoanExists(long id)
        {
            return db.tbl_TaiKhoan.Count(e => e.ID == id) > 0;
        }
    }
}