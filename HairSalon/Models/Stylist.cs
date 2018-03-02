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
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (nameEquality);
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

    //RETURNS A LIST OF ALL STYLISTS CURRENT IN THE DATABASE
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

    //RETURNS A LIST OF ALL CLIENTS ASSIGNED TO THIS PARTICULAR STYLIST
    public List<Client> GetAllClients()
    {
      List<Client> stylistClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter temporaryStylistId = new MySqlParameter("@stylist_id", _id);
      cmd.Parameters.Add(temporaryStylistId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int phoneNumber = rdr.GetInt32(2);
        int stylistId = rdr.GetInt32(3);
        Client temporaryClient = new Client(name, phoneNumber, stylistId);
        temporaryClient.SetId(id);
        stylistClients.Add(temporaryClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylistClients;
    }

    //SAVES A STYLIST IN THE DATABASE
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

    //FINDS A SPECIFIC STYLIST
    public static Stylist Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

     MySqlParameter searchId = new MySqlParameter("@searchId", id);
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int stylistId = 0;
     string stylistName = "";
     int stylistNumber = 0;
     string stylistEmail = "";
     int stylistExperience = 0;

     while(rdr.Read())
     {
       stylistId = rdr.GetInt32(0);
       stylistName = rdr.GetString(1);
       stylistNumber = rdr.GetInt32(2);
       stylistEmail = rdr.GetString(3);
       stylistExperience = rdr.GetInt32(4);
     }
     Stylist newStylist = new Stylist(stylistName, stylistNumber, stylistEmail, stylistExperience, stylistId);
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return newStylist;
   }

   //ADDS A SPECIALTY TO A STYLIST
   public void AddSpecialty(Specialty newSpecialty)
   {
     MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter stylist_id = new MySqlParameter("@StylistId", _id);
      cmd.Parameters.Add(stylist_id);
      MySqlParameter specialty_id = new MySqlParameter("@SpecialtyId", newSpecialty.GetId());
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
   }

   //EDITS THIS STYLIST IN THE DATABASE
   public void Edit(string newName, int newPhoneNumber, string newEmail)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"UPDATE stylists SET name = @newName, phone_number = @phone_number, email = @email WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter("@searchId", _id);
     cmd.Parameters.Add(searchId);

     MySqlParameter name = new MySqlParameter("@newName", newName);
     cmd.Parameters.Add(name);

     MySqlParameter phoneNumber = new MySqlParameter("@phone_number", newPhoneNumber);
     cmd.Parameters.Add(phoneNumber);

     MySqlParameter email = new MySqlParameter("@email", newEmail);
     cmd.Parameters.Add(email);
     cmd.ExecuteNonQuery();
     _name = newName;
     _phoneNumber = newPhoneNumber;
     _email = newEmail;
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

   //DELETES THIS PARTICULAR STYLIST
   public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter ("@thisId", _id);
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    //DELETES *ALL* STYLISTS IN THE DATABASE
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
