using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalonApp.Models;
using HairSalonApp;

namespace HairSalonApp.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }

    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=drew_tucker_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_AssignsIdToClient_Id()
    {
      //Arrange
        Client testClient = new Client("Steve", 2067130144, 1, 1);
        testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetAll_DatabaseSavesListOfAllClients_ClientList()
    {
      Client testClient = new Client("Steve", 2067130144, 1, 1);
      testClient.Save();
      Client testClient2 = new Client("Berta", 2074258687, 2, 2);
      testClient2.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient, testClient2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Steve", 2067130144, 1, 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      Client testClient = new Client("Steve", 2067130144, 1, 1);
      testClient.Save();
      Client secondClient = new Client ("Jeff", 2067140144, 1, 1);

      testClient.Edit("Jeff", 2067140144);

      Assert.AreEqual(testClient, secondClient);
    }
}
}
