using Shorewind.Data;
using Shorewind.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Services
{
    public class CustomerService
    {
        private readonly Guid _userId;
        public CustomerService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCustomer(CustomerCreate model)
        {
            var entity =
                new Customer()
                {
                    CustomerId = _userId,
		            Email = model.Email,
		            FirstName = model.FirstName,
                    //LastName = model.LastName,
		            Address= model.Address,
		            City = model.City,
		            State = model.State,
		            PostalCode = model.PostalCode,
		            PhoneNumber = model.PhoneNumber,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<CustomerList> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Select(
                            e =>
                                new CustomerList
                                {
                                    CustomerId = e.CustomerId,
                                    Email = e.Email,
                                    FirstName = e.FirstName,
                                    //LastName = e.LastName,
                                    Address = e.Address,
                                    City = e.City,
                                    State = e.State,
                                    PostalCode = e.PostalCode,
                                    PhoneNumber = e.PhoneNumber,
                                    CreatedUtc = DateTimeOffset.Now,
                                    ModifiedUtc = DateTimeOffset.Now

                                });

                return query.ToArray();
            }
        }

        public CustomerDetail GetCustomerById(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == id);
                return
                    new CustomerDetail
                    {
                        CustomerId = entity.CustomerId,
                        Email = entity.Email,
                        FirstName = entity.FirstName,
                        //LastName = entity.LastName,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State,
                        PostalCode = entity.PostalCode,
                        PhoneNumber = entity.PhoneNumber,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public CustomerDetail GetCustomer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == _userId);
                return
                    new CustomerDetail
                    {
                        CustomerId = entity.CustomerId,
                        Email = entity.Email,
                        FirstName = entity.FirstName,
                        //LastName = entity.LastName,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State,
                        PostalCode = entity.PostalCode,
                        PhoneNumber = entity.PhoneNumber,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateCustomer(CustomerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == _userId);

                        entity.CustomerId = model.CustomerId;
                        entity.Email = model.Email;
                        entity.FirstName = model.FirstName;
                        //entity.LastName = model.LastName;
                        entity.Address = model.Address;
                        entity.City = model.City;
                        entity.State = model.State;
                        entity.PostalCode = model.PostalCode;
                        entity.PhoneNumber = model.PhoneNumber;
                        entity.CreatedUtc = model.CreatedUtc;
                        entity.ModifiedUtc = model.ModifiedUtc;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCustomer(Guid customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Customers
                    .Single(e => e.CustomerId == customerId);
                ctx.Customers.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
