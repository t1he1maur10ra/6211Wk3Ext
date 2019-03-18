using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = new WebClient().DownloadString("https://uinames.com/api/?ext&amount=100");

            List<RootObject> data = new JavaScriptSerializer().Deserialize<List<RootObject>>(json);

            List<RootObject> maleData = new List<RootObject>();
            List<RootObject> femaleData = new List<RootObject>();

            foreach(RootObject x in data)
            {
                if (x.gender == "female")
                    femaleData.Add(x);//Add method
                else
                    maleData.Add(x);
            }

            Console.WriteLine($"There are {femaleData.Count} females and {maleData.Count} males in the dataset downloaded...");

            string[] names = new string[femaleData.Count];
            int counter = 0;
            Console.WriteLine("\n... Original Data ...");
            foreach (RootObject x in femaleData)
            {
                Console.WriteLine($"\nName: {x.name} {x.surname}\nGender: {x.gender}");
                names[counter] = $"{x.surname}, {x.name}";//Create my own composite key for each person and add to an array
                counter++;
            }

            Array.Sort(names);//Sort my array in alphabetical order
            Console.WriteLine("\n... Sorted array ...");
            foreach(string x in names)
                Console.WriteLine($"Name: {x}");

            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("... Sorting the data ...");

            RootObject[] females = new RootObject[femaleData.Count];
            femaleData.CopyTo(females);
            Console.WriteLine(femaleData[0].surname);
            RootObject[] sortedFemales = new RootObject[femaleData.Count];
            for (int i = 0; i< females.Length; i++)
            {
                for(int j = 0; j < females.Length; j++)
                {
                    if ($"{females[j].surname}, {females[j].name}" == names[i])
                    {
                        sortedFemales[i] = females[j];
                        Console.WriteLine($"{i}: Person added. {j} internal loops required");
                    }
                    
                }
                Console.WriteLine($"Sorted: {sortedFemales[i].name}\nNames: {names[i]}");
            }
            
            foreach(RootObject x in sortedFemales)
            {
                Console.WriteLine($"Name: {x.surname}, {x.name}");
            }


            Console.ReadLine();

        }
    }
}
