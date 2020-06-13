using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections.Generic;
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
            WriteLine("4- Deletar cadastro;");
            WriteLine("0- Sair");
            

            char Option = ReadLine().ToCharArray()[0];

            switch (Option)
            {
                case '1': Cadastrar(); break;
                case '2': Pesquisar(); break;
                case '3': Alterar(); break;

                case '0': Sair(); break;

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
            var birthdate = DateTime.Parse(ReadLine());
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
            WriteLine("1- Buscar aniversariante pelo nome;");
            WriteLine("2- Listar todos os aniversariantes;");
            WriteLine("0- Voltar ao Menu do Sistema.");

            char Option = ReadLine().ToCharArray()[0];

            switch (Option)
            {
                case '1': FindName(); break;
                case '2': ListAll(); break;
                case '0': Clear(); MainMenu(); break;

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

        /////////////////////////LISTNAME/////////////////////////
        static void FindName()
        {
            Clear();

            Write("Entre com o nome ou sobrenome: ");
            string name = ReadLine();
            //Write(name);

            var pesquisa = DataBase.Cadastrados().Where(aniversariante => aniversariante.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            var resultado = new List<Aniversariante>();

            foreach (var aniversariante in pesquisa)
            {
                resultado.Add(aniversariante);
            }

            foreach (var aniversariante in resultado)
            {
                WriteLine($"{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
            }

            Write("\nPressione qualquer tecla para voltar ao Menu.");
        }

        /////////////////////////LISTALL/////////////////////////
        static void ListAll()
        {
            Clear();

            var resultado = new List<Aniversariante>();

            foreach (Aniversariante aniversariante in DataBase.Cadastrados())
            { 
                WriteLine($"{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                //WriteLine(aniversariante.Birthdate.ToString("dd/MM/yyyy"));
            }

            Write("\nPressione qualquer tecla para voltar ao Menu.");
        }

        /////////////////////////CHANGE/////////////////////////
        public static void Alterar()
        {
            Clear();


        }
        /////////////////////////EXIT/////////////////////////
        public static void Sair()
        {
            Environment.Exit(0);
        }
    }
}
