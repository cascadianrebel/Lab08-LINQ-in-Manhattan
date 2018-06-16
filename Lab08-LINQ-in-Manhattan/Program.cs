using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Lab08_LINQ_in_Manhattan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to display a list of neighborhoods in Manhattan!");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.Read();

            // read file into a string and deserialize JSON to an object type
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText("../../../data.json"));

            //allHoodValues : all values for the Neighborhood properties included empty strings
            var allHoodValues = from n in rootObject.Features
                                select n;

            //allNeighborhoods : all the neighborhood property values excluded empty strings but including duplicates
            var allNeighborhoods = from n in rootObject.Features
                                   where n.Properties.Neighborhood != ""
                                   select n;
            
            //distinctNeighborhoods : all the neighborhoods excluding duplicates
            var distinctNeighborhoods = allNeighborhoods.GroupBy(x => x.Properties.Neighborhood)
                                                        .Select(n => n.First());

            //allDistinctNeighborhoods : consolidated query of allHoodValues, AllNeighborhoods, and distinctNeighborhoods
            var allDistinctNeighborhoods = from n in rootObject.Features
                                           where n.Properties.Neighborhood != ""
                                           orderby n.Properties.Neighborhood
                                           group n by n.Properties.Neighborhood into g
                                           select g.First();
                                           
        
            foreach(var i in allDistinctNeighborhoods)
            {
                Console.WriteLine(i.Properties.Neighborhood);
            }

        }

    }
}
