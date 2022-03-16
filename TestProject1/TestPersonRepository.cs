using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebAPI01.Domain;

namespace TestProject1
{
    public class TestPersonRepository
    {
        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var id = new Guid();
            User user = new User {Id = id, FirstName = "Nic", LastName = "Abramov"};
            personRepository.AddAsync(user).Wait();

            Assert.True(personRepository.GetAllAsync().Result.Count == 1);
            Assert.Equal("Nic", personRepository.GetByIdAsync(id).Result.FirstName);
            Assert.Equal("Abramov", personRepository.GetByIdAsync(id).Result.LastName);
        }
    }
}
