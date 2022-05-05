
using Licenta.Data;
using Licenta.Models.Base;
using Licenta.Repositories.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Implementarea metodelor de Crud
namespace Licenta.Repositories.Generic_Repository
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        //Contextul este oferit automat de .Net prin dependecy injection

        protected readonly Context _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(Context context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public TEntity GetById(string id)
        {
            return _table.FirstOrDefault(x => x.Id.ToString().Equals(id));

        }


        // Get all

        public async Task<List<TEntity>> GetAll()
        {
            // the select to the db 
            return await _table.AsNoTracking().ToListAsync();
            //AsNoTracking limiteaza .Net-ul sa caute modificarile, deoarece facem doar un get, nu si modificari
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _table.AsNoTracking();

            // try not to use toList, count etc, before filtering the data
            // var entityList = _table.ToList();
            // var entityListFiltered = _table.Where(x => x.Id.ToString() != "");

            // better version; the data is filtered in the query 
            // select * from entity where Id is not null
            // var entitylistFiltered = _table.Where(x => x.Id.ToString() != "").ToList();
        }


        // Create

        public void Create(TEntity entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(TEntity entity)
        {
            entity.DateCreated = DateTime.Now;
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities) // folosit pentru adaugarea mai multorr elemente
        {
            await _table.AddRangeAsync(entities);
        }

        // Update

        public void Update(TEntity entity)
        {
           // entity.DateCreated
            entity.DateModified= DateTime.Now;
            _table.Update(entity);

        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }

        // Delete

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }

        // Find

        public TEntity FindById(object id)
        {
            return _table.Find(id);

            // another option
            // return _table.FirstOrDefault(x=> x.Id.Equals(id));
        }

        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await _table.FindAsync(id);

            // another option
            // return await _table.FirstOrDefaultAsync(x=> x.Id.Equals(id));
        }


        // Save
        public bool Save()
        {
            try
            {
            return _context.SaveChanges() > 0;
            }
           catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
            return await _context.SaveChangesAsync() > 0;
            }
            catch(SqlException ex)
            {
               Console.WriteLine(ex);
            }

             return false;
        }
    }
}

