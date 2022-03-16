using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI01.Domain;
using WebAPI01.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAPI01.API.Services;
using WebAPI01.Infrastructure.Data;
using WebAPI01.Infrastructure.Facades;
using WebAPI01.Infrastructure.Repositories;

namespace TestProject1
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "DocumentsDB");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public UserRepository PersonRepository
        {
            get
            {
                return new UserRepository(_context);
            }
        }
    }
}
