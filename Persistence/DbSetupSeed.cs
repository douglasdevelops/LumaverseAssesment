using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            var locations = new List<Location>
            {
                new Location
                {
                    State = "New York",
                    City = "New York",
                    Latitude = 0,
                    Longitude = 0,

                },
                new Location
                {
                    State = "Illinois",
                    City = "Chicago",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "Florida",
                    City = "Miami",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "California",
                    City = "Los Angeles",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "Arizona",
                    City = "Phoenix",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "Texas",
                    City = "Dallas",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "Texas",
                    City = "Houston",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "North Carolina",
                    City = "Charlotte",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "Vermont",
                    City = "Burlington",
                    Latitude = 0,
                    Longitude = 0,
                },
                new Location
                {
                    State = "West Virginia",
                    City = "Charleston",
                    Latitude = 0,
                    Longitude = 0,
                }
            };

            var engineers = new List<Engineer>
            {
              new Engineer
              {
                  FirstName = "Ada",
                  LastName = "Lovelace",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },
              new Engineer
              {
                  FirstName = "Alan",
                  LastName = "Turing",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },
              new Engineer
              {
                  FirstName = "Ray",
                  LastName = "Tomlinson",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },
              new Engineer
              {
                  FirstName = "John",
                  LastName = "McCarthy",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },
              new Engineer
              {
                  FirstName = "James",
                  LastName = "Gosling",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },
              new Engineer
              {
                  FirstName = "Margaret",
                  LastName = "Hamilton",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },new Engineer
              {
                  FirstName = "Barbara",
                  LastName = "Liskov",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },new Engineer
              {
                  FirstName = "Kimberly",
                  LastName = "Bryant",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              },new Engineer
              {
                  FirstName = "Tim",
                  LastName = "Berners-Lee",
                  EmployeeStatus = EmployeeStatus.Working,
                  HireDate = DateTime.Now,
                  LastKnownLocationId = 9,
                  PhoneNumber = "8005551212"
              }
            };

            try
            {

                if (context.Locations.Any() == false)
                {
                    await context.Locations.AddRangeAsync(locations);
                    await context.SaveChangesAsync();
                }
                    
                if (context.Engineers.Any() == false)
                {
                    await context.Engineers.AddRangeAsync(engineers);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
