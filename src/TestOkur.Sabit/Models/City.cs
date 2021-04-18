using System.Collections.Generic;

namespace TestOkur.Sabit.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; } 
    }
}
