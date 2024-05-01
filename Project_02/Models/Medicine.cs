using SQLite;
using System;
using System.Collections.Generic;

namespace Project_02.Models
{
    public class Medicine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image {  get; set; }

    }
}
