using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.Text;
using System.Net.NetworkInformation;

namespace Aniversario
{
    class DataBase
    {
        static List<Aniversariante> Cadastro = new List<Aniversariante>();

        public static void Save(Aniversariante aniversariante)
        {
            var exist = Cadastro.Find(i => i.Name == aniversariante.Name);

            if(exist == null)
            {
                Cadastro.Add(aniversariante);
            }
            else
            {
                WriteLine("\nCadastro já existente!\n\nPressione qualquer tecla para voltar ao Menu.");
                ReadKey(); Clear(); Presentation.MainMenu();
            }
        }

        public static IEnumerable<Aniversariante> Cadastrados()
        {
            return Cadastro;
        }

        public static IEnumerable<Aniversariante> Cadastrados(string fullname)
        {
            return Cadastro.Where(aniversariante => aniversariante.Name.Contains(fullname, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<Aniversariante> Cadastrados(DateTime birthdate)
        {
            return Cadastro.Where(aniversariante => aniversariante.Birthdate.Date == birthdate);
        }

        public static Aniversariante FindRecordName(string fullname)
        {
            return Cadastro.Find(aniversariante => aniversariante.Name == fullname);
        }

        public static void DeleteRecord(Aniversariante aniversariante)
        {
            Cadastro.Remove(aniversariante);
        }

        public static IEnumerable<Aniversariante> BirthToday()
        {
            return (from dude in Cadastrados() where dude.Birthdate.Day == DateTime.Today.Day && dude.Birthdate.Month == DateTime.Today.Month orderby dude.Age select dude);
        }
    }
}