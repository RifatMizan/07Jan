﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EmployeeInformationSystem.DAL.DAO;

namespace EmployeeInformationSystem.DAL.DBGateway
{
    class DesignationDBGateway
    {
        string connectionStr = @"Data Source = \SQLEXPRESS; database = employeeDB; Integrated Security = True";
        private SqlConnection aSqlConnection;
        private SqlCommand aSqlCommand;
        public DesignationDBGateway()
        {
            aSqlConnection = new SqlConnection(connectionStr);
        }

        public void Save(Designation aDesignation)
        {   
            string query = "INSERT INTO t_designation VALUES ('" + aDesignation.Code + "','" + aDesignation.Title 
                + "')";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            aSqlConnection.Close();
        }

        public Designation Find(string code)
        {
            string query = "SELECT * FROM t_designation WHERE Code = '" + code +"'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();
            Designation aDesignation;

            if (aDataReader.HasRows)
            {
                aDesignation = new Designation();
                aDataReader.Read();
                aDesignation.Id = Convert.ToInt32(aDataReader["id"]);
                aDesignation.Code = aDataReader["code"].ToString();
                aDesignation.Title = aDataReader["title"].ToString();
                aDataReader.Close();
                aSqlConnection.Close();
                return aDesignation;
            }
            else
            {
                aSqlConnection.Close();
                return null;
            }
        }
    }
}
