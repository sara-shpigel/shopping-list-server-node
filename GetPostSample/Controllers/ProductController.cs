using LinqLesson;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GetPostSample.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("addOrder")]
        public IActionResult AddOrder([FromBody] Order newOrder)
        {
            try
            {
                string connectionString = "server=localhost;user=root;password=root;database=shopping-list;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO orders (full_name, full_address, email, order_details) VALUES (?, ?, ?, ?)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@full_name", newOrder.Full_name);
                    insertCommand.Parameters.AddWithValue("@full_address", newOrder.Full_address);
                    insertCommand.Parameters.AddWithValue("@email", newOrder.Email);
                    insertCommand.Parameters.AddWithValue("@order_details", newOrder.Order_details);

                    insertCommand.ExecuteNonQuery();

                }

                return Ok(new { Message = "Order added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to add order. Error: {ex.Message}" });
            }
        }
    }
}
