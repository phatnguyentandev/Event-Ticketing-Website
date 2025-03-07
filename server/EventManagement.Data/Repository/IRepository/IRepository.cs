﻿using EventManagement.Data.Helpers;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string? includeProperties = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<PagedList<T>> GetPagedAllAsync(Expression<Func<T, bool>>? filter = null
            , List<(Expression<Func<T, object>> orderBy, bool isDescending)>? orderByList = null
            , string? includeProperties = null
            , int pageSize = 0, int pageNumber = 1);
        Task CreateAsync(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> listEntity);
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
