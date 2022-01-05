using System;
using System.Collections.Generic;
using System.Text;

namespace Racing2021.Models
{
    public abstract class Person
    {
        private int _id;
        private string _name;
        private int _age;
        private string _nationality;
        private int _teamId;

        public string Name { get => _name; set { _name = value; } }
        public int Age { get => _age; set { _age = value; } }
        public int Id { get => _id; set { _id = value; } }
        public string Nationality { get => _nationality; set { _nationality = value; } }
        public int TeamId { get => _teamId; set { _teamId = value; } }
    }
}
