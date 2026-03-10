using static System.Console;

namespace Object_Serialization
{
    internal class FileManager
    {
        internal static void ManageFiles()
        {
            string textFile = Path.Combine(
                Environment.CurrentDirectory, "streams.txt"
                );
            StreamWriter sw = File.CreateText(textFile);
            WriteLine(File.Exists(textFile));
            sw.WriteLine("This is some dummy text");
            sw.Close();

            //
            WriteLine($"Folder Name:{Path.GetDirectoryName(textFile)}"); 
            WriteLine($"Folder Name:{Path.GetFileName(textFile)}");
            WriteLine($"Folder Name without Extension:{Path.GetFileNameWithoutExtension(textFile)}");
            WriteLine($"Folder Name:{Path.GetExtension(textFile)}");


            var info = new FileInfo(textFile);
            WriteLine($"{textFile} contains {info.Length} bytes");
            WriteLine($"Last Accessed: {info.LastAccessTime}");
            WriteLine($"Is ReadOnly: {info.IsReadOnly}");

            string backupFile=Path.Combine(Environment.CurrentDirectory, "mybackup.bak");
            File.Copy(sourceFileName:textFile,destFileName:backupFile,overwrite:true);
            ReadLine();
            File.Delete(textFile);

            StreamReader sr=File.OpenText(backupFile);
            WriteLine(sr.ReadToEnd());
            sr.Close();

            WriteLine(File.ReadAllText(backupFile));




        }
    }
}
