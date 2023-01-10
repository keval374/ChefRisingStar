using LTDCWebservice.Models;

Console.WriteLine("Hello, World!");

School school = new School
{
    Address = "700 Wellington, Montreal, Quebec",
    City = "Montreal",
    Name = "Main Office"
};
try
{


    using (var db = new LtdcContext())
    {
        //long nextId = db.Schools.Max(s => s.Id);
        //school.Id = nextId+1;

        db.Schools.Add(school);
        int result = db.SaveChanges();
        Console.WriteLine($"Successfully inserted school id:{school.Id}. Result: {result}");
    } 
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Press enter to exit");
Console.ReadLine();
