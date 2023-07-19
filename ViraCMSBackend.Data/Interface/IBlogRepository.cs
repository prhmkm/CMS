﻿using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IBlogRepository
    {
        void Add(Blog blog);
        void Edit(Blog blog);
        Blog GetById(int id);
        Blog GetByAlias(string alias);
        List<ShowBlogs> GetAll(int id);
        List<BOShowBlogs> BOGetAll(int id);
    }
}
