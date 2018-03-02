using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp;

namespace HairSalonApp.Models
{
  public class Specialty
  {
    private string _specialty;
    private int _id;

    public Specialty(string specialty, int Id = 0)
    {
      _specialty = specialty;
      _id = Id;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool specialtyEquality = this.GetSpecialty() == newSpecialty.GetSpecialty();
        return (idEquality && specialtyEquality);
      }
    }

    //GETTERS
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetSpecialty()
    {
      return _specialty;
    }

    public int GetId()
    {
      return _id;
    }

    //SETTERS

    public void SetSpecialty(string newSpecialty)
    {
      _specialty = newSpecialty;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    //MAIN FUNCTIONS
    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialty = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialty, specialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter stylist_id = new MySqlParameter("@Stylistid", newStylist.GetId());
      cmd.Parameters.Add(stylist_id);

      MySqlParameter specialty_id = new MySqlParameter(@"SpecialtyId", _id);
      cmd.Parameters.Add(specialty_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialties
        JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
        JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
        WHERE specialties.id = @SpecialtyId;";

      MySqlParameter specialtyIdParameter = new MySqlParameter("@SpecialtyId", _id);
      cmd.Parameters.Add(specialtyIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        int stylistPhoneNumber = rdr.GetInt32(2);
        string stylistEmail = rdr.GetString(3);
        int stylistExperience = rdr.GetInt32(4);
        Stylist newStylist = new Stylist(stylistName, stylistPhoneNumber, stylistEmail, stylistExperience, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO specialties (specialty) VALUES (@specialty);";
     MySqlParameter specialty = new MySqlParameter("@specialty", _specialty);
     cmd.Parameters.Add(specialty);

     cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

     MySqlParameter thisId = new MySqlParameter("@searchId", id);
     cmd.Parameters.Add(thisId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int specialtyId = 0;
     string specialty = "";
     while(rdr.Read())
     {
       specialtyId = rdr.GetInt32(0);
       specialty = rdr.GetString(1);
     }
     Specialty newSpecialty = new Specialty(specialty, specialtyId);
     conn.Close();
     if(conn != null)
     {
       conn.Dispose();
     }
     return newSpecialty;
    }

    public void Delete()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId;";
       MySqlParameter thisId = new MySqlParameter ("@thisId", _id);
       cmd.Parameters.Add(thisId);
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
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
     }



  }
}
