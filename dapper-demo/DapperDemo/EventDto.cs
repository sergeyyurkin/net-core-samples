using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperDemo
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public int EventLocationId { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime DateCreated { get; set; }


        public static Event Create(int locationId, string name, DateTime date, DateTime created)
        {
            return new Event
            {
                EventLocationId = locationId,
                EventName = name,
                EventDate = date,
                DateCreated = created
            };
        }
    }
}
