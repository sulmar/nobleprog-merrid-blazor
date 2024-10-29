using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeCustomerRepository : ICustomerRepository
{
    private readonly IDictionary<int, Customer> _customers;

    public FakeCustomerRepository(IEnumerable<Customer> customers)
    {
        _customers = customers.ToDictionary(c => c.Id);
    }

    public Task AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
       

        return await Task.FromResult(_customers.Values.ToList());
    }

    public Task<Customer> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
