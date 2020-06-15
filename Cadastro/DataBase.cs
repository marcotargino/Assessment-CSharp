using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.Text;

namespace Aniversario
{
    class DataBase
    {
        static List<Aniversariante> Cadastro = new List<Aniversariante>();

        public static void Save(Aniversariante aniversariante)
        {
            var exist = Cadastro.Find(i => i == aniversariante);

            if(exist == null)
            {
                Cadastro.Add(aniversariante);
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

        public static Aniversariante FindRecord(string fullname)
        {
            return Cadastro.Find(aniversariante => aniversariante.Name == fullname);
        }

        public static void DeleteRecord(Aniversariante aniversariante)
        {
            Cadastro.Remove(aniversariante);
        }
    }
}