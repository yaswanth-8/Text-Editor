using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Text_Editor.Models;

namespace Text_Editor.Controllers
{
	public class TexteditorController : Controller
	{
        IConfiguration _configuration;
        public TexteditorController(IConfiguration configuration)
        {
            _configuration = configuration;
            //_Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Index(TextEditor obj)
        {
            SqlConnection _Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("InsertData", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@content", obj.Content);
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            SqlConnection _Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Text_Editor", _Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<TextEditor> list = new List<TextEditor>();
            while (reader.Read())
            {
                TextEditor file = new TextEditor();
                file.Id = reader.GetInt32("Id");
                file.Name = reader.GetString("File_Name");
                file.Content = reader.GetString("Content");
                list.Add(file);
            }
            return View(list);
        }




        public IActionResult Edit(int itemId)
        {
            Console.WriteLine("Inside edit and id is : " + itemId);
            SqlConnection _Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _Connection.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Text_Editor WHERE Id={itemId}", _Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            TextEditor file = new TextEditor();
            while (reader.Read())
            {
                file.Id = reader.GetInt32("Id");
                file.Name = reader.GetString("File_Name");
                file.Content = reader.GetString("Content");
            }
            return View(file);
        }

        [HttpPost]
        public IActionResult Edit(TextEditor obj)
        {
            SqlConnection _Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("updateData", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@content", obj.Content);
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int itemId)
        {
            Console.WriteLine("Inside Delete and id is : " + itemId);
            SqlConnection _Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _Connection.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM Text_Editor WHERE Id={itemId}", _Connection);
            cmd.ExecuteNonQuery();
            return RedirectToAction("List");
        }
    }
}
