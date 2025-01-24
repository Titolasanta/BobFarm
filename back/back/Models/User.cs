using System;

namespace CornFarmBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime? LastTimeCornBought { get; set; }
        public int CornBought { get; set; }
    }
}