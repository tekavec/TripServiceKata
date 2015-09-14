using System;
using NUnit.Framework;

namespace TripService.Tests.UnitTests
{
    [TestFixture]
    public class TripDaoShould
    {
        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void ReturnTripsByAUser()
        {
            new TripDao().TripsByUser(new User());
        }
    }
}