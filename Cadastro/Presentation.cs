using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Threading;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;

namespace Marco_Targino.DR2.AT
{
    class Presentation
    {
        public static void MainMenu()
        {
            BirthDay();
            WriteLine("\n--------------------\nLISTA DE ANIVERSARIO\n--------------------");
            WriteLine("Menu do Sistema\n\nSelecione uma opção:\n");
            WriteLine("1 - Cadastrar aniversariante;");
            WriteLine("2 - Pesquisar aniversariante;");
            WriteLine("3 - Alterar cadastro;");
            WriteLine("4 - Deletar cadastro;");
            WriteLine("ESC para sair do Sistema");

            ConsoleKeyInfo keyMenu = ReadKey();

            switch (keyMenu.Key)
            {
                case ConsoleKey.D1: Record(); break;                            //(1)
                case ConsoleKey.D2: Search(); break;                            //(2)
                case ConsoleKey.D3: Shift(); break;                             //(3)
                case ConsoleKey.D4: Delete(); break;                            //(4)

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                default:
                    Clear(); WriteLine("OPÇÃO INVÁLIDA!");
                    Thread.Sleep(2000); Clear();
                    MainMenu();
                    break;
            }

            static void BirthDay()
            {
                var birthToday = Archive.BirthToday().ToList();

                if (birthToday.Count != 0)
                {
                    WriteLine("Aniversariantes de hoje:\n");
                    foreach (var aniversariante in birthToday)
                    {
                        WriteLine($"{aniversariante.Name}");
                    }
                }
            }

            static void Record()                                                //(1)
            {
                Clear();

                Write("Entre com o nome e sobrenome: ");
                string fullname = ReadLine();

                Write("Entre com a data de nascimento no formato YYYY/MM/DD: ");
                var birthdate = DateTime.Parse(ReadLine());
                var today = DateTime.Today.Date;
                var aniversariante = new Aniversariante();
                int age = today.Year - birthdate.Year;
                aniversariante.Name = fullname;
                aniversariante.Birthdate = birthdate;
                aniversariante.Age = age;

                Archive.Save(aniversariante);

                WriteLine("\nCADASTRO REALIZADO COM SUCESSO!"); Thread.Sleep(1000);

                WriteLine("\nPressione ENTER para fazer novos cadastros ou ESC para voltar ao Menu Principal.");

                ConsoleKeyInfo keyExit = ReadKey();
                switch (keyExit.Key)
                {
                    case ConsoleKey.Enter: Clear(); Record(); break;
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }
            }

            static void Search()                                                //(2)
            {
                Clear();
                Options();
            }

            static void Options()
            {
                Clear();

                WriteLine("Escolha uma opção para consulta:\n");
                WriteLine("1 - Pesquisar aniversariante pelo nome;");
                WriteLine("2 - Pesquisar aniversariante pela data;");
                WriteLine("3 - Listar todos os aniversariantes;");
                WriteLine("ESC para voltar ao Menu do Sistema.");

                ConsoleKeyInfo keyOptions = ReadKey();

                switch (keyOptions.Key)
                {
                    case ConsoleKey.D1: FindName(); break;                      //[1]
                    case ConsoleKey.D2: FindDate(); break;                      //[2]
                    case ConsoleKey.D3: ListAll(); break;                       //[3]

                    case ConsoleKey.Escape:
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

            static void FindName()                                              //[1]
            {
                Clear();

                Write("Entre com o nome completo: ");
                string fullname = ReadLine();

                var pesquisa = Archive.Cadastrados(fullname);
                var resultado = new List<Aniversariante>();


                foreach (var aniversariante in pesquisa)
                {
                    resultado.Add(aniversariante);
                }

                foreach (var aniversariante in resultado)
                {
                    WriteLine($"\n{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                    CountTime(aniversariante.Birthdate);
                }

                if (resultado.Count == 0)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);
                }

                WriteLine("\nPressione ENTER para fazer nova pesquisa ou ESC para voltar ao Menu Principal.");

                ConsoleKeyInfo keyName = ReadKey();
                switch (keyName.Key)
                {
                    case ConsoleKey.Enter: Clear(); Search(); break;
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }
            }


            static void FindDate()                                              //[2]
            {
                Clear();

                Write("Entre com a data no formato YYYY/MM/DD: ");
                var birthdate = DateTime.Parse(ReadLine());

                var pesquisa = Archive.Cadastrados(birthdate);
                var resultado = pesquisa.ToList();

                foreach (var aniversariante in resultado)
                {
                    WriteLine($"\nNome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                    CountTime(aniversariante.Birthdate);
                }

                if (resultado.Count == 0)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);
                }

                WriteLine("\nPressione ENTER para fazer nova pesquisa ou ESC para voltar ao Menu Principal.");

                ConsoleKeyInfo keyDate = ReadKey();
                switch (keyDate.Key)
                {
                    case ConsoleKey.Enter: Clear(); Search(); break;
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }
            }

            static void CountTime(DateTime aniversariante)
            {
                DateTime today = DateTime.Today.Date;
                DateTime nextBirthday = new DateTime(today.Year, aniversariante.Month, aniversariante.Day);

                int age = today.Year - aniversariante.Year;

                if (nextBirthday < today)
                {
                    nextBirthday = nextBirthday.AddYears(1);
                    age = age + 1;
                }

                var daysLeft = (nextBirthday - today).Days;
                if (daysLeft == 0)
                {
                    Write($"\nHoje é o seu aniversário de {age} anos!\n");
                }
                else
                {
                    Write($"Restam apenas {daysLeft} dias para o seu aniversário de {age} anos.\n");
                }
            }

            static void ListAll()                                       //[3]
            {
                Clear();

                var pesquisa = Archive.Cadastrados();
                var resultado = new List<Aniversariante>();

                foreach (var aniversariante in pesquisa)
                {
                    resultado.Add(aniversariante);
                }

                foreach (var aniversariante in resultado)
                {
                    WriteLine($"{resultado.IndexOf(aniversariante)}. Nome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                }
                Write("\nPressione ENTER para mais detalhes ou ESC para voltar.");

                ConsoleKeyInfo keyInfo = ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter: MoreInfo(resultado); break;
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }

                if (resultado.Count == 0)
                {
                    WriteLine("NENHUM CADASTRO ENCONTRADO!");
                    Thread.Sleep(1500);
                }

                WriteLine("\nPressione ENTER para fazer nova pesquisa ou ESC para voltar ao Menu Principal.");

                ConsoleKeyInfo key = ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Enter: Clear(); Search(); break;
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }
            }

            static void MoreInfo(IEnumerable<Aniversariante> aniversariante)
            {
                WriteLine("Entre com o índice do aniversariante para ver mais detalhes.");
                var index = int.Parse(ReadLine());
                var resultado = new List<Aniversariante>();

                foreach (var id in aniversariante)
                {
                    if (aniversariante.ToList().IndexOf(id) == index) { resultado.Add(id); break; }
                }

                if (resultado.Count() == 0)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para tentar novamente ou ESC para voltar ao Menu.");

                    ConsoleKeyInfo keyError = ReadKey();
                    switch (keyError.Key)
                    {
                        case ConsoleKey.Enter: Clear(); ListAll(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }
                else
                {
                    Clear();

                    foreach (var id in resultado)
                    {
                        WriteLine($"\nNome: {id.Name}\t Data: {id.Birthdate.ToString("dd/MM/yyyy")}");
                        CountTime(id.Birthdate);
                    }
                }
            }

            static void Shift()                                                 //(3)
            {
                Clear();

                WriteLine("Selecione uma opção para alterar:\n");
                WriteLine("1 - Alterar Nome;");
                WriteLine("2 - Alterar Data;");
                WriteLine("ESC para voltar para o Menu do Sistema.");
                ConsoleKeyInfo keyOption = ReadKey();

                switch (keyOption.Key)
                {
                    case ConsoleKey.D1: Clear(); ShiftName(); break;          //{1}
                    case ConsoleKey.D2: Clear(); ShiftDate(); break;          //{2}
                    case ConsoleKey.Escape: Clear(); MainMenu(); break;
                }
            }

            static void ShiftName()                                             //{1}
            {
                Clear();

                Write("Entre com o nome completo para confirmar: ");
                string fullname = ReadLine();

                var aniversariante = Archive.FindRecordName(fullname);

                if (aniversariante == null)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para continuar ou ESC para voltar ao Menu.");

                    ConsoleKeyInfo keyError = ReadKey();
                    switch (keyError.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Shift(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }


                WriteLine($"\nDeseja realmente alterar esse cadastro?\n\nNome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                WriteLine("\nPressione ENTER para CONFIRMAR ou ESC para CANCELAR.");
                ConsoleKeyInfo key = ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Clear(); MainMenu();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Archive.DeleteRecord(aniversariante);
                    Write("\nEntre com o novo nome: ");
                    string newname = ReadLine();
                    aniversariante.Name = newname;
                    Archive.Save(aniversariante);

                    WriteLine("\nCADASTRO ALTERADO COM SUCESSO!"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para fazer nova pesquisa ou ESC para voltar ao Menu Principal.");

                    ConsoleKeyInfo keyExit = ReadKey();
                    switch (keyExit.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Shift(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }
            }

            static void ShiftDate()                                             //{2}
            {
                Clear();

                Write("Entre com o nome completo para confirmar: ");
                string fullname = ReadLine();

                var aniversariante = Archive.FindRecordName(fullname);

                if (aniversariante == null)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para continuar ou ESC para voltar ao Menu.");

                    ConsoleKeyInfo keyError = ReadKey();
                    switch (keyError.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Shift(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }

                WriteLine($"\nDeseja realmente alterar esse cadastro?\n\nNome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                WriteLine("\nPressione ENTER para CONFIRMAR ou ESC para CANCELAR.");
                ConsoleKeyInfo keyDate = ReadKey();
                if (keyDate.Key == ConsoleKey.Escape)
                {
                    Clear(); MainMenu();
                }
                else if (keyDate.Key == ConsoleKey.Enter)
                {
                    Archive.DeleteRecord(aniversariante);
                    Write("\nEntre com a nova data no formato YYYY/MM/DD: ");
                    var newdate = DateTime.Parse(ReadLine());
                    aniversariante.Birthdate = newdate;
                    Archive.Save(aniversariante);

                    WriteLine("\nCADASTRO ALTERADO COM SUCESSO!"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para fazer nova pesquisa ou ESC para voltar ao Menu Principal.");

                    ConsoleKeyInfo keyExit = ReadKey();
                    switch (keyExit.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Shift(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }
            }


            static void Delete()                                                //(4)
            {
                Clear();

                Write("Entre com o nome completo: ");
                string fullname = ReadLine();

                var aniversariante = Archive.FindRecordName(fullname);

                if (aniversariante == null)
                {
                    WriteLine("\nNENHUM CADASTRO ENCONTRADO!"); Thread.Sleep(1500);

                    WriteLine("\nPressione ENTER para continuar ou ESC para voltar ao Menu.");
                    ConsoleKeyInfo keyDel = ReadKey();
                    switch (keyDel.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Delete(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }

                WriteLine($"\nDeseja realmente alterar esse cadastro?\n\nNome: {aniversariante.Name}\t Data: {aniversariante.Birthdate.ToString("dd/MM/yyyy")}");
                WriteLine("\nPressione ENTER para CONFIRMAR ou ESC para CANCELAR.");
                ConsoleKeyInfo key = ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Clear(); MainMenu();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Archive.DeleteRecord(aniversariante);

                    Write("\nCADASTRO REMOVIDO COM SUCESSO!\n"); Thread.Sleep(1500);
                    WriteLine("\nPressione ENTER para deletar novo cadastro ou ESC para voltar ao Menu Principal.");

                    ConsoleKeyInfo keyExit = ReadKey();
                    switch (keyExit.Key)
                    {
                        case ConsoleKey.Enter: Clear(); Delete(); break;
                        case ConsoleKey.Escape: Clear(); MainMenu(); break;
                    }
                }
            }
        }
    }
}
