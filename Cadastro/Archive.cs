using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace Marco_Targino.DR2.AT
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

        public static void Save(Aniversariante aniversariante)
        {
            var existente = FindRecordName(aniversariante.Name);
            if (existente == null)
            {
                string savefile = $"{aniversariante.Name},{aniversariante.Birthdate.Date.ToString()}\n";

                File.AppendAllText(FileName(), savefile);
            }
            else
            {
                Clear();
                WriteLine("\nCadastro já existente!\n\nPressione qualquer tecla para voltar ao Menu.");
                ReadKey(); Presentation.MainMenu();
            }
        }


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

        public static IEnumerable<Aniversariante> Cadastrados(string fullname)
        {
            return from dude in Cadastrados() where dude.Name.Contains(fullname) orderby dude.Name select dude;
        }

        public static IEnumerable<Aniversariante> Cadastrados(DateTime birthdate)
        {
            return from dude in Cadastrados() where dude.Birthdate.Month == birthdate.Month && dude.Birthdate.Day == birthdate.Day orderby dude.Name select dude;
        }

        public static Aniversariante FindRecordName(string fullname)
        {
            return (from dude in Cadastrados() where dude.Name == fullname select dude).FirstOrDefault();
        }

        public static void DeleteRecord(Aniversariante aniversariante)
        {
            string fullname = aniversariante.Name;

            var oldones = File.ReadAllLines(FileName());
            var newones = oldones.Where(line => !line.Contains(fullname));
            File.WriteAllLines(FileName(), newones);
        }

        public static IEnumerable<Aniversariante> BirthToday()
        {
            return from dude in Cadastrados() where dude.Birthdate.Day == DateTime.Today.Day && dude.Birthdate.Month == DateTime.Today.Month orderby dude.Age select dude;
        }
    }
}
