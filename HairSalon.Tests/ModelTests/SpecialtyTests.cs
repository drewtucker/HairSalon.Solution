using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalonApp.Models;
using HairSalonApp;

namespace HairSalonApp.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public void Dispose()
    {
      Specialty.DeleteAll();
      Stylist.DeleteAll();
    }

    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=drew_tucker_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Specialty.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_SpecialtyList()
    {
      Specialty testSpecialty = new Specialty("Men's haircuts");
      testSpecialty.Save();
      Specialty testSpecialty2 = new Specialty("Women's haircuts");
      testSpecialty2.Save();
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty> {testSpecialty, testSpecialty2};
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
