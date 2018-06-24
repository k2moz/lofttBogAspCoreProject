using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuissnesLayer;
using BuissnesLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private EFDBContext _context;
        //private IDirectorysRepository _dirRep;
        private DataManager _datamanager;
        public HomeController(/*EFDBContext context, IDirectorysRepository dirRep, */DataManager dataManager)
        {
            //_context = context;
            //dirRep = _dirRep;
            _datamanager = dataManager;
        }

        public IActionResult Index()
        {
            HelloModel _model = new HelloModel() { HelloMessage = "Hey Aleksander!" };
            //List<Directory> _dirs = _context.Directory.Include(x=>x.Materials).ToList();
            //List<Directory> _dirs = _dirRep.GetAllDirectorys().ToList();
            List<Directory> _dirs = _datamanager.Directorys.GetAllDirectorys(true).ToList();
            return View(_dirs);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
