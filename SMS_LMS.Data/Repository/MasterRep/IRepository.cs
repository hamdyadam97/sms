using SMS_LMS.Models.DataModels.PagedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.Repository.MasterRep
{
    public interface IRepository<T> where T : class
    {

        Task<PagedResultDto<TDto>> GetPagedAsync<T, TDto>(
            PagingRequest pagingRequest,
            Expression<Func<T, bool>> filter = null,
              List<Expression<Func<T, object>>> includeExpressions = null,

             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             string? includeProperties = null) where T : class;


        IEnumerable<T> GetAll(
                 Expression<Func<T, bool>> filtter = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                 string includeProperties = null
            );

        IEnumerable<T> GetAllWithPaging(
                 Expression<Func<T, bool>> filtter = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                 string includeProperties = null,
                 int? pageIndex = 1,
                 int? pageSize = 10
            );
        Task<IQueryable<T>> GetAllAsync();
        T GetFirstOrDefault(
                            Expression<Func<T, bool>> filtter = null,
                                            string includeProperties = null
                       );
        T GetLastOrDefault(
            Expression<Func<T, bool>> filtter = null,
            string includeProperties = null
                                  );

        T GetById(int id);
        int count(Expression<Func<T, bool>> filtter = null);
        void Insert(T obj);
        Task AddAsync(T obj);
        Task AddRangeAsync(ICollection<T> obj);
        void InsertRange(ICollection<T> obj);
        void Update(T obj);
        void UpdateRange(ICollection<T> obj);
        void Delete(int id);
        void Delete(T entity);
        void DeleteAllRows();

        Task<TDto> GetFirstOrDefaultAsync<T, TDto>(
        Expression<Func<T, bool>> filter = null,
        List<Expression<Func<T, object>>> includeExpressions = null,
        List<string> includeStrings = null
    ) where T : class;

        Task<TDto> GetLastOrDefaultAsync<T, TDto>(
            Expression<Func<T, bool>> filter = null,
            List<Expression<Func<T, object>>> includeExpressions = null,
            List<string> includeStrings = null
        ) where T : class;

        Task<TDto> GetByIdAsync<T, TDto>(
            int id,
            List<Expression<Func<T, object>>> includeExpressions = null,
            List<string> includeStrings = null
        ) where T : class;
    }
}
