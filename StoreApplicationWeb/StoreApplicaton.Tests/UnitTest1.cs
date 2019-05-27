using StoreApplication.DataAccess;
using StoreApplication.DataAccess.Entities;
using StoreApplication.Library;
using System;
using Xunit;

namespace StoreApplicaton.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Customer c = new Customer
            {
                CustomerId = 1,
                FName = "a",
                LName = "b",
                State = "af",
                StoreId = 2,
                DefaultLocation = new Location()
                {
                    LocationId = 1,
                    Name = "a",
                    State = "d",
                }
            };
            Consumer c2 = Mapper.Map(c);

            Assert.Equal("a", c2.Fname);
                 
        }
    }
}
