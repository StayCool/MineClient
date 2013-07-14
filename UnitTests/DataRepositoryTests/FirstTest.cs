using System.Linq;
using DataRepository.Models;
using NUnit.Framework;
using DataRepository.DataAccess;

namespace UnitTests.DataRepositoryTests
{
    [TestFixture]
    public class FirstTest
    {
        [Test]
        public void TestDb() {
            //Database.SetInitializer<MineContext>(new MineDbInitializer());
            
            using (var context = new MineContext()) {
                Assert.AreNotEqual(context, null);

                Door door = context.Doors.First();

                Assert.AreEqual(door.Id, 1);
                Assert.AreEqual(door.DoorStateId, 1);
                Assert.AreEqual(door.DoorStateId, 1);
            } 
        }
    }
}
