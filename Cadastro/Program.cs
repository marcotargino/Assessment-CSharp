using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Aniversario
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        //static List<Pessoas> cadastros = new List<Pessoas>();

        /////////////////////////CRUD/////////////////////////
        static void MainMenu()
        {
            WriteLine("--------------------\nLISTA DE ANIVERSARIO\n--------------------");
            WriteLine("Menu do Sistema\n\nSelecione uma opção:\n");
            WriteLine("1- Cadastrar aniversariante;");
            WriteLine("2- Pesquisar aniversariante;");
            WriteLine("3- Alterar cadastro;");
            WriteLine("4- Excluir cadastro;");
            WriteLine("5- Sair.");

            char Option = ReadLine().ToCharArray()[0];

            switch (Option)
            {
                case '1':
                    Cadastrar();
                    break;

                case '2':
                    Pesquisar();
                    break;

                case '3':
                    Alterar();
                    break;

                default:
                    WriteLine("Opção inválida!");
                    break;
            }
        }

        /////////////////////////RECORD/////////////////////////
        public static void Cadastrar()
        {
            Clear();

            Write("Entre com o primeiro nome: ");
            string firstname = ReadLine();

            Write("Entre com o sobrenome: ");
            string surname = ReadLine();

            string name = (firstname + " " + surname);
            //WriteLine(Name);

            Write("Entre com a data de nascimento no formato AAAA, MM, DD: ");
            DateTime birthdate = DateTime.Parse(ReadLine());
            //WriteLine(Birthdate);

            Aniversariante aniversariante = new Aniversariante();
            aniversariante.Name = name;
            aniversariante.Birthdate = birthdate;

            DataBase.Save(aniversariante);

            WriteLine("\nCADASTRO REALIZADO COM SUCESSO!\n\nPressione qualquer tecla para voltar ao Menu.");
            ReadKey();
            Clear();
            MainMenu();
        }

        /////////////////////////SEARCH/////////////////////////
        public static void Pesquisar()
        {
            Clear();

            Options();
        }

        static void Options()
        {
            Clear();

            WriteLine("Escolha uma opção para consulta:");
            WriteLine("1- Buscar aniversariante pelo nome");
            WriteLine("2- Listar todos os aniversariantes");

            char Option = ReadLine().ToCharArray()[0];

            switch (Option)
            {
                case '1':
                    FindName();
                    break;

                case '2':
                    ListAll();
                    break;

                default:
                    Write("Opção inválida!");
                    ReadKey();
                    Pesquisar();
                    break;
            }

            ReadKey();
            Clear();
            MainMenu();
        }

        /////////////////////////LIST/////////////////////////
        static void ListAll()
        {
            Clear();

            foreach (Aniversariante aniversariante in DataBase.Cadastrados())
            { 
                WriteLine($"Nome: {aniversariante.Name} - Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                //WriteLine(aniversariante.Birthdate.ToString("dd/MM/yyyy"));
            }

            Write("\nPressione qualquer tecla para voltar ao Menu.");
        }

        static void FindName()
        {
            Clear();

            Write("Entre como o nome ou sobrenome: ");
            string name = ReadLine();
            Write(name);

            //var pesquisa = DataBase.Cadastrados().Where(aniversariante => aniversariante.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /////////////////////////CHANGE/////////////////////////
        public static void Alterar()
        {
            Clear();


        }
    }
}
