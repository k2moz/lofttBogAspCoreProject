using BuissnesLayer;
using DataLayer.Entityes;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationLayer.Services
{
    public class MaterialService
    {
        private DataManager dataManager;
        public MaterialService(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public MaterialViewModel MaterialDBModelToView(int materialId)
        {
            var _model = new MaterialViewModel()
            {
                Material = dataManager.Materials.GetMaterialById(materialId),
            };
            var _dir = dataManager.Directorys.GetDirectoryById(_model.Material.DirectoryId,true);

            if(_dir.Materials.IndexOf(_dir.Materials.FirstOrDefault(x=>x.Id== _model.Material.Id)) != _dir.Materials.Count() - 1)
            {
                _model.NextMaterial = _dir.Materials.ElementAt(_dir.Materials.IndexOf(_dir.Materials.FirstOrDefault(x => x.Id == _model.Material.Id)) + 1);
            }
            return _model;
        }

        public MaterialEditModel GetMaterialEdetModel(int materialId)
        {
            var _dbModel = dataManager.Materials.GetMaterialById(materialId);
            var _editModel = new MaterialEditModel() {
                Id = _dbModel.Id = _dbModel.Id,
                Directoryid = _dbModel.DirectoryId,
                Title=_dbModel.Title,
                Html=_dbModel.Html
            };
            return _editModel;
        }

        public MaterialViewModel SaveMaterialEditModelToDb(MaterialEditModel editModel)
        {
            Material material;
            if(editModel.Id!=0)
            {
                material = dataManager.Materials.GetMaterialById(editModel.Id);
            }
            else
            {
                material = new Material();
            }
            material.Title = editModel.Title;
            material.Html = editModel.Html;
            material.DirectoryId = editModel.Directoryid;
            dataManager.Materials.SaveMaterial(material);
            return MaterialDBModelToView(material.Id);
        }
        public MaterialEditModel CreateNewMaterialEditModel(int directoryId)
        {
            return new MaterialEditModel() { Directoryid = directoryId };
        }

    }
}
