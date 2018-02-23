using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp;

namespace HairSalonApp.Models
{
  public class Stylist
  {
    private string _name;
    private int _phoneNumber;
    private string _email;
    private int _experience;
    private int _id;

    public Stylist(string name, int phoneNumber, string email, int experience, int Id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _email = email;
      _experience = experience;
      _id = Id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }

    //GETTERS

    public string GetName()
    {
      return _name;
    }

    public int GetPhoneNumber()
    {
      return _phoneNumber;
    }

    public string GetEmail()
    {
      return _email;
    }

    public int GetExperience()
    {
      return _experience;
    }

    public int GetId()
    {
      return _id;
    }

    //SETTERS

    public void SetName(string newName)
    {
      _name = newName;
    }

    public void SetPhoneNumber(int newPhoneNumber)
    {
      _phoneNumber = newPhoneNumber;
    }

    public void SetEmail(string newEmail)
    {
      _email = newEmail;
    }

    public void SetExperience(int newExperience)
    {
      _experience = newExperience;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        int stylistNumber = rdr.GetInt32(2);
        string stylistEmail = rdr.GetString(3);
        int stylistExperience = rdr.GetInt32(4);
        Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistEmail, stylistExperience, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name, phone_number, email, experience, id) VALUES (@name, @phone_number, @email, @experience, @id);";

      MySqlParameter name = new MySqlParameter("@name", _name);
      cmd.Parameters.Add(name);
      MySqlParameter phone_number = new MySqlParameter("@phone_number", _phoneNumber);
      cmd.Parameters.Add(phone_number);
      MySqlParameter email = new MySqlParameter("@email", _email);
      cmd.Parameters.Add(email);
      MySqlParameter experience = new MySqlParameter("@experience", _experience);
      cmd.Parameters.Add(experience);
      MySqlParameter id = new MySqlParameter("@id", _id);
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
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
     cmd.CommandText = @"DELETE FROM stylists;";
     cmd.ExecuteNonQuery();
     conn.Close();
     if(conn != null)
     {
       conn.Dispose();
     }
    }


  }
}
