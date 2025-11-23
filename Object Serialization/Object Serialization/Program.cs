using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Object_Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileManager.ManageFiles();
            Person p = new Person
            {
                Name = "Ali",
                Age = 30,
                Address=new Address
                {
                    City="Lahore",
                    ZipCode=54000,
                    State="Pak"
                }

            };

            string jsonOutput=JsonSerializer.Serialize(p);
            //is person k object ko json main convert kry ga jo 
            //string me convert kr dia
            Console.WriteLine(jsonOutput);
            File.WriteAllText("myfile.txt",jsonOutput);

            string jsonString = File.ReadAllText("myfile.txt");
            Person p2 = JsonSerializer.Deserialize<Person>(jsonString);
            Console.WriteLine(p2.Name);
        }
    }
}