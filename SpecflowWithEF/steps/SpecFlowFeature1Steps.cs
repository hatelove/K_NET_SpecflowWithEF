using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowWithEF.steps
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        [BeforeScenario()]
        public void BeforeScenario()
        {
            using (var dbcontext = new NorthwindEntities())
            {
                dbcontext.Database.ExecuteSqlCommand("Delete Orders Where CustomerID='Joey'");
                dbcontext.Database.ExecuteSqlCommand("Delete Customers Where CustomerID='Joey'");
            }
        }

        [Given(@"Customers table exists")]
        public void GivenCustomersTableExists(Table table)
        {
            var customers = table.CreateSet<Customers>();
            using (var dbcontext = new NorthwindEntities())
            {
                dbcontext.Customers.AddRange(customers);
                dbcontext.SaveChanges();
            }
        }

        [Given(@"Orders table exists")]
        public void GivenOrdersTableExists(Table table)
        {
            var orders = table.CreateSet<Orders>();
            using (var dbcontext = new NorthwindEntities())
            {
                dbcontext.Orders.AddRange(orders);
                dbcontext.SaveChanges();
            }
        }

        [When(@"I want to know count of (.*)'s orders")]
        public void WhenIWantToKnowCountOfSOrders(string customerId)
        {
            var orderService = new OrdereService();
            var count = orderService.Count(customerId);
            ScenarioContext.Current.Set<int>(count);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expected)
        {
            var count = ScenarioContext.Current.Get<int>();
            Assert.AreEqual(expected, count);
        }
    }

    public class OrdereService
    {
        public int Count(string customerId)
        {
            using (var dbcontext = new NorthwindEntities())
            {
                var count = dbcontext.Orders
                    .Where(o => o.CustomerID == customerId)
                    .Count();

                return count;
            }
        }
    }
}