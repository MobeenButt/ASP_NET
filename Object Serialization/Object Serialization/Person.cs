namespace Object_Serialization
{
    internal class Person 
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    internal class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }

}
