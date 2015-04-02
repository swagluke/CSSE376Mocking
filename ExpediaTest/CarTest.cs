using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

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
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        public void TestThatCarGetLocation()
        {
            IDatabase mockFakeDB = mocks.StrictMock<IDatabase>();
            String LocationA = "Whale Rider";
            String LocationB = "Raptor Wrangler";
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
	}
}
