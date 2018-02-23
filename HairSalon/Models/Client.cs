using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp;

namespace HairSalonApp.Models
{
  public class Client
  {
    private string _name;
    private int _phoneNumber;
    private int _id;
    private int _stylistId;

    public Client (string name, int phoneNumber, int stylistId = 0, int Id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _stylistId = stylistId;
      _id = Id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && stylistIdEquality);
      }
    }

    //GETTERS
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public int GetPhoneNumber()
    {
      return _phoneNumber;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }
    
    public void SetId(int newId)
    {
      _id = newId;
    }

    //MAIN FUNCTIONS

    public void Save()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO clients (name, phone_number, stylist_id, id) VALUES (@name, @phone_number, @stylist_id, @id);";

     MySqlParameter name = new MySqlParameter("@name", _name);
     cmd.Parameters.Add(name);
     MySqlParameter phoneNumber = new MySqlParameter("@phone_number", _phoneNumber);
     cmd.Parameters.Add(phoneNumber);
     MySqlParameter stylistId = new MySqlParameter("@stylist_id", _stylistId);
     cmd.Parameters.Add(stylistId);
     MySqlParameter clientId = new MySqlParameter("@id", _id);
     cmd.Parameters.Add(clientId);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

    public static List<Client> GetAll()
   {
     List<Client> allClients = new List<Client> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients;";
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string clientName = rdr.GetString(1);
       int clientPhoneNumber = rdr.GetInt32(2);
       int clientStylistId = rdr.GetInt32(3);
       Client newClient = new Client(clientName, clientPhoneNumber, clientStylistId, clientId);
       allClients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allClients;
   }

   public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE id = (@searchId);";

      MySqlParameter thisId = new MySqlParameter("@searchId", id);
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      int clientPhoneNumber = 0;
      int clientStylistId = 0;

      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientPhoneNumber = rdr.GetInt32(2);
        clientStylistId = rdr.GetInt32(3);
      }
      Client newClient = new Client(clientName, clientPhoneNumber, clientStylistId, clientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return newClient;
    }

    public void Edit(string newName, int newPhoneNumber)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName, phone_number = @phone_number WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter("@searchId", _id);
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter("@newName", newName);
      cmd.Parameters.Add(name);

      MySqlParameter phoneNumber = new MySqlParameter("@phone_number", newPhoneNumber);
      cmd.Parameters.Add(phoneNumber);
      cmd.ExecuteNonQuery();
      _name = newName;
      _phoneNumber = newPhoneNumber;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM clients;";
     cmd.ExecuteNonQuery();
     conn.Close();
     if(conn != null)
     {
       conn.Dispose();
     }
    }

  }
}
