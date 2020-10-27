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
    public class tbl_SanPhamController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/tbl_SanPham
        //[Route("api/products/getAllProduct")]
        [HttpGet]
        public IQueryable<tbl_SanPham> Gettbl_SanPham()
        {
            return db.tbl_SanPham;
        }
        //[Route("api/products/getProductByID/{id}")]
        [HttpGet]
        // GET: api/tbl_SanPham/5
        [ResponseType(typeof(tbl_SanPham))]
        public IHttpActionResult Gettbl_SanPham(long id)
        {
            tbl_SanPham tbl_SanPham = db.tbl_SanPham.Find(id);
            if (tbl_SanPham == null)
            {
                return NotFound();
            }

            return Ok(tbl_SanPham);
        }
        
        [HttpPut]
        // PUT: api/tbl_SanPham/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_SanPham(long id, tbl_SanPham tbl_SanPham)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_SanPham.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_SanPham).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_SanPhamExists(id))
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
        
        [HttpPost]
        // POST: api/tbl_SanPham
        [ResponseType(typeof(tbl_SanPham))]
        public IHttpActionResult Posttbl_SanPham(tbl_SanPham tbl_SanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_SanPham.Add(tbl_SanPham);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_SanPham.ID }, tbl_SanPham);
        }
        //[Route("api/products/deleteProductByID/{id}")]
        [HttpDelete]
        
        // DELETE: api/tbl_SanPham/5
        [ResponseType(typeof(tbl_SanPham))]
        public IHttpActionResult Deletetbl_SanPham(long id)
        {
            tbl_SanPham tbl_SanPham = db.tbl_SanPham.Find(id);
            if (tbl_SanPham == null)
            {
                return NotFound();
            }

            db.tbl_SanPham.Remove(tbl_SanPham);
            db.SaveChanges();

            return Ok(tbl_SanPham);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_SanPhamExists(long id)
        {
            return db.tbl_SanPham.Count(e => e.ID == id) > 0;
        }
    }
}