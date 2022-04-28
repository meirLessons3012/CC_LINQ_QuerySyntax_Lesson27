using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CC_LINQ_QuerySyntax_Lesson27
{
    internal class Car
    {
        public int CarNumber { get; set; }
        public string Color { get; set; }
        public int NumOfDoors { get; set; }
        public int PersonId { get; set; }

        public Car()
        {

        }
        public Car(int carNumber, string color, int numOfDoors, int personId)
        {
            CarNumber = carNumber;
            Color = color;
            NumOfDoors = numOfDoors;
            PersonId = personId;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
