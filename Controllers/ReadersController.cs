using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using library_backend.Models;

namespace library_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ReadersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select reader_id, surname, name, patronymic, password, email, address, phone
                            from
                            dbo.Readers
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Readers rea)
        {
            string query = @"
                            insert into dbo.Readers
                            values (@surname, @name, @patronymic, @password, @email, @address, @phone)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@surname", rea.surname);
                    myCommand.Parameters.AddWithValue("@name", rea.name);
                    myCommand.Parameters.AddWithValue("@patronymic", rea.patronymic);
                    myCommand.Parameters.AddWithValue("@password", rea.password);
                    myCommand.Parameters.AddWithValue("@email", rea.email);
                    myCommand.Parameters.AddWithValue("@address", rea.address);
                    myCommand.Parameters.AddWithValue("@phone", rea.phone);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Readers rea)
        {
            string query = @"
                            update dbo.Readers
                            set surname = @surname, name = @name, patronymic = @patronymic, password = @password, email = @email, address = @address, phone = @phone
                            where reader_id = @reader_id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@reader_id", rea.reader_id);
                    myCommand.Parameters.AddWithValue("@surname", rea.surname);
                    myCommand.Parameters.AddWithValue("@name", rea.name);
                    myCommand.Parameters.AddWithValue("@patronymic", rea.patronymic);
                    myCommand.Parameters.AddWithValue("@password", rea.password);
                    myCommand.Parameters.AddWithValue("@email", rea.email);
                    myCommand.Parameters.AddWithValue("@address", rea.address);
                    myCommand.Parameters.AddWithValue("@phone", rea.phone);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Readers
                            where reader_id = @reader_id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@reader_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
