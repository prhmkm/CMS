﻿using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class ColorService : IColorService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public ColorService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Color color)
        {
            _repository.Color.Add(color);
        }

        public void Edit(Color color)
        {
            _repository.Color.Edit(color);
        }

        public Color GetById(int id)
        {
            return _repository.Color.GetById(id);
        }

        public List<Color> GetAll()
        {
            return _repository.Color.GetAll(); 
        }
    }
}
