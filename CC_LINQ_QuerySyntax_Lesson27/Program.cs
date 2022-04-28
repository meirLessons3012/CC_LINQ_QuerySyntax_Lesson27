using CC_LINQ_QuerySyntax_Lesson27;
using System.Reflection;

List<Person> persons = new List<Person>()
{
    new Person(1,"shimi tavori", "0541231412"),
    new Person(2,"riki cohen", "05476213563"),
    new Person(3,"KOBI AZIMI", "05476235234"),
    new Person(4,"Sahar rubi", "0547636324"),
    new Person(5,"aVI SADON", "0542341")
};

List<Car> cars = new List<Car>()
{
    new Car(235124,"red",4,1),
    new Car(365235,"silver",3,0),
    new Car(2352521,"yellow",2,5),
    new Car(95745,"black",1,4),
    new Car(5123412,"gray",4,1),
    new Car(512413241,"gray",4,3),
};

SelectMethod(persons);
OrderByMethod(cars);
WhereMethod(cars);
GroupByMethod(cars);
JoinMethod(persons, cars);
LetKeyword(persons);

#region Reflection

Type typeFromInstance = typeof(Car);

Car c = Activator.CreateInstance<Car>();

Assembly a = Assembly.LoadFile(@"C:\Users\meir7\source\repos\Courses\30.12\CC_ActionFucPredicate_Event_Lesson24\CC_ActionFucPredicate_Event_Lesson24\bin\Debug\net6.0\CC_ActionFucPredicate_Event_Lesson24.dll");
//Car c2 = (Car)Activator.CreateInstance(typeof(Car));
Type[] types = a.GetTypes();
foreach (Type t in types)
{
    Console.WriteLine(t.Name);
    Console.WriteLine(t.Namespace);
    foreach (MethodInfo method in t.GetMethods())
    {
        Console.WriteLine(method.ReturnType);
        Console.WriteLine(method.IsPublic);
        Console.WriteLine(method.IsPrivate);
    }

    foreach (PropertyInfo prop in t.GetProperties())
    {
        Console.WriteLine(prop.Name);
    }        
}

Type t3 = typeof(Car);
Type t4 = typeof(Person);

#endregion

#region 

static void SelectMethod(List<Person> persons)
{
    var allFullNames = from person in persons
                       select person.FullName + ", ";

    List<string> person2 = allFullNames.ToList();//lazy evaluation
}

static void OrderByMethod(List<Car> cars)
{
    var orderedCars = from car in cars
                      orderby car.NumOfDoors, car.PersonId, car.CarNumber descending
                      select car;

    orderedCars.ToList().ForEach(car => Console.WriteLine(car));
}

static void WhereMethod(List<Car> cars)
{
    var carNumbersLessThan4Doors = (from car in cars
                                    where car.NumOfDoors < 4 && car.Color.StartsWith("b")
                                    select car.CarNumber).ToList();

    carNumbersLessThan4Doors.ForEach(car => Console.WriteLine(car));
}

static void GroupByMethod(List<Car> cars)
{
    var groupedCars = (from car in cars
                       group car by car.PersonId).ToList();

    groupedCars.ForEach(igp =>
    {
        Console.WriteLine($"Key: {igp.Key}");
        igp.ToList().ForEach(car => Console.WriteLine(car));
    });

}

#endregion

static void JoinMethod(List<Person> persons, List<Car> cars)
{
    //join = inner join
    var result = from prs in persons
                 join car in cars
                 on prs.Id equals car.PersonId
                 where prs.Phone.EndsWith("4")
                 select new
                 {
                     PersonFullName = prs.FullName,
                     PersonPhone = prs.Phone,
                     CarCarNumber = car.CarNumber,
                     CarColor = car.Color,
                     CarNumOfDoors = car.NumOfDoors
                 };

    result.ToList().ForEach(result => Console.WriteLine(result));

    var groupedCarsByPerson = (from prs in persons
                               join car in cars
                               on prs.Id equals car.PersonId
                               into carsOfPerson
                               where carsOfPerson.Count() > 0
                               select new
                               {
                                   PersonName = prs.FullName,
                                   PersonPhone = prs.Phone,
                                   Cars = carsOfPerson
                               }).ToList();

    groupedCarsByPerson.ForEach(personWithCars =>
    {
        Console.WriteLine($"Person Details: {personWithCars.PersonName + " " + personWithCars.PersonPhone}.\nCars:");
        personWithCars.Cars.ToList().ForEach((car) => Console.WriteLine("\t" + car));
        Console.WriteLine();
    });

    //same way
    foreach (var personWithCars in groupedCarsByPerson)
    {
        Console.WriteLine($"Person Details: {personWithCars.PersonName + " " + personWithCars.PersonPhone}.\nCars:");
        foreach (Car car in personWithCars.Cars)
        {
            Console.WriteLine("\t" + car);
        }
    }

}

static void LetKeyword(List<Person> persons)
{
    var lowerCasePersonName = from prs in persons
                                  //where prs.FullName.ToLower().Contains("a")
                                  //select prs;
                              let nameAsLower = prs.FullName.ToLower()
                              where nameAsLower.Contains("a")
                              select nameAsLower;
}




