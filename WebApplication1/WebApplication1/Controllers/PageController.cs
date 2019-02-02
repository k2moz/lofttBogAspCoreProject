using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuissnesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using static DataLayer.Enums.PageEnums;

namespace WebApplication1.Controllers
{
    public class PageController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public PageController(DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(dataManager);
        }
        public IActionResult Index(int pageId, PageType pageType)
        {
            PageViewModel _viewModel;
            switch (pageType)
            {
                case PageType.Directory: _viewModel = _servicesmanager.Directorys.DirectoryDBToViewModelById(pageId); break;
                case PageType.Material: _viewModel = _servicesmanager.Materials.MaterialDBModelToView(pageId); break;
                default: _viewModel = null; break;
            }
            ViewBag.PageType = pageType;
            return View(_viewModel);
        }

        [HttpGet]
        public IActionResult PageEditor(int pageId, PageType pageType, int directoryId = 0)
        {
            PageEditModel _editModel;

            switch (pageType)
            {
                case PageType.Directory:
                    if (pageId != 0) _editModel = _servicesmanager.Directorys.GetDirectoryEdetModel(pageId);
                    else _editModel = _servicesmanager.Directorys.CreateNewDirectoryEditModel();
                    break;
                case PageType.Material:
                    if (pageId != 0) _editModel = _servicesmanager.Materials.GetMaterialEdetModel(pageId);
                    else _editModel = _servicesmanager.Materials.CreateNewMaterialEditModel(directoryId);
                    break;
                default: _editModel = null; break;
            }

            ViewBag.PageType = pageType;
            return View(_editModel);
        }

        [HttpPost]
        public IActionResult SaveDirectory(DirectoryEditModel model)
        {
            _servicesmanager.Directorys.SaveDirectoryEditModelToDb(model);
            return RedirectToAction("PageEditor", "Page", new { pageId = model.Id, pageType=PageType.Directory });
        }

        [HttpPost]
        public IActionResult SaveMaterial (MaterialEditModel model)
        {
            _servicesmanager.Materials.SaveMaterialEditModelToDb(model);
            return RedirectToAction("PageEditor", "Page", new { pageId = model.Id, pageType=PageType.Material });
        }
    }
}