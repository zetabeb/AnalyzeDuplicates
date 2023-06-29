using ZBB;
using System.IO;
namespace AnalizeDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aplicación para análisis de archivos repetidos");
            Console.WriteLine("¿Desea continuar?");
            Console.WriteLine("1 - Sí");
            Console.WriteLine("2 - Salir");
            short opcion = Convert.ToInt16(Console.ReadLine());
            if(opcion == 1 )
                do
                {                
                    AnalizarArchivos();
                    Console.WriteLine("¿Desea volver a analizar?");
                    Console.WriteLine("1 - Sí");
                    Console.WriteLine("2 - Salir");
                    opcion = Convert.ToInt16(Console.ReadLine());
                }
                while (opcion == 1);
            Console.WriteLine(":::Gracias por utilizar AnalyzeDuplicates:::");
        }
        static void AnalizarArchivos()
        {
            //string root = @"D:\ZAMIR\libros-20230626T163654Z-001\libros";
            Console.WriteLine("Ingrese la ruta de la carpeta a analizar");
            string root = Convert.ToString(Console.ReadLine());
            bool correcto = Directory.Exists(root);
            if (!correcto)
            {
                Console.WriteLine("No ingresó una ruta correcta");
                return;
            }
            Console.Clear();
            string[] FoldersList = Directory.GetDirectories(root, "*", SearchOption.AllDirectories);
            string[] FilesList = Directory.GetFiles(root, "*", SearchOption.AllDirectories);

            int folderCount = FoldersList.Count();
            int fileCount = FilesList.Count();

            Console.WriteLine("Folders (" + folderCount + "): \n");
            foreach (string folder in FoldersList)
            {
                Console.WriteLine(folder);
            }

            Console.WriteLine("\n\nFiles (" + fileCount + "):\n");
            foreach (string files in FilesList)
            {
                Console.WriteLine(files);
            }

            List<DuplicateFiles> DuplicateFilesList = FileManager.GetDuplicateFiles(root);

            Console.WriteLine("\n\nDuplicados:");
            foreach (var d in DuplicateFilesList)
            {
                Console.WriteLine("\n" + d.Name);
                foreach (var df in d.Files)
                {
                    Console.WriteLine(df + " (" + FileManager.GetLongitud(df) + ")");
                }
            }
            Console.WriteLine("\n ¿Desea realizar alguna acción?");
            Console.WriteLine("1 - Sí");
            Console.WriteLine("2 - No");
            short opcion = Convert.ToInt16(Console.ReadLine());

            if(opcion == 1 )
                do
                {
                    VerArchivos(DuplicateFilesList);
                } 
                while (opcion == 1);
            Console.Clear();
        }
        static void VerArchivos(List<DuplicateFiles> duplicateFilesList)
        {            
            foreach (var d in duplicateFilesList)
            {
                short opcionContinuar = 1;
                int NroArchivo = 1;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Archivo Nro "+NroArchivo);
                    Console.WriteLine("");
                    Console.WriteLine(d.Name);
                    for(int i=0; i<d.Files.Count; i++)
                    {
                        if (File.Exists(d.Files[i]))
                            Console.WriteLine((i+1)+"-> " + d.Files[i] + " (" + FileManager.GetLongitud(d.Files[i]) + ")");
                        else
                            Console.WriteLine((i + 1) + "-> " + d.Files[i]);
                    }
                    Console.WriteLine("\n ¿Desea eliminar algún archivo?");
                    Console.WriteLine("1 - Sí");
                    Console.WriteLine("2 - No");
                    short opcion = Convert.ToInt16(Console.ReadLine());
                    short opcionFile = 0;
                    short opcionEliminar = 0;
                    if (opcion == 1)
                    {
                        Console.WriteLine("Ingrese el número correspondiente al archivo que desea eliminar");
                        opcionFile = Convert.ToInt16(Console.ReadLine());
                        if(opcionFile > d.Files.Count)
                        {
                            bool opcionValida;
                            do
                            {
                                Console.WriteLine("No ingresó una opción válida");
                                Console.WriteLine("Ingrese el número correspondiente al archivo que desea eliminar");
                                opcionFile = Convert.ToInt16(Console.ReadLine());
                                if (opcionFile > d.Files.Count) opcionValida = false;
                                else opcionValida = true;
                            }
                            while (!opcionValida);
                        }
                        string URIFile = d.Files[opcionFile - 1];                        

                        Console.WriteLine("¿Está seguro que desea eliminar el archivo? \n " + URIFile);
                        Console.WriteLine("1 - Sí");
                        Console.WriteLine("2 - No");
                        opcionEliminar = Convert.ToInt16(Console.ReadLine());

                        if(opcionEliminar == 1)
                        {
                            Console.WriteLine("Eliminando...");
                            if(URIFile == "Archivo Eliminado")
                            {
                                Console.WriteLine("El archivo ya había sido eliminado");
                                opcionEliminar = 2;
                            }
                            else
                            {
                                FileManager.EliminarArchivo(URIFile);
                                Console.WriteLine("Se ha eliminado el archivo : " + URIFile);
                            }                            
                        }
                        if(opcionEliminar == 2)
                        {
                            Console.WriteLine("No se ha eliminado el archivo");
                        }
                    }
                    Console.WriteLine("\n¿Desea continuar al siguiente archivo?");
                    Console.WriteLine("1 - Sí");
                    Console.WriteLine("2 - No");
                    if (opcionEliminar == 1) Console.WriteLine("3 - No, deshacer última acción");
                    opcionContinuar = Convert.ToInt16(Console.ReadLine());

                    if(opcionContinuar == 3 && opcionEliminar == 1)
                    {
                        short indexFile;
                        if ((opcionFile - 1) == 0)
                            indexFile = 1;
                        else
                            indexFile = 0;
                        File.Copy(d.Files[indexFile], d.Files[opcionFile - 1]);
                    }
                    else if (opcionEliminar == 1)
                        d.Files[opcionFile - 1] = "Archivo Eliminado";
                } 
                while (opcionContinuar != 1);
                NroArchivo += 1;
            }
        }
        static void EliminarDuplicado()
        {

        }
    }
    
}