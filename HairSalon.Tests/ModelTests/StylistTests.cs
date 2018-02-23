using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalonApp.Models;
using HairSalonApp;

namespace HairSalonApp.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=drew_tucker_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jim", 2067130144, "Jim@gmail.com", 4, 1);

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jim", 2067130144, "Jim@gmail.com", 4, 1);
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jim", 2067130144, "Jim@gmail.com", 4, 1);
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }


}

}
