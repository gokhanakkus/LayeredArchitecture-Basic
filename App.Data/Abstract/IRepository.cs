using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Abstract;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IQueryable<T> Get();
    T Create(T entity);
    T Update(int id, T entity);
    bool Delete(int id);
    T Remove(T entity);
}