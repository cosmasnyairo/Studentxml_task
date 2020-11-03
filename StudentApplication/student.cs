using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

namespace StudentApplication

{
    public class Student
    {

        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("gender")]
        public string gender { get; set; }

        [XmlElement("id")]
        public int id { get; set; }

        [XmlElement("year")]
        public int year { get; set; }

        [XmlElement("no_of_courses")]
        public int no_of_courses { get; set; }

    }
    public class Students
    {


        public List<Student> students = new List<Student>();

        public void SerializeFile(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var xml = new XmlSerializer(typeof(Students));

                xml.Serialize(stream, this);
            }
        }

        public Students deSerializeFile(string filename)
        {

            string data = File.ReadAllText(filename);
            var xml = new XmlSerializer(typeof(Students));
            Students fetchedstudents = (Students)xml.Deserialize(new StringReader(data));
            foreach (Student student in fetchedstudents.students)
            {   
                Console.WriteLine(student.name);
                Console.WriteLine(student.gender);
                Console.WriteLine(student.id);
                Console.WriteLine(student.no_of_courses);
                Console.WriteLine(student.year);
                Console.WriteLine();

            }
            return fetchedstudents;
        }
    }
}