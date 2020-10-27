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
    public class tbl_ChiTietHoaDonController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_ChiTietHoaDon
        public IQueryable<tbl_ChiTietHoaDon> Gettbl_ChiTietHoaDon()
        {
            return db.tbl_ChiTietHoaDon;
        }

        // GET: api/tbl_ChiTietHoaDon/5
        [ResponseType(typeof(tbl_ChiTietHoaDon))]
        public IHttpActionResult Gettbl_ChiTietHoaDon(long id)
        {
            tbl_ChiTietHoaDon tbl_ChiTietHoaDon = db.tbl_ChiTietHoaDon.Find(id);
            if (tbl_ChiTietHoaDon == null)
            {
                return NotFound();
            }

            return Ok(tbl_ChiTietHoaDon);
        }

        // PUT: api/tbl_ChiTietHoaDon/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ChiTietHoaDon(long id, tbl_ChiTietHoaDon tbl_ChiTietHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ChiTietHoaDon.IDSanPham)
            {
                return BadRequest();
            }

            db.Entry(tbl_ChiTietHoaDon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ChiTietHoaDonExists(id))
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

        // POST: api/tbl_ChiTietHoaDon
        [ResponseType(typeof(tbl_ChiTietHoaDon))]
        public IHttpActionResult Posttbl_ChiTietHoaDon(tbl_ChiTietHoaDon tbl_ChiTietHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ChiTietHoaDon.Add(tbl_ChiTietHoaDon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ChiTietHoaDonExists(tbl_ChiTietHoaDon.IDSanPham))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_ChiTietHoaDon.IDSanPham }, tbl_ChiTietHoaDon);
        }

        // DELETE: api/tbl_ChiTietHoaDon/5
        [ResponseType(typeof(tbl_ChiTietHoaDon))]
        public IHttpActionResult Deletetbl_ChiTietHoaDon(long id)
        {
            tbl_ChiTietHoaDon tbl_ChiTietHoaDon = db.tbl_ChiTietHoaDon.Find(id);
            if (tbl_ChiTietHoaDon == null)
            {
                return NotFound();
            }

            db.tbl_ChiTietHoaDon.Remove(tbl_ChiTietHoaDon);
            db.SaveChanges();

            return Ok(tbl_ChiTietHoaDon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ChiTietHoaDonExists(long id)
        {
            return db.tbl_ChiTietHoaDon.Count(e => e.IDSanPham == id) > 0;
        }
    }
}