﻿using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new LienHeDAO().getThongTinLienHe();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new tbl_Feedback();
            feedback.sTenKH = name;
            feedback.sEmail = email;
            feedback.dNgayTao = DateTime.Now;
            feedback.sSoDienThoai = mobile;
            feedback.sNoiDung = content;
            feedback.sDiaChi = address;

            var id = new LienHeDAO().InsertFeedBack(feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                //send mail
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}