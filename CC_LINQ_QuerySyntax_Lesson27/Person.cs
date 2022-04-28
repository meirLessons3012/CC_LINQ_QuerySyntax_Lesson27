using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CC_LINQ_QuerySyntax_Lesson27
{
    internal class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        public Person(int id, string fullName, string phone)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
