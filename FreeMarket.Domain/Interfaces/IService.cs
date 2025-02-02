﻿using FreeMarket.Domain.Classes;

namespace FreeMarket.Domain.Interfaces
{
    public interface IService<T> where T : class?
    {
        public Task<ServiceResponse<List<T>>> FindAll();
        public Task<ServiceResponse<T>> FindOne(int id);
        public Task<ServiceResponse<T>> Upsert(T entity);
        public Task<ServiceResponse<object>> Delete(int id);
    }
}
