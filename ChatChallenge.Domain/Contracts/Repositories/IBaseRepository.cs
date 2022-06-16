namespace ChatChallenge.Domain.Contracts.Repositories;

public interface IBaseRepository<T>
{
    Task<int> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
    Task<T> Get(int id);
    Task<ICollection<T>> GetAll();
}