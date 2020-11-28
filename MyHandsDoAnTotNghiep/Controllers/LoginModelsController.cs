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
using Model.DAO;
using Model.EF;
using MyHandsDoAnTotNghiep.Common;
using MyHandsDoAnTotNghiep.Models;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class LoginModelsController : ApiController
    {
        private MyHandsDbContext db = new MyHandsDbContext();

        // GET: api/LoginModels

        public int GetLoginModels(LoginModel loginModel)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                result = dao.Login(loginModel.sTenTaiKhoanLogin, Encryptor.MD5Hash(loginModel.sMatKhauLogin));
                // return 1;//success
                //return 0; //tài khoản ko tồn tại
                //return -2; //mksai
                //return -1; //tk bị khóa
                //return 1;//đăng nhập thành công
                //return -3;//exception
            }
            return result;
        } 
    }
}