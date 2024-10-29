using Domain.Models;

namespace Domain.Abstractions;

public interface IMessageService
{
    void Send(string message);
}

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageService messageService;
    private const decimal Ratio = 100;

    public CustomerService(ICustomerRepository customerRepository, IMessageService messageService)
    {
        _customerRepository = customerRepository;
        this.messageService = messageService;
    }

    public async Task<decimal> CalculateSalary(int customerId)
    {
        var customer = await _customerRepository.GetAsync(customerId);

        return customer.Name.Length * Ratio; 
    }

    public async Task AddAsync(Customer customer)
    {
        await _customerRepository.AddAsync(customer);

        messageService.Send($"Dodano klienta {customer.Name}");
    }
}

