using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using C__API_DB_Tests.DataBase;

namespace C__API_DB_Tests.DataBaseTest
{
    public class DataBaseTest1
    {
        private DataBase1 dbConnection;

        [SetUp]
        public void SetUp()
        {
            dbConnection = new DataBase1();
        }
        [Test]
        public void TestGet()
        {
            string query = "SELECT * FROM customers;";
            DataTable result = dbConnection.GetData(query);

            Assert.IsNotNull(result, "Result set should not be null.");
            Console.WriteLine($"Fetched {result.Rows.Count} rows.");
        }

        [Test]
        public void TestRetrieveData()
        {
            string query = "SELECT * FROM customers WHERE customerName = 'Atelier graphique';";
            DataTable result = dbConnection.GetData(query);

            Assert.IsNotNull(result, "Result set should not be null.");
            Assert.IsTrue(result.Rows.Count > 0, "No data found in the result set.");

            foreach (DataRow row in result.Rows)
            {
                Assert.AreEqual(103, row["customerNumber"], "Customer Number does not match.");
                Assert.AreEqual("Atelier graphique", row["customerName"], "Customer Name does not match.");

                Console.WriteLine($"Customer Name: {row["customerName"]}");
                Console.WriteLine($"Customer Number: {row["customerNumber"]}");
            }
        }
        [Test]
        public void TestInsertData()
        {
            string insertQuery = @"
                INSERT INTO customers (
                    customerNumber, customerName, contactLastName, 
                    contactFirstName, phone, addressLine1, addressLine2, 
                    city, state, postalCode, country, salesRepEmployeeNumber, creditLimit
                ) VALUES (
                    500, 'Georgea Company', 'Doe', 'John', '123-456-7890', 
                    '123 Main St', NULL, 'New York', 'NY', '10001', 'USA', 1370, 50000.00
                );";

            int rowsInserted = dbConnection.UpdateData(insertQuery);
            Assert.IsTrue(rowsInserted > 0, "No rows inserted.");

            string selectQuery = "SELECT * FROM customers WHERE customerNumber = 500;";
            DataTable result = dbConnection.GetData(selectQuery);

            Assert.IsNotNull(result, "Result set should not be null.");
            Assert.IsTrue(result.Rows.Count > 0, "No data found for customerNumber 500.");

            foreach (DataRow row in result.Rows)
            {
                Assert.AreEqual("Georgea Company", row["customerName"], "Customer Name does not match.");
                Assert.AreEqual("Doe", row["contactLastName"], "Contact Last Name does not match.");
                Assert.AreEqual("John", row["contactFirstName"], "Contact First Name does not match.");
                Assert.AreEqual("123-456-7890", row["phone"], "Phone does not match.");
                Assert.AreEqual("123 Main St", row["addressLine1"], "Address Line 1 does not match.");
                Assert.AreEqual("New York", row["city"], "City does not match.");
                Assert.AreEqual("NY", row["state"], "State does not match.");
                Assert.AreEqual("10001", row["postalCode"], "Postal Code does not match.");
                Assert.AreEqual("USA", row["country"], "Country does not match.");
                Assert.AreEqual(1370, row["salesRepEmployeeNumber"], "Sales Rep Employee Number does not match.");
                Assert.AreEqual(50000.00, Convert.ToDouble(row["creditLimit"]), 0.01, "Credit Limit does not match.");
            }
        }

        [Test]
        public void DeleteInsertedData()
        {
            // SQL query to delete the customer with customerNumber 500
            string deleteQuery = "DELETE FROM customers WHERE customerNumber = 500;";

            // Execute the delete query
            int rowsDeleted = dbConnection.UpdateData(deleteQuery);

            // Assert that at least one row was deleted
            Assert.IsTrue(rowsDeleted > 0, "No rows deleted.");
        }

        [Test]
        public void TestUpdateData()
        {
            string updateQuery = @"
                UPDATE customers SET 
                    customerName = 'Georgea Company', contactLastName = 'Doe', 
                    contactFirstName = 'John', phone = '123-456-7890', 
                    addressLine1 = '123 Main St', addressLine2 = NULL, 
                    city = 'New York', state = 'NY', postalCode = '10001', 
                    country = 'USA', salesRepEmployeeNumber = 1370, 
                    creditLimit = 50000.00
                WHERE customerNumber = 119;";

            int rowsUpdated = dbConnection.UpdateData(updateQuery);
            Assert.IsTrue(rowsUpdated > 0, "No rows updated.");

            string selectQuery = "SELECT * FROM customers WHERE customerNumber = 119;";
            DataTable result = dbConnection.GetData(selectQuery);

            Assert.IsNotNull(result, "Result set should not be null.");
            Assert.IsTrue(result.Rows.Count > 0, "No data found for customerNumber 119.");

            foreach (DataRow row in result.Rows)
            {
                Assert.AreEqual("Georgea Company", row["customerName"], "Customer Name does not match.");
                Assert.AreEqual("Doe", row["contactLastName"], "Contact Last Name does not match.");
                Assert.AreEqual("John", row["contactFirstName"], "Contact First Name does not match.");
                Assert.AreEqual("123-456-7890", row["phone"], "Phone does not match.");
                Assert.AreEqual("123 Main St", row["addressLine1"], "Address Line 1 does not match.");
                Assert.AreEqual("New York", row["city"], "City does not match.");
                Assert.AreEqual("NY", row["state"], "State does not match.");
                Assert.AreEqual("10001", row["postalCode"], "Postal Code does not match.");
                Assert.AreEqual("USA", row["country"], "Country does not match.");
                Assert.AreEqual(1370, row["salesRepEmployeeNumber"], "Sales Rep Employee Number does not match.");
                Assert.AreEqual(50000.00, Convert.ToDouble(row["creditLimit"]), 0.01, "Credit Limit does not match.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            dbConnection.CloseConnection();
        }
    }
}
