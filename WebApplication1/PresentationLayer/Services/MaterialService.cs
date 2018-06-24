using BuissnesLayer;
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
            var _dir = dataManager.Directorys.GetDirectoryById(_model.Material.DirectoryId);

            if (_dir.Materials.IndexOf(_model.Material) != _dir.Materials.Count() - 1)
            {
                _model.NextMaterial = _dir.Materials.ElementAt(_dir.Materials.IndexOf(_model.Material) + 1);
            }
            return _model;
        }


    }
}
