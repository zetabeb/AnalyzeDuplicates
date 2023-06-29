using ZBB;
using System.IO;
namespace AnalizeDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = @"D:\ZAMIR\libros-20230626T163654Z-001\libros";
            string[] FoldersList = Directory.GetDirectories(root, "*", SearchOption.AllDirectories);
            string[] FilesList = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            
            int folderCount = FoldersList.Count();
            int fileCount = FilesList.Count();            

            Console.WriteLine("Folders ("+folderCount+"): \n");
            foreach (string folder in FoldersList)
            {
                Console.WriteLine(folder);
            }

            Console.WriteLine("\n\nFiles ("+fileCount+"):\n");
            foreach (string files in FilesList)
            {
                Console.WriteLine(files);
            }

            List<DuplicateFiles> DuplicateFilesList = FileManager.GetDuplicateFiles(root);

            Console.WriteLine("\n\nDuplicados:");
            foreach(var d in DuplicateFilesList)
            {
                Console.WriteLine("\n"+d.Name);
                foreach(var df in d.Files)
                {                    
                    Console.WriteLine(df+" ("+FileManager.GetLongitud(df)+")");
                }
            }
            Console.ReadLine();
        }
    }
    
}