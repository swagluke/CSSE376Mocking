using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;
namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
            var target = ObjectMother.Saab(); ;
            Assert.AreEqual(10 * 7 * .8, target.getBasePrice());
		}

        [TestMethod]
        public void TestThatCarHasCorrectBasePriceForTenDays()
        {
            var target = ObjectMother.Bmw();
            Assert.AreEqual(80, target.getBasePrice());
        }		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [TestMethod]
        public void TestThatCarGetLocation()
        {
            IDatabase mockFakeDB = mocks.StrictMock<IDatabase>();
            String LocationA = "Terre Haute";
            String LocationB = "Indianapolis";
            Expect.Call(mockFakeDB.getCarLocation(3)).Return(LocationA);
            Expect.Call(mockFakeDB.getCarLocation(24)).Return(LocationB);
            mockFakeDB.Stub(x => x.getCarLocation(Arg<int>.Is.Anything)).Return("Anywhere");
            mocks.ReplayAll();
            Car target = new Car(10);
            target.Database = mockFakeDB;
            String result;
            result = target.getCarLocation(3);
            Assert.AreEqual(LocationA, result);
            result = target.getCarLocation(24);
            Assert.AreEqual(LocationB, result);
            result = target.getCarLocation(66);
            Assert.AreEqual("Anywhere", result);

            mocks.VerifyAll();            
        }

        [TestMethod()]
        public void TestThatCarMileage()
        {
            IDatabase mockFakeDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = new Int32();
            for (var i = 0; i < 100; i++)
            {
                Miles = Miles + i;
            }
            Expect.Call(mockFakeDatabase.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockFakeDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockFakeDatabase;
            int milleageCount = target.Mileage;
            Assert.AreEqual(milleageCount, Miles);
            mocks.VerifyAll();
        }
	}
}
