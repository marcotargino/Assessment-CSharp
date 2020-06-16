using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Threading;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Aniversario
{
    class Presentation
    {
        public static void MainMenu()
        {
            WriteLine("--------------------\nLISTA DE ANIVERSARIO\n--------------------");
            WriteLine("Menu do Sistema\n\nSelecione uma opção:\n");
            WriteLine("1- Cadastrar aniversariante;");
            WriteLine("2- Pesquisar aniversariante;");
            WriteLine("3- Alterar cadastro;");
            WriteLine("4- Deletar cadastro;");
            WriteLine("0- Sair");

            char choice = ReadLine().ToCharArray()[0];

            switch (choice)
            {
                case '1': Record(); break;                  //(1)
                case '2': Search(); break;                  //(2)
                case '3': Shift(); break;                   //(3)
                case '4': Delete(); break;                  //(4)
                
                case '0': 
                    Environment.Exit(0); 
                    break;

                default:
                    Clear(); WriteLine("OPÇÃO INVÁLIDA!");
                    Thread.Sleep(2000); Clear();
                    MainMenu();
                    break;
            }

            static void Record()                            //(1)
            {
                Clear();

                Write("Entre com o nome e sobrenome: ");
                string fullname = ReadLine();

                Write("Entre com a data de nascimento no formato YYYY/MM/DD: ");
                var birthdate = DateTime.Parse(ReadLine());

                var aniversariante = new Aniversariante();
                aniversariante.Name = fullname;
                aniversariante.Birthdate = birthdate;
                DataBase.Save(aniversariante);
 
                WriteLine("\nCADASTRO REALIZADO COM SUCESSO!"); Thread.Sleep(1000);
                Write("\n\nPressione qualquer tecla para voltar ao Menu.");
                ReadKey(); Clear(); MainMenu();
            }

            static void Search()                            //(2)
            {
                Clear();
                Options();
            }

            static void Options()
            {
                Clear();

                WriteLine("Escolha uma opção para consulta:\n");
                WriteLine("1- Pesquisar aniversariante pelo nome;");
                WriteLine("2- Pesquisar aniversariante pela data;");
                WriteLine("3- Listar todos os aniversariantes;");
                WriteLine("0- Voltar ao Menu do Sistema.");

                char choice = ReadLine().ToCharArray()[0];

                switch (choice)
                {
                    case '1': FindName(); break;            //[1]
                    case '2': FindDate(); break;            //[2]
                    case '3': ListAll(); break;             //[3]
                    
                    case '0': 
                        Clear(); 
                        MainMenu(); 
                        break;

                    default:
                        Write("OPÇÃO INVÁLIDA!");
                        ReadKey();
                        Search();
                        break;
                }

                ReadKey();
                Clear();
                MainMenu();
            }

            static void FindName()                          //[1]
            {
                Clear();

                Write("Entre com o nome ou sobrenome: ");
                string fullname = ReadLine();

                var pesquisa = DataBase.Cadastrados(fullname);
                var resultado = new List<Aniversariante>();

                
                foreach (var aniversariante in pesquisa)
                {
                    resultado.Add(aniversariante);
                }

                foreach (var aniversariante in resultado)
                {
                    WriteLine($"{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                    CountTime(aniversariante.Birthdate);
                }
                
                if(resultado.Count == 0)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!");
                    Thread.Sleep(1500);
                }

                Write("\n\nPressione qualquer tecla para voltar ao Menu.");
            }

            static void FindDate()                          //[4]
            {
                Clear();

            }

            static void CountTime(DateTime aniversariante)
            {
                DateTime today = DateTime.Today.Date;
                DateTime nextBirthday = new DateTime(today.Year, aniversariante.Month, aniversariante.Day);

                int age = (today.Year - aniversariante.Year);

                if (nextBirthday < today)
                {
                    nextBirthday = nextBirthday.AddYears(1);
                }

                var daysLeft = (nextBirthday - today).Days;
                if (daysLeft == 0)
                {
                    Write($"\nHoje é o seu aniversário de {age} anos!");
                }
                else
                {
                    Write($"\nRestam apenas {daysLeft} dias para o seu aniversário de {age} anos.");
                }
            }

            static void ListAll()                           //[3]
            {
                Clear();

                var pesquisa = DataBase.Cadastrados();
                var resultado = new List<Aniversariante>();

                foreach(var aniversariante in pesquisa)
                {
                    resultado.Add(aniversariante);
                }

                if(resultado.Count != 0)
                {
                    foreach (var aniversariante in DataBase.Cadastrados())
                    {
                        WriteLine($"{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                    }
                }
                else
                {
                    WriteLine("NENHUM CADASTRO ENCONTRADO!");
                    Thread.Sleep(1500);
                }

                Write("\nPressione qualquer tecla para voltar ao Menu.");
            }

            static void Shift()                             //(3)
            {
                Clear();

                Write("Entre com o nome completo: ");
                string fullname = ReadLine();

                var aniversariante = DataBase.FindRecord(fullname);

                if(aniversariante == null)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!");
                    Thread.Sleep(1500);
                    Clear();
                }
                else
                {
                    DataBase.DeleteRecord(aniversariante);
                    Write("\nEntre com o novo nome: ");
                    string newname = ReadLine();
                    aniversariante.Name = newname;
                    DataBase.Save(aniversariante);
                    WriteLine("\nCADASTRO ALTERADO COM SUCESSO!");
                    Thread.Sleep(1500);
                }
                WriteLine("\nPressione qualquer tecla para voltar ao Menu.");
                ReadKey(); Clear();  Presentation.MainMenu(); 
            }

            static void Delete()                            //(4)
            {
                Clear();

                Write("Entre com o nome completo: ");
                string fullname = ReadLine();

                var aniversariante = DataBase.FindRecord(fullname);

                if (aniversariante == null)
                {
                    Write("\nNENHUM CADASTRO ENCONTRADO!");
                    Thread.Sleep(1500); Clear();
                }
                else
                {
                    DataBase.DeleteRecord(aniversariante);
                    Write("\nCADASTRO REMOVIDO COM SUCESSO!");
                    Thread.Sleep(1500); Clear();
                }
                WriteLine("\nPressione qualquer tecla para voltar ao Menu.");
                ReadKey(); Clear();  Presentation.MainMenu();
            }
        }
    }
}
