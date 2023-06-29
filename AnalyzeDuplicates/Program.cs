using ZBB;
using System.IO;
namespace AnalizeDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = @"D:\ZAMIR\libros-20230626T163654Z-001\libros";
            //List<string> listFolders = new List<string>();
            //listFolders = ZBB.FileManager.ListarDirectorios(ruta);
            //listFolders.Add(ruta);
            string[] FoldersList = Directory.GetDirectories(root, "*", SearchOption.AllDirectories);
            string[] FilesList = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            string[] FilesListCompare = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            int folderCount = FoldersList.Count();
            int fileCount = FilesList.Count();
            
            List<DuplicateFiles> DuplicateFilesList = new List<DuplicateFiles>();

            Console.WriteLine("Folders: \n");
            foreach (string folder in FoldersList)
            {
                Console.WriteLine(folder);
            }

            Console.WriteLine("\n\nFiles:\n");
            foreach (string files in FilesList)
            {
                Console.WriteLine(files);
            }
                        
            foreach (string file in FilesList)
            {                
                DuplicateFiles duplicateFiles = new DuplicateFiles();
                List<string> files = new List<string>();
                
                FileInfo file1 = new FileInfo(file);
                string fileName = file1.Name;

                files.Add(file);
                duplicateFiles.Name = fileName;                

                for (int i=0; i < FilesListCompare.Length; i++)
                {
                    if (FilesListCompare[i] == "") continue;
                    FileInfo file2 = new FileInfo(FilesListCompare[i]);
                    string fileCompareName = file2.Name;                    
                    
                    if(fileName == fileCompareName)
                    {                        
                        if (file != FilesListCompare[i])
                        {
                            files.Add(FilesListCompare[i]);                            
                        }
                        FilesListCompare[i] = "";
                    }                    
                }
                if (files.Count > 1)
                {
                    duplicateFiles.Files.AddRange(new List<string>(files));
                    DuplicateFilesList.Add(duplicateFiles);
                }
                
                files.Clear();
            }

            Console.WriteLine("\n\nDuplicados:\n");
            foreach(var d in DuplicateFilesList)
            {
                Console.WriteLine(d.Name);
                foreach(var df in d.Files)
                {
                    Console.WriteLine(df);
                }
            }
            Console.ReadLine();
        }
    }
    public class DuplicateFiles
    {
        public string Name { get; set; }
        public List<string> Files { get; set; } = new List<string>();
    }
}