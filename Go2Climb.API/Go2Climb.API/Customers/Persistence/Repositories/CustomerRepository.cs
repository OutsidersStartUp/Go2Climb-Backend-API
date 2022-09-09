using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Go2Climb.API.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer> FindByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public Customer FindById(int id)
        {
            return _context.Customers.Find(id);
        }

        public async Task<Customer> FindByEmailAsync(string email)
        {
            return await _context.Customers.SingleOrDefaultAsync(C => C.Email == email);
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Customers.Any(c => c.Email == email);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }
    }
}