using System;
using System.IO;
using System.Text;

namespace IAList
{
    class Program
    {
        static string DIRSQL = "Query.sql";  //  @"C:\Users\luizf\IAList3.sql";
        static string FOLDER = @"E:\";
        static string EXT = "*.";

        static void Main(string[] args)
        {
            Leia();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(DIRSQL, true))
            {
                file.WriteLine("CREATE TABLE IF NOT EXISTS `IAList` (`id` int(10) NOT NULL auto_increment, `nome` varchar(255), `tamanho` varchar(255), `diretorio` varchar(255), PRIMARY KEY( `id` )); ");
                file.WriteLine("INSERT INTO `IAList` (`nome`, `tamanho`, `diretorio`) VALUES");
            }
            ListFiles(FOLDER, EXT);
            ListFolder(FOLDER, EXT);
            Console.ReadKey();
        }
        static void Leia() {

            Console.Write(@"Diretorio de Pesquisa :::  ");
            FOLDER = Console.ReadLine();
            if (FOLDER == "") FOLDER = @"E:\";

            Console.Write(@"Extenção: pdf, jpge :::  ");
            EXT += Console.ReadLine();
            if (EXT == "*.") EXT = "*";
        }
        static void ListFiles(string Dirname, string atributo)
        {
            DirectoryInfo di = new DirectoryInfo(Dirname);
            FileInfo[] files = di.GetFiles(atributo, SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                
                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(DIRSQL, true))
                {

                    string format = @"('" + file.Name + "', '" + file.Length + "', '" + FormatString(file.FullName) + "'),";
                    txt.WriteLine(format);
                    Console.WriteLine(format);
                }
            }
        }
        static void ListFolder(string Dirname, string atribute)
        {
            DirectoryInfo di = new DirectoryInfo(Dirname);
            DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in directories)
            {
                ListFiles(@dir.FullName, atribute);
                ListFolder(dir.FullName, atribute);
            }
        }
        static string FormatString(string STR)
        {
            StringBuilder sb = new StringBuilder(STR, 50);
            sb.Replace(@"\", "\\\\");
            sb.Replace(@"'", "''''");
            return sb.ToString();
        } 
    }
}

