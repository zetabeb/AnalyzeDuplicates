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
        public static List<string> ListarDirectorios(string? dir)
        {
            List<string> levelOneList = new List<string>();
            List<string> list = new List<string>();
            string[] dirs = Directory.GetDirectories(dir);

            foreach (string subDir in dirs)
            {
                levelOneList.Add(subDir);                
            }

            List<string> result = new List<string>();
            result = levelOneList;
            list.AddRange(result);
            do
            {
                foreach (var item in result)
                {
                    result = GetSubDirectorio(item);
                    list.AddRange(result);
                }
            } 
            while (result.Count > 0);

            return list;
        }
        private static List<string> GetSubDirectorio(string? dir)
        {
            string[] dirs = Directory.GetDirectories(dir);
            List<string> subDirs = new List<string>();
            //foreach (string subDir in dirs)
            //{
            //    subDirs.Add(subDir);
            //}
            
                foreach (string subDir in dirs)
                {
                    subDirs.Add(subDir);
                    GetSubDirectorio(subDir);
                }
            
            
            return subDirs;
        }
    }
}