using Shorewind.Data;
using Shorewind.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorewind.Services
{
    public class EmployeeService
    {

        public bool CreateEmployee(EmployeeCreate model)
        {
            var entity =
                new Employee()
                {
                    EmployeeId = model.EmployeeId,
                    //Future migration
                    //EmployeeEmail = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    //Future migration
                    //CreatedUtc = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Employees.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<EmployeeList> GetEmployees()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Employees
                        .Select(
                            e =>
                                new EmployeeList
                                {
                                    EmployeeId = e.EmployeeId,
                                    //Future migration
                                    //EmployeeEmail = model.Email,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    //Future migration
                                    //CreatedUtc = DateTimeOffset.Now,
                                });

                return query.ToArray();
            }
        }

        public EmployeeDetail GetEmployeeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Employees
                        .SingleOrDefault(e => e.EmployeeId == id);
                return
                    new EmployeeDetail
                    {
                        EmployeeId = entity.EmployeeId,
                        //Future migration
                        //EmployeeEmail = model.Email,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        //Future migration
                        //CreatedUtc = DateTimeOffset.Now,
                    };
            }
        }

        public EmployeeDetail GetEmployeeByName(string firstName, string lastName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Employees
                        .Single(e => e.LastName == lastName && e.FirstName == firstName);

                return new EmployeeDetail
                {
                    EmployeeId = entity.EmployeeId,
                    //Future migration
                    //EmployeeEmail = model.Email,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName
                    //Future migration
                    //CreatedUtc = DateTimeOffset.Now,
                };
            }

        }

        public bool UpdateEmployee(EmployeeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Employees
                        .Single(e => e.EmployeeId == model.EmployeeId);

                entity.EmployeeId = model.EmployeeId;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                //Uncomment after migration
                //entity.Email = model.Email;
                //Uncomment after migration
                //entity.CreatedUtc = model.CreatedUtc;
                //entity.ModifiedUtc = model.ModifiedUtc;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEmployee(int EmployeeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Employees
                    .Single(e => e.EmployeeId == EmployeeId);
                ctx.Employees.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
