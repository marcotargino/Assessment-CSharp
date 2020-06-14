using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Text;

namespace Aniversario
{
    class DataBase
    {
        static List<Class> Cadastro = new List<Class>();

        public static void Save(Class aniversariante)
        {
            Cadastro.Add(aniversariante);
        }

        public static IEnumerable<Class> Cadastrados()
        {
            return Cadastro;
        }

        public static IEnumerable<Class> Cadastrados(string fullname)
        {
            return Cadastro.Where(aniversariante => aniversariante.Name.Contains(fullname, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<Class> Cadastrados(DateTime birthdate)
        {
            return Cadastro.Where(aniversariante => aniversariante.Birthdate.Date == birthdate);
        }
    }
}