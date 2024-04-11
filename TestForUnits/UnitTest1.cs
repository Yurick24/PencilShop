using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace TestForUnits
{
    [TestClass]
    public class UnitTest1
    {
        public SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True;MultipleActiveResultSets=true");
        public SqlCommand cmd;
        public SqlDataReader dr;

        [TestMethod]
        public void AddNewProductTest()
        {
            cn.Open();
            bool test = false;
            string[] mass = { "Apple", "apple.jpg", "Fruit", "120 r/kg"};
            string[] result = { "", "", "", "" };

            cmd = new SqlCommand("INSERT INTO Products VALUES('" + mass[0] + "','" + mass[1] + "','" + mass[2] + "','" + mass[3] + "')", cn);
            cmd.ExecuteReader();

            cmd = new SqlCommand("SELECT Name, SourceName, Category, Prise FROM Products", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result[0] = dr[0].ToString();
                result[1] = dr[1].ToString();
                result[2] = dr[2].ToString();
                result[3] = dr[3].ToString();
                if (mass.SequenceEqual(result))
                {
                    test = true;
                }
            }
            Assert.AreEqual(test, true);
            dr.Close();
            cn.Close();
        }

        [TestMethod]
        public void AltProductTest()
        {
            cn.Open();
            string[] mass = { "Apple", "apple.jpg", "Fruit", "120 r/kg" };
            string[] update = { "Apple", "apple.jpg", "Berry", "120 r/kg" };
            string[] result = { "", "", "", "" };
            string ProductID = "";

            cmd = new SqlCommand("SELECT * FROM Products WHERE Name='" + mass[0] + "' and SourceName='" + mass[1] + "' and Category='" + mass[2] + "' and Prise='" + mass[3] + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductID = dr[0].ToString();
            }
            dr.Close();

            cmd = new SqlCommand("UPDATE Products SET Category='" + update[2] + "' WHERE ID='" + ProductID + "'", cn);
            cmd.ExecuteReader();

            cmd = new SqlCommand("SELECT ID, Name, SourceName, Category, Prise FROM Products WHERE id='" + ProductID + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result[0] = dr[1].ToString();
                result[1] = dr[2].ToString();
                result[2] = dr[3].ToString();
                result[3] = dr[4].ToString();
            }
            CollectionAssert.AreEqual(update, result);
            dr.Close();
            cn.Close();
        }

        [TestMethod]
        public void DelProductTest()
        {
            cn.Open();
            bool test = true;
            string[] mass = { "Apple", "apple.jpg", "Berry", "120 r/kg" };

            cmd = new SqlCommand("DELETE FROM Products WHERE Name='" + mass[0] + "' and SourceName='" + mass[1] + "' and Category='" + mass[2] + "' and Prise='" + mass[3] + "'", cn);
            cmd.ExecuteReader();

            cmd = new SqlCommand("SELECT * FROM Products WHERE Name='" + mass[0] + "' and SourceName='" + mass[1] + "' and Category='" + mass[2] + "' and Prise='" + mass[3] + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                test = false;
            }
            Assert.IsTrue(test);
            dr.Close();
            cn.Close();
        }
    }
}
