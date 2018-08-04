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
        public IActionResult PageEditor(int pageId, PageType pageType)
        {
            PageEditModel _editModel;
            switch (pageType)
            {
                case PageType.Directory: _editModel = _servicesmanager.Directorys.GetDirectoryEdetModel(pageId);break;
                case PageType.Material: _editModel = _servicesmanager.Materials.GetMaterialEdetModel(pageId); break;
                default: _editModel = null;break;
            }
            ViewBag.PageType = pageType;
            return View(_editModel);
        }
    }
}