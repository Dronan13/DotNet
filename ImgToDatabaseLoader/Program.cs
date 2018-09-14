using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImgToDatabaseLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            string connetionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ImgDatabase;User ID=username;Password=password";
            string dir = "imgDir";// your dirrectory with images
           
            SqlConnection connetion = new SqlConnection(connetionString);

            try
            {
                connetion.Open();
                
                Console.WriteLine("Connection Open ! ");

                List<string> files = new List<string>();

                files = Directory.GetFiles(dir, "*.JPG", SearchOption.AllDirectories).ToList();

                Console.Write("filenames: "); Console.WriteLine(files.Count);
                int counter = 0;

                foreach (string f in files)
                {
                    Console.Clear();
                    counter++;
                    byte[] image = File.ReadAllBytes(f);
                    string filename = Path.GetFileNameWithoutExtension(f);

                    Console.WriteLine("Inserting {0} out of {1}", counter, files.Count);

                    SqlCommand sqlCommand = 
                            new SqlCommand(@"INSERT INTO Image (filename, ImageData) 
                                             VALUES (@filename, @image)", connetion);

                    sqlCommand.Parameters.AddWithValue("@image", image);
                    sqlCommand.Parameters.AddWithValue("@filename", filename);
                    sqlCommand.ExecuteNonQuery();
                   
                }
                connetion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection !");
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
