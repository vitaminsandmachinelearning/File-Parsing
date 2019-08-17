using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Parse
{
    class Program
    {
        static List<Part> Parts;
        static void Main(string[] args)
        {
            //Set up database and set location of csv to load
            string connectionString = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = DB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            string csvFileName = "C:\\Users\\Jake\\source\\repos\\File Parse\\File Parse\\stock.csv";

            //Parse data from file into Parts object list
            FileParser parser = new FileParser();
            Parts = parser.LoadFile(csvFileName);

            //Print parts to check if they are loaded properly
            foreach (Part p in Parts)
            {
                Console.WriteLine(p.ToString());
            }

            //Make DBManager, will output if connection to database was successful
            DBManager manager = new DBManager(connectionString);

            //Add parts to the database via the manager
            manager.AddParts(Parts);

            //Call this to update database when parts are updated in csv
            manager.UpdateParts(Parts);

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
