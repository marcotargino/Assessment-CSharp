using System;
using System.Collections.Generic;
using System.Text;

namespace Aniversario
{
    class DataBase
    {
        static List<Aniversariante> Cadastro = new List<Aniversariante>();

        public static void Save(Aniversariante aniversariante)
        {
            Cadastro.Add(aniversariante);
        }

        public static List<Aniversariante> Cadastrados()
        {
            return Cadastro;
        }
    }
}