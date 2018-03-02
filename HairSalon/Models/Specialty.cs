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

    public Specialty(string specialty, int Id = 0);
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

    public List<Specialty> GetAll()
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



  }
}
