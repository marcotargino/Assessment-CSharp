using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace Aniversario
{
    public class Archive
    {
        private static string FileName()
        {
            var folder = Environment.SpecialFolder.Desktop; 
            var local = Environment.GetFolderPath(folder); 
            var filename = @"\marco_targino_dr2_at.txt";
            return local + filename;
        }

        //static List<Aniversariante> Cadastro = new List<Aniversariante>();

        //public static void Save(Aniversariante aniversariante)
        //{
        //    var exist = Cadastro.Find(i => i.Name == aniversariante.Name);

        //    if (exist == null)
        //    {
        //        Cadastro.Add(aniversariante);
        //    }
        //    else
        //    {
        //        WriteLine("\nCadastro já existente!\n\nPressione qualquer tecla para voltar ao Menu.");
        //        ReadKey(); Clear(); Presentation.MainMenu();
        //    }
        //}

        public static void Save(Aniversariante aniversariante)
        {
            var existente = FindRecordName(aniversariante.Name);
            if (existente == null)
            {
                string savefile = $"{aniversariante.Name},{aniversariante.Birthdate.Date.ToString()}\n";

                File.AppendAllText(FileName(), savefile); //cria/abre o file, adiciona o conteudo ao file e fecha;
            }
            else
            {
                Clear();
                WriteLine("Ja cadastrado. Pressione uma tecla para voltar ao menu principal");
                ReadKey(); Presentation.MainMenu();
            }
        }

        //public static IEnumerable<Aniversariante> Cadastrados()
        //{
        //    return Cadastro;
        //}

        public static List<Aniversariante> Cadastrados()
        {
            var filename = FileName();
            FileStream file;
            if (!File.Exists(filename))
            {
                file = File.Create(filename);
                file.Close();
            }

            string[] indent = File.ReadAllLines(FileName()).ToArray();
            List<Aniversariante> Cadastro = new List<Aniversariante>();

            for (int i = 0; i < indent.Length; i++)
            {
                string[] filedata = indent[i].Split(',');
                string nome = filedata[0];
                DateTime birthdate = DateTime.Parse(filedata[1]);

                Aniversariante aniversariante = new Aniversariante()
                {
                    Name = nome,
                    Birthdate = birthdate
                };
                Cadastro.Add(aniversariante);
            }
            return Cadastro;
        }

        //public static IEnumerable<Aniversariante> Cadastrados(string fullname)
        //{
        //    return Cadastro.Where(aniversariante => aniversariante.Name.Contains(fullname, StringComparison.InvariantCultureIgnoreCase));
        //}

        public static IEnumerable<Aniversariante> Cadastrados(string fullname)
        {
            return (from dude in Cadastrados() where dude.Name.Contains(fullname) orderby dude.Name select dude);
        }

        //public static IEnumerable<Aniversariante> Cadastrados(DateTime birthdate)
        //{
        //    return Cadastro.Where(aniversariante => aniversariante.Birthdate.Date == birthdate);
        //}

        public static IEnumerable<Aniversariante> Cadastrados(DateTime birthdate)
        {
            return (from dude in Cadastrados() where dude.Birthdate.Month == birthdate.Month && dude.Birthdate.Day == birthdate.Day orderby dude.Name select dude);
        }

        //public static Aniversariante FindRecordName(string fullname)
        //{
        //    return Cadastro.Find(aniversariante => aniversariante.Name == fullname);
        //}

        public static Aniversariante FindRecordName(string fullname)
        {
            return (from dude in Cadastrados() where dude.Name == fullname select dude).FirstOrDefault();
        }

        //    public static void DeleteRecord(Aniversariante aniversariante)
        //{
        //    Cadastro.Remove(aniversariante);

        //}

        public static void DeleteRecord(Aniversariante aniversariante)
        {
            string fullname = aniversariante.Name;

            var oldones = File.ReadAllLines(FileName());
            var newones = oldones.Where(line => !line.Contains(fullname));
            File.WriteAllLines(FileName(), newones);
        }

        //public static IEnumerable<Aniversariante> BirthToday()
        //{
        //    return (from dude in Cadastrados() where dude.Birthdate.Day == DateTime.Today.Day && dude.Birthdate.Month == DateTime.Today.Month orderby dude.Age select dude);
        //}

        public static IEnumerable<Aniversariante> BirthToday()
        {
            return (from dude in Cadastrados() where dude.Birthdate.Day == DateTime.Today.Day && dude.Birthdate.Month == DateTime.Today.Month orderby dude.Name select dude);
        }
    }
}
