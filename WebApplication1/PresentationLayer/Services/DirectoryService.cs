using BuissnesLayer;
using DataLayer.Entityes;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PresentationLayer.Services
{
    public class DirectoryService
    {
        private DataManager _dataManager;
        private MaterialService _materialService;
        public DirectoryService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _materialService = new MaterialService(dataManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DirectoryViewModel> GetDirectoryesList()
        {
            var _dirs = _dataManager.Directorys.GetAllDirectorys();
            List<DirectoryViewModel> _modelsList = new List<DirectoryViewModel>();
            foreach (var item in _dirs)
            {
                _modelsList.Add(DirectoryDBToViewModelById(item.Id));
            }
            return _modelsList;
        }

        public DirectoryViewModel DirectoryDBToViewModelById(int directoryId)
        {
            var _directory = _dataManager.Directorys.GetDirectoryById(directoryId, true);

            List<MaterialViewModel> _materialsViewModelList = new List<MaterialViewModel>();
            foreach (var item in _directory.Materials)
            {
                _materialsViewModelList.Add(_materialService.MaterialDBModelToView(item.Id));
            }
            return new DirectoryViewModel() { Directory = _directory, Materials = _materialsViewModelList };
        }
        public DirectoryEditModel GetDirectoryEdetModel(int directoryid = 0)
        {
            if (directoryid != 0)
            {
                var _dirDB = _dataManager.Directorys.GetDirectoryById(directoryid);
                var _dirEditModel = new DirectoryEditModel() {
                    Id = _dirDB.Id,
                    Title = _dirDB.Title,
                    Html = _dirDB.Html
                };
                return _dirEditModel;
            }
            else { return new DirectoryEditModel() { }; }
        }
        public DirectoryViewModel SaveDirectoryEditModelToDb(DirectoryEditModel directoryEditModel)
        {
            Directory _directoryDbModel;
            if(directoryEditModel.Id!=0)
            {
                _directoryDbModel = _dataManager.Directorys.GetDirectoryById(directoryEditModel.Id);
            }
            else
            {
                _directoryDbModel = new Directory();
            }
            _directoryDbModel.Title = directoryEditModel.Title;
            _directoryDbModel.Html = directoryEditModel.Html;

            _dataManager.Directorys.SaveDirectory(_directoryDbModel);

            return DirectoryDBToViewModelById(_directoryDbModel.Id);
        }

        public DirectoryEditModel CreateNewDirectoryEditModel()
        {
            return new DirectoryEditModel() { };
        }
    }
}
