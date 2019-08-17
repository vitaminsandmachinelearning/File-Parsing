using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Parse
{
    class DBManager
    {
        string connectionString;
        SqlConnection connection;

        public DBManager(string connectionString)
        {
            this.connectionString = connectionString;
            try
            {
                //Test connection to make sure database can be accessed
                connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
                Console.WriteLine("Connection successful.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection error - " + e);
            }
        }

        //Try to add new entry, return bool to optionally check if it was successful
        //Added bool parameter to ensure each AddEntry call doesn't need new open/close connection calls if called from AddEntries
        public bool AddPart(string PartNumber, string Description, double EAN, int FreeStock, bool multipleCalls)
        {
            try
            {
                if (!multipleCalls)
                    connection.Open();

                //If part already exists then return true because it has already been added
                if (CheckPartExists(PartNumber))
                    return true;

                //Requires C#6 for string interpolation
                string sql = $"Insert into Parts (PartNumber, Description, EAN, FreeStock) values('{PartNumber}', '{Description}', '{EAN}', '{FreeStock}')";

                using (SqlDataAdapter adapter = new SqlDataAdapter
                {
                    InsertCommand = new SqlCommand(sql, connection)
                })
                {
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                if (!multipleCalls)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("AddEntry error - " + e);
                return false;
            }
            return true;
        }

        //Try to add every Part in list, if any individual Part returns false, return false
        public bool AddParts(List<Part> Parts)
        {
            connection.Open();
            foreach (Part p in Parts)
            {
                if (!AddPart(p.PartNumber, p.Description, p.EAN, p.FreeStock, true))
                {
                    connection.Close();
                    return false;
                }
            }
            connection.Close();
            return true;
        }

        public bool UpdateParts(List<Part> Parts)
        {
            string sql;
            try
            {
                connection.Open();

                foreach (Part p in Parts)
                {
                    //Make sure part exists before trying to update it to avoid unexpected behaviour
                    if (CheckPartExists(p.PartNumber))
                    {
                        sql = $"UPDATE Parts SET FreeStock = '{p.FreeStock}' WHERE PartNumber = '{p.PartNumber}'";
                        using (SqlDataAdapter adapter = new SqlDataAdapter
                        {
                            UpdateCommand = new SqlCommand(sql, connection)
                        })
                        {
                            adapter.UpdateCommand.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Update parts error - " + e);
                return false;
            }
            return true;
        }

        bool CheckPartExists(string PartNumber)
        {
            string sql = $"SELECT COUNT(*) from Parts where PartNumber like '{PartNumber}'";

            SqlCommand command = new SqlCommand(sql, connection);

            int exists = (int)command.ExecuteScalar();

            return exists > 0;
        }
    }
}