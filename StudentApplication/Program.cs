using System;
using MySql.Data.MySqlClient;

namespace StudentApplication
{
    class Program
    {
        static void Main(string[] args)
        {


            void serializexmfile(string filename, Students fetchedstudents)
            {
                //code to serialize to xml

                Console.WriteLine();
                Console.WriteLine("Starting Serialization");

                fetchedstudents.SerializeFile(filename);
                Console.WriteLine("File saved at: " + filename);
                Console.WriteLine("Serialization complete!");
                Console.WriteLine();
            }

            void deserializexmfile(string filename, Students fetchedstudents)
            {
                //code to deserialize from xml
                Console.WriteLine("Starting Deserialization");

                Console.WriteLine("Fetched data is :");

                Console.WriteLine();

                fetchedstudents.deSerializeFile(filename);
                Console.WriteLine("Deserialization complete!");

                Console.WriteLine();
            }


            void fetchfromdb()
            {
                MySqlConnection conn = null;
                MySqlCommand cmd = null;

                Students students = new Students();
                int i = 0;

                try
                {
                    string connStr = "SERVER=localhost;" + "DATABASE=student;" + "UID=root;" + "PWD= ;";
                    string sql = "SELECT * FROM student_table";
                    conn = new MySqlConnection(connStr);
                    cmd = new MySqlCommand(sql, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.id = reader.GetInt16("StudentID");
                        student.name = reader.GetString("Name");
                        student.gender = reader.GetString("gender");
                        student.no_of_courses = reader.GetInt16("No_of_courses");
                        student.year = reader.GetInt16("year");

                        students.students.Add(student);

                        i++;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("An error occurred {0}", ex.Message));
                }
                finally
                {
                    if (conn != null) conn.Close();
                }

                //serialize xml
                serializexmfile("data/student.xml", students);

                //check if it deserialized
                deserializexmfile("data/student.xml", students);


            }

            fetchfromdb();


        }

    }
}

