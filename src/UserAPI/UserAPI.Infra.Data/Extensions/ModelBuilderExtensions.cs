using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Enums;

namespace UserAPI.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedInitialData(this ModelBuilder modelBuilder)
        {
            List<Job> jobs = new List<Job>()
            {
                new Job(){Id = Guid.Parse("4CD25F20-ED21-48D3-9AD7-2E960F7B020B"), Name = "Technical Writer", Excluded = true},
                new Job(){Id = Guid.Parse("1FBA3924-5728-4370-BAFC-0E56A792A3FD"), Name = "Doctor"},
                new Job(){Id = Guid.Parse("9624F6BB-D28D-44FA-8314-B50D577C95B1"), Name = "Civil Drafter"},
                new Job(){Id = Guid.Parse("DECDE446-A8A7-48AD-BA3E-FBA8040E142C"), Name = "Nanosystems Engineer"}
            };

            List<User> users = new List<User>()
            {
                new User(){Id = Guid.Parse("B087C7BB-CE36-4C6C-A597-3350E11EA318"), Name = "Mildred L Miller", Gender = EGender.Female, Cellphone = "978-956-6106", Email = "daphne.lan6@gmail.com", CPF = "869602519", JobId = Guid.Parse("4CD25F20-ED21-48D3-9AD7-2E960F7B020B"), Role = "manager", Password = "123"},
                new User(){Id = Guid.Parse("1D76F592-08FC-4B00-964D-95C8AE8B7E43"), Name = "Donald M Benfield", Gender = EGender.Male, Cellphone = "858-722-8320", Email = "darron2006@gmail.com", CPF = "200316424", JobId = Guid.Parse("1FBA3924-5728-4370-BAFC-0E56A792A3FD"), Role = "employee", Password = "123", Excluded = true},
                new User(){Id = Guid.Parse("E2CBB204-8AF2-407D-98FB-FC302765C085"), Name = "Susan C Grayson", Gender = EGender.Female, Cellphone = "715-927-7473", Email = "gabrielle.ja@yahoo.com", CPF = "908063946", JobId = Guid.Parse("9624F6BB-D28D-44FA-8314-B50D577C95B1"), Role = "analyst", Password = "123"},
                new User(){Id = Guid.Parse("B41EE7CF-FD0A-4B62-B320-AF907BDCAF3A"), Name = "Louise J Arruda", Gender = EGender.PreferNotToSay, Cellphone = "706-351-4717", Email = "florine1982@yahoo.com", CPF = "802615173", JobId = Guid.Parse("DECDE446-A8A7-48AD-BA3E-FBA8040E142C"), Role = "staff", Password = "123"},
                new User(){Id = Guid.Parse("E776A160-045D-439A-B3CD-FE15EBE6B7F2"), Name = "Rodney L Thomas", Gender = EGender.Male, Cellphone = "706-351-4717", Email = "marcos1997@gmail.com", CPF = "04660758", Role = "team", Password = "123"}
            };

            modelBuilder.Entity<Job>().HasData(jobs);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
