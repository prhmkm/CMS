using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class BlogService : IBlogService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public BlogService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Blog blog)
        {
            _repository.Blog.Add(blog);
        }

        public void Edit(Blog blog)
        {
            _repository.Blog.Edit(blog);
        }

        public Blog GetById(int id)
        {
            return _repository.Blog.GetById(id);    
        }

        public List<BlogDTO.ShowBlogs> GetAll(int id)
        {
            return _repository.Blog.GetAll(id);
        }

        public List<BlogDTO.BOShowBlogs> BOGetAll(int id)
        {
            return _repository.Blog.BOGetAll(id);
        }

        public Blog GetByAlias(string alias)
        {
            return _repository.Blog.GetByAlias(alias);
        }
    }
}
