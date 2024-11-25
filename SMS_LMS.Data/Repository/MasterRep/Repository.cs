using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SMS_LMS.Models.DataModels.PagedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.Repository.MasterRep
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SMS_LMS_DBContext _db;
        internal DbSet<T> dbSet;
        private readonly IMapper _mapper;

        public Repository(SMS_LMS_DBContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this.dbSet = _db.Set<T>();
        }

        public int count(Expression<Func<T, bool>> filtter = null)
        {
            if (filtter != null)
            {
                return dbSet.Where(filtter).Count();
            }
            return dbSet.Count();
        }

        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filtter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtter != null)
            {
                query = query.Where(filtter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAllWithPaging(Expression<Func<T, bool>> filtter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? pageIndex = 1, int? pageSize = 10)
        {
            IQueryable<T> query = dbSet;
            if (filtter != null)
            {
                query = query.Where(filtter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            }
            return query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return dbSet;
        }

        public T GetById(int id)
        {
            if (id != 0)
            {
                return dbSet.Find(id);
            }
            return null;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filtter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtter != null)
            {
                query = query.Where(filtter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public T GetLastOrDefault(Expression<Func<T, bool>> filtter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtter != null)
            {
                query = query.Where(filtter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.LastOrDefault();
        }

        public async Task<PagedResultDto<TDto>> GetPagedAsync<T, TDto>(
         PagingRequest pagingRequest,
         Expression<Func<T, bool>> filter = null,
         List<Expression<Func<T, object>>> includeExpressions = null,

         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string? includeProperties = null) where T : class
        {
            IQueryable<T> query = _db.Set<T>();



            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Apply include expressions
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            // Include related entities
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            // Apply sorting
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else if (!string.IsNullOrEmpty(pagingRequest.SortBy))
            {
                // Dynamic sorting using System.Linq.Dynamic.Core
                var sorting = $"{pagingRequest.SortBy} {(pagingRequest.IsDescending ? "descending" : "ascending")}";
                query = query.OrderBy(sorting);
            }
            else
            {
                // Default sorting to avoid unpredictable results
                query = query.OrderBy(e => true);
            }

            // Get total count before paging
            var totalCount = await query.CountAsync();

            // Apply paging
            var items = await query
                .Skip((pagingRequest.PageNumber - 1) * pagingRequest.PageSize)
                .Take(pagingRequest.PageSize)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagingRequest.PageSize);

            // Return paged result
            return new PagedResultDto<TDto>
            {
                CurrentPage = pagingRequest.PageNumber,
                TotalPages = totalPages,
                PageSize = pagingRequest.PageSize,
                TotalCount = totalCount,
                Items = items
            };
        }

        public void Insert(T obj)
        {
            if (obj != null)
            {
                dbSet.Add(obj);
            }
        }

        public void InsertRange(ICollection<T> obj)
        {
            if (obj != null)
            {
                dbSet.AddRange(obj);
            }

        }

        public void Update(T obj)
        {
            if (obj != null)
            {
                dbSet.Update(obj);
            }
        }

        public void UpdateRange(ICollection<T> obj)
        {
            if (obj != null)
            {
                dbSet.UpdateRange(obj);
            }
        }


        public async Task<TDto> GetFirstOrDefaultAsync<T, TDto>(
       Expression<Func<T, bool>> filter = null,
       List<Expression<Func<T, object>>> includeExpressions = null,
       List<string> includeStrings = null
   ) where T : class
        {
            IQueryable<T> query = _db.Set<T>();

            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply include expressions
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            // Apply include strings
            if (includeStrings != null)
            {
                foreach (var includeString in includeStrings)
                {
                    query = query.Include(includeString);
                }
            }

            // Retrieve the first or default entity
            var entity = await query.FirstOrDefaultAsync();

            // Map to DTO
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> GetLastOrDefaultAsync<T, TDto>(
            Expression<Func<T, bool>> filter = null,
            List<Expression<Func<T, object>>> includeExpressions = null,
            List<string> includeStrings = null
        ) where T : class
        {
            IQueryable<T> query = _db.Set<T>();

            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply include expressions
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            // Apply include strings
            if (includeStrings != null)
            {
                foreach (var includeString in includeStrings)
                {
                    query = query.Include(includeString);
                }
            }

            // Retrieve the last or default entity
            var entity = await query.LastOrDefaultAsync();

            // Map to DTO
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> GetByIdAsync<T, TDto>(
            int id,
            List<Expression<Func<T, object>>> includeExpressions = null,
            List<string> includeStrings = null
        ) where T : class
        {
            IQueryable<T> query = _db.Set<T>();

            // Apply include expressions
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            // Apply include strings
            if (includeStrings != null)
            {
                foreach (var includeString in includeStrings)
                {
                    query = query.Include(includeString);
                }
            }

            // Assuming the entity has a primary key named "Id"
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

            // Map to DTO
            return _mapper.Map<TDto>(entity);
        }

        public async Task AddAsync(T obj)
        {
            // implement add method
            await dbSet.AddAsync(obj);
        }

        public async Task AddRangeAsync(ICollection<T> obj)
        {
            await dbSet.AddRangeAsync(obj);
        }
        public void DeleteAllRows()
        {
            var myids = (from t in _db.SMSConfigTbl select t.Id).ToList();
            foreach (var id in myids)
            {
                T entity = dbSet.Find(id);
                dbSet.Remove(entity);

            }
        }
        // Implement other repository methods...
    }

}

