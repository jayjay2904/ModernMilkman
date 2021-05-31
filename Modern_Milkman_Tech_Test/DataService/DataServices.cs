using Microsoft.Extensions.Configuration;
using Modern_Milkman_Tech_Test.Interfaces;
using Modern_Milkman_Tech_Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Modern_Milkman_Tech_Test.DataService.cs
{
    public class DataServices : IDataServices
    {
        private readonly string conn;
        public DataServices(IConfiguration _conn)
        {
            conn = GlobalSettings.GetModernMilkmanConnection(_conn);
        }

        public List<Customer> getAllCustomers()
        {
            var allCustomers = new List<Customer>();
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"Select [Id]
                                ,[Title]
                               ,[Forename]
                                ,[Surname]
                                ,[Email_Address]
                                ,[Mobile]
                                ,[Active]
                                FROM [Customer]";
                    allCustomers = db.Query<Customer>(sql).ToList();

                }
                return allCustomers;
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return null;
            }
        }
        public List<Customer> getAllActiveCustomers()
        {
            var allActiveCustomers = new List<Customer>();
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"Select [Id]
                                ,[Title]
                               ,[Forename]
                                ,[Surname]
                                ,[Email_Address]
                                ,[Mobile]
                                ,[Active]
                                FROM [Customer]
                                WHERE Active = 1";
                    allActiveCustomers = db.Query<Customer>(sql).ToList();

                }
                return allActiveCustomers;
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return null;
            }
        }
        public bool checkCustomerExists(string email)
        {
            var addNewCustomer = new List<Customer>();
            var parameters = new { Customeremail = email };
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"Select [Id]
                                ,[Title]
                               ,[Forename]
                                ,[Surname]
                                ,[Email_Address]
                                ,[Mobile]
                                ,[Active]
                                FROM [Customer]
                                WHERE Email_Address = @email";
                    addNewCustomer = db.Query<Customer>(sql, parameters).ToList();

                }
                if (addNewCustomer.Count < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return true;
            }
        }
        public bool checkAddressExists(string address1, string postcode)
        {
            var checkAddress = new List<Address>();
            var parameters = new { Address1 = address1, PostCode = postcode };
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"Select [Address1] 
                                [PostCode]
                                FROM [Address]
                                WHERE Address1 = @Address1 AND PostCode = @Postcode";
                    checkAddress = db.Query<Address>(sql, parameters).ToList();

                }
                if (checkAddress.Count < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return true;
            }
        }
        public void addNewCustomer(Customer newCustromer)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = "INSERT INTO Customer" +
                        "([Title]," +
                        "[Forename]," +
                        "[Surname]," +
                        "[Email_Address]," +
                        "[Mobile]," +
                        "[Active]" +
                        "VAULES(@Title,@Forename,@Surname,@Email_Address,@Mobile,@Active";

                    var result = db.Execute(sql, new
                    {
                        Title = newCustromer.Title,
                        Forename = newCustromer.Forename,
                        Surname = newCustromer.Surname,
                        Email_Address = newCustromer.Email_Address,
                        Mobile = newCustromer.Moblie,
                        Active = 1
                    });

                }
            }

            catch (Exception ex)
            {
                //ToDo: Add logging to db
            }
        }
        public void addNewAddress(Address newAddress)
        {
            var country = "";
            if (String.IsNullOrEmpty(newAddress.Country))
            {
                country = "UK";
            }
            else
            {
                country = newAddress.Country;
            }
            if (newAddress.Primary_Address)
            {
                updatePreviousPrimaryAddress(newAddress.Address1, newAddress.PostCode);
            }

            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = "INSERT INTO Address" +
                        "([Address1]," +
                        "[Address2]," +
                        "[Town]," +
                        "[County]," +
                        "[PostCode]," +
                        "[Country]" +
                        "[CustomerID]," +
                        "[Primary_Address]" +
                        "VAULES(@Address1,@Address2,@Town,@County,@Postcode,@Country,@CustomerId,@Primary_Address";

                    var result = db.Execute(sql, new
                    {
                        Address1 = newAddress.Address1,
                        Address2 = newAddress.Address2,
                        Town = newAddress.Town,
                        County = newAddress.County,
                        Postcode = newAddress.PostCode,
                        Country = country,
                        CustomerId = newAddress.CustomerID,
                        Primary_Address = newAddress.Primary_Address

                    });


                }
            }

            catch (Exception ex)
            {
                //ToDo: Add logging to db
            }
        }
        public void updatePreviousPrimaryAddress(string address1, string postcode)
        {
            var checkAddress = new List<Address>();
            var parameters = new { Address1 = address1, PostCode = postcode };
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"UPDATE [Address]
                                SET [Primary_Address] = 0
                                WHERE Address1 = @Address1 AND PostCode = @Postcode";
                    checkAddress = db.Query<Address>(sql, parameters).ToList();

                }

            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
            }
        }
        public bool setCustomertoInActive(string Email)
        {
            var parameters = new { Email = Email};
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"UPDATE [Customer]
                                SET [Active] = 0
                                WHERE Email = @Email ";
                    db.Execute(sql, parameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return false;
            }
        }
        public List<Address> getCustomerAddresses(int custId)
        {
            var allCustomerAddress = new List<Address>();
            var parameters = new { CustomerId = custId };
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"Select [Address1]
                                [Address2]
                                [Town]
                                [County]
                                [Postcode]
                                [Country]
                                [CustomerId]
                                [Primary_Address]
                                FROM [Address]
                                WHERE CustomerId = 1";
                    allCustomerAddress = db.Query<Address>(sql).ToList();

                }
                return allCustomerAddress;
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return null;
            }
        }
        public bool DeleteCustomer(int Id, string email)
        {
            var parameters = new { Id = Id, Email = email };
            // Delete all addresses associated with this customer.
            // this call is only accessable from this method
            DeleteAllAddress(Id);
            // Delete customer details.
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"DELETE FROM [Customer]
                                WHERE Id = @Id AND Email = @email";
                    var result = db.Execute(sql, parameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
                return false;
            }

        }
        private void DeleteAllAddress(int CustId)
        {
            var parameters = new { customerId = CustId };
            try
            {
                using (IDbConnection db = new SqlConnection(conn))
                {
                    var sql = @"DELETE FROM [Address]
                                WHERE CustomerId = @customerId";
                    var result = db.Execute(sql, parameters);

                }
            }
            catch (Exception ex)
            {
                //ToDo: Add logging to db
            }

        }
        public bool DeleteSingleAddress(int CustomerId, string Postcode)
        {
            var parameters = new { Postcode = Postcode };
            var checkAddresses = getCustomerAddresses(CustomerId);
            if (checkAddresses.Count > 1)
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(conn))
                    {
                        var sql = @"DELETE FROM [Address]
                                WHERE Postcode = @Postcode";
                        var result = db.Execute(sql, parameters);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    //ToDo: Add logging to db
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
