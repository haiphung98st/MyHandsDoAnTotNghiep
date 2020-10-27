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
    public class tbl_DanhMucSanPhamController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_DanhMucSanPham
        public IQueryable<tbl_DanhMucSanPham> Gettbl_DanhMucSanPham()
        {
            return db.tbl_DanhMucSanPham;
        }

        // GET: api/tbl_DanhMucSanPham/5
        [ResponseType(typeof(tbl_DanhMucSanPham))]
        public IHttpActionResult Gettbl_DanhMucSanPham(long id)
        {
            tbl_DanhMucSanPham tbl_DanhMucSanPham = db.tbl_DanhMucSanPham.Find(id);
            if (tbl_DanhMucSanPham == null)
            {
                return NotFound();
            }

            return Ok(tbl_DanhMucSanPham);
        }

        // PUT: api/tbl_DanhMucSanPham/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_DanhMucSanPham(long id, tbl_DanhMucSanPham tbl_DanhMucSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_DanhMucSanPham.IDDanhMuc)
            {
                return BadRequest();
            }

            db.Entry(tbl_DanhMucSanPham).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_DanhMucSanPhamExists(id))
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

        // POST: api/tbl_DanhMucSanPham
        [ResponseType(typeof(tbl_DanhMucSanPham))]
        public IHttpActionResult Posttbl_DanhMucSanPham(tbl_DanhMucSanPham tbl_DanhMucSanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_DanhMucSanPham.Add(tbl_DanhMucSanPham);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_DanhMucSanPham.IDDanhMuc }, tbl_DanhMucSanPham);
        }

        // DELETE: api/tbl_DanhMucSanPham/5
        [ResponseType(typeof(tbl_DanhMucSanPham))]
        public IHttpActionResult Deletetbl_DanhMucSanPham(long id)
        {
            tbl_DanhMucSanPham tbl_DanhMucSanPham = db.tbl_DanhMucSanPham.Find(id);
            if (tbl_DanhMucSanPham == null)
            {
                return NotFound();
            }

            db.tbl_DanhMucSanPham.Remove(tbl_DanhMucSanPham);
            db.SaveChanges();

            return Ok(tbl_DanhMucSanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_DanhMucSanPhamExists(long id)
        {
            return db.tbl_DanhMucSanPham.Count(e => e.IDDanhMuc == id) > 0;
        }
    }
}