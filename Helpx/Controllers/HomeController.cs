using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.DAO;

namespace Helpx.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Home()
        {
            if (Session["codUser"] == null || (int)Session["codUser"] == 0) 
                return Redirect("/Login/Login");

            var lst = new SolicitacoesDAO().ConsultarChamados();

            ViewBag.EmAtendimento = lst.Where(a => a.StatusChamadoId == 1).ToList().Count();
            ViewBag.Pendente = lst.Where(a => a.StatusChamadoId == 2).ToList().Count();
            ViewBag.Resolvidos = lst.Where(a => a.StatusChamadoId == 3).ToList().Count();
            ViewBag.Cancelados = lst.Where(a => a.StatusChamadoId == 4).ToList().Count();
            return View();
        }

    }
}
