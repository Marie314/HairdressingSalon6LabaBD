﻿namespace HairdressingSalon.App.DAL.DTO
{
    public class OrderCreated
    {
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
        public int WorkerId { get; set; }
    }
}
