using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueyDataService<T> _nonQueryDataService;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueyDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity) =>await _nonQueryDataService.Create(entity);
        public async Task<bool> Delete(int id) => await _nonQueryDataService.Delete(id);
        public async Task<T> Update(int id, T entity) => await _nonQueryDataService.Update(id, entity);
        
        public async Task<T?> Get(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }
    }
}
