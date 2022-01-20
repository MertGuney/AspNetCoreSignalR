using System;

namespace CovidChart.Api.Models
{
    public enum ECity
    {
        Istanbul = 1,
        Ankara,
        Izmir,
        Eskisehir,
        Diyarbakır,
        Trabzon
    }
    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
