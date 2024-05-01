using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_02.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDes { get; set; }
        public string Image { get; set; }
    }
}
