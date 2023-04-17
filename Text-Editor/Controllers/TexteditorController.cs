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
            return View();
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
    }
}
