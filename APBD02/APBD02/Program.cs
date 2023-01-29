using APBD02.Models;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

HashSet<Student> students = new HashSet<Student>();
HashSet<Studies> studieCurses = new HashSet<Studies>();
string adres = args[0];
string path = args[1];

if (System.IO.Directory.Exists(path))
{
    throw new ArgumentException("Podana sciezka jest niepoprawna.");

}
if (!File.Exists(adres))
{
    throw new FileNotFoundException("Plik nie istnieje");
}
string format = args[2];
FileInfo fi = new FileInfo("dane.csv");
var reader = new StreamReader(fi.OpenRead());

string line = null;

while ((line = reader.ReadLine()) != null)
{
    {
        Student student = new Student();
        Studies s = new Studies();
        List<string> studentsData = line.Split(",").ToList<string>();

        if (studentsData.Count() != 9)
        {
            using (StreamWriter sw = File.AppendText(@"C:\Users\Anna Wyszyńska\Desktop\Damianus\APBD\APBD02\APBD02\log.txt"))
            {
                sw.WriteLine("Zła liczba kolumn, log: " + line);
                sw.Close();
            }
        }

        else if (checkifColumnisEmpty(studentsData))
        {
            using (StreamWriter sw = File.AppendText(@"C:\Users\Anna Wyszyńska\Desktop\Damianus\APBD\APBD02\APBD02\log.txt"))
            {
                sw.WriteLine("Pusty rekord, log: " + line);
                sw.Close();
            }
        }
        else
        {
            student.firstName = studentsData[0];
            student.lastName = studentsData[1];
            s.name = studentsData[2];
            s.mode = studentsData[3];
            student.indexNumber = studentsData[4];
            student.birthDate = DateTime.Parse(studentsData[5]);
            student.email = studentsData[6];
            student.fathersName = studentsData[7];
            student.mothersName = studentsData[8];
            student.studies = s;

            students.Add(student);
            studieCurses.Add(s);
        }
    }
}

List<Student> listaStudentow = students.ToList();
List<Studies> coursesList = studieCurses.ToList();
University university = new University
{
    studenci = null,
    kierunki = coursesList,
};
var jsonString = JsonSerializer.Serialize(university);
Console.WriteLine(jsonString);
bool checkifColumnisEmpty(List<string> column)
{
    return column.Any(e => e.Count() == 0);
}