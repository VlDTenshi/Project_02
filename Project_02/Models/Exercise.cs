using SQLite;

namespace Project_02.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDes { get; set; }
        public string Image { get; set; }
    }
}
