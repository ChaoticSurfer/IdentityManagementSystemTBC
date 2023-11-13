using System;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IdentityManagementDbContext _identityManagementDbContext;

        public PhoneRepository(IdentityManagementDbContext identityManagementDbContext)
        {
            _identityManagementDbContext = identityManagementDbContext;
        }

        public async Task<Phone> GetById(int id)
        {
            return await _identityManagementDbContext.Phones.FindAsync(id);
        }

        public async Task Add(Phone phone)
        {
            await _identityManagementDbContext.Phones.AddAsync(phone);
        }

        public void Update(Phone phone)
        {
            _identityManagementDbContext.Update(phone);
        }

        public void Delete(Phone phone)
        {
            _identityManagementDbContext.Phones.Remove(phone);
        }

        public async Task SaveChanges()
        {
            await _identityManagementDbContext.SaveChangesAsync();
        }
    }
}