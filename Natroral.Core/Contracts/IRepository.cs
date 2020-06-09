using System.Linq;
using Natroral.Core.Models;

namespace Natroral.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);      
        void Insert(T t);
        void Update(T t);
    }
}