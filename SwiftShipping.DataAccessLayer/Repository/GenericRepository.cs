﻿using Microsoft.EntityFrameworkCore;
using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Repository
{
    public class GenericRepository<TEntity> where TEntity:class
    {
        ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>();
            return query.ToList();
        }


        public void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity GetFirstByFilter(Func<TEntity, bool> filter)
        {
            return _context.Set<TEntity>().FirstOrDefault(filter);
        }



    }
}
