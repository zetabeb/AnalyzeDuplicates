using System.Collections.Generic;

namespace ZBB
{
    public static class FileManager
    {
        //para saber cuantos archivos dejaron
        public static List<string> BuscarArchivos(string? @dir, string? format = "")
        {
            List<string> NamesFile = new List<string>();

            if(VerficarCarpeta(@dir))
            {
                DirectoryInfo di = new DirectoryInfo(@dir);

                string _format = string.IsNullOrEmpty(format) ? "" : "."+format;

                foreach (var fi in di.GetFiles("*"+_format))
                {
                    if (!(fi.Name.Contains("~$"))) NamesFile.Add(fi.Name); //para ignorar archivos temp -- sucede cuando hay un archivo abierto
                }
            }
            return NamesFile;
        }
        public static bool VerficarCarpeta(string? dir)
        {
            if (string.IsNullOrEmpty(@dir))
            {                
                return false;
            }
            //si no existe la carpeta crearla
            if (!Directory.Exists(@dir))
            {
                System.IO.Directory.CreateDirectory(@dir);
            }
            return true;
        }
        public static void EliminarArchivo(string? dir)
        {
            try
            {                
                if (File.Exists(dir))
                {
                   File.Delete(dir);
                   Console.WriteLine("Archivo eliminado.");
                }
                else
                {
                   Console.WriteLine("El archivo ya no existe.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al borrar archivo origen: {0}", e.ToString());
            }
        }        
        public static string GetLongitud(string? dir)
        {            
            FileInfo file = new FileInfo(dir);
            long longitudBytes = file.Length;            
            float longitud = longitudBytes;
            short veces = 0;
            string resultado;

            if (longitudBytes > 1024)
            {
                do
                {
                    longitud = longitud / 1024;
                    veces+= 1;
                } 
                while (longitud > 1024);

                switch (veces)
                {
                    case 1: resultado = longitud.ToString("0.000") + "KB"; break;
                    case 2: resultado = longitud.ToString("0.000") + "MB"; break;
                    case 3: resultado = longitud.ToString("0.000") + "GB"; break;
                    case 4: resultado = longitud.ToString("0.000") + "TB"; break;
                    default: resultado = "error"; break;
                }
            }                
            else
                resultado = longitudBytes.ToString() + "B";

            return resultado;
        }
        public static List<DuplicateFiles> GetDuplicateFiles(string root)
        {
            string[] FilesList = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            string[] FilesListCompare = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            List<DuplicateFiles> DuplicateFilesList = new List<DuplicateFiles>();

            foreach (string file in FilesList)
            {
                DuplicateFiles duplicateFiles = new DuplicateFiles();
                List<string> files = new List<string>();

                FileInfo file1 = new FileInfo(file);
                string fileName = file1.Name;

                files.Add(file);
                duplicateFiles.Name = fileName;

                for (int i = 0; i < FilesListCompare.Length; i++)
                {
                    if (FilesListCompare[i] == "") continue;
                    FileInfo file2 = new FileInfo(FilesListCompare[i]);
                    string fileCompareName = file2.Name;

                    if (fileName == fileCompareName)
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
                    duplicateFiles.Name += " (" + files.Count + ")";
                    duplicateFiles.Files.AddRange(new List<string>(files));
                    DuplicateFilesList.Add(duplicateFiles);
                }

                files.Clear();
            }
            return DuplicateFilesList;
        }

    }
    public class DuplicateFiles
    {
        public string Name { get; set; }
        public List<string> Files { get; set; } = new List<string>();
    }
}