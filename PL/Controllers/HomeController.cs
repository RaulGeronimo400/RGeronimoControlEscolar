﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login(ML.Usuario usuario)
        {
            ServiceLogin.LoginClient service = new ServiceLogin.LoginClient();
            var result = service.SingIn(usuario);
            //ML.Result result = BL.Usuario.GetByNombre(usuario);
            if (result.Correct)
            {
                Session["Usuario"] = usuario.Nombre;
                return View("Index");
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ML.Usuario usuario)
        {
            ServiceLogin.LoginClient service = new ServiceLogin.LoginClient();
            var result = service.Registrar(usuario);
            if (result.Correct)
            {
                return View("Login");
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }
}