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
