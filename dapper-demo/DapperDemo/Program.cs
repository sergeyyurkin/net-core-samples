using System;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DapperDemo
{
    class Program
    {
        /**
         * SOURCE: https://dotnetcoretutorials.com/2019/08/05/dapper-in-net-core-part-4-dapper-contrib/
         */

        static void Main(string[] args)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=DapperExample;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventName = connection.QueryFirst<string>("SELECT TOP 1 EventName FROM Event WHERE Id = 1");
                Console.WriteLine(eventName);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var myEvent = connection.QueryFirst<Event>("SELECT * FROM Event WHERE Id = 1");
                Console.WriteLine(myEvent.Id + " : " + myEvent.EventName);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int eventId = 1;
                var myEvent = connection.QueryFirst<Event>("SELECT Id, EventName FROM Event WHERE Id = @Id", new { Id = eventId });
                Console.WriteLine(myEvent.Id + " : " + myEvent.EventName);
            }

            // Updating A Record
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("UPDATE Event SET EventName = 'NewEventName' WHERE Id = 1");
            }

            // Inserting A Record
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO Event (EventLocationId, EventName, EventDate, DateCreated) VALUES(1, 'InsertedEvent', '2019-01-01', GETUTCDATE())");
            }

            // Delete A Record
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("DELETE FROM Event WHERE Id = 4");
            }

            // Inserting Records Using Dapper.Contrib
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var newEvent = Event.Create(1, "Contrib Inserted Event", DateTime.Now.AddDays(1), DateTime.UtcNow);
                connection.Insert(newEvent);
            }

            // Get A Record By Id Using Dapper.Contrib
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventId = 1;
                var myEvent = connection.Get<Event>(eventId);
            }

            // Updating Records Using Dapper.Contrib
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventId = 1;
                var myEvent = connection.Get<Event>(eventId);
                myEvent.EventName = "New Name";
                connection.Update(myEvent);
            }

            // Delete Records Using Dapper.Contrib
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Delete(new Event { Id = 5 });
            }

            Console.ReadLine();
        }
    }
}
