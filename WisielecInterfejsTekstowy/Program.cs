using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WiesielecLogika;


namespace WisielecInterfejsTekstowy
{

    class Program
    {
        static string[] pozycjeMenu =
            {
                "Nowa gra", "Zmien poziom trudnosci", "Ranking" ,"Wyjscie"
            };
        static string[] poziomyTrudnosciString =
        {
            ": Trudny", ": Łatwy"
        };
        static string nazwaGracza = "GalAnonim";
        static int aktywnaPozycja = 0;
        static int poziomTrudnosci = 1;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StartMenu();
        }

        static void UruchomOpcje()
        {
            switch (aktywnaPozycja)
            {
                case 0:
                    Console.Clear();
                    UstawImie();
                    Console.Clear();
                    GraWisielec graWisielec = new GraWisielec(); //inicjalizacja gry
                    graWisielec.NowaGra(poziomTrudnosci,nazwaGracza);
                    int rozmiarSlowa = graWisielec.GetSlowo().GetSlowo().Length;//wielkość wylosowanego słowa
                    char[] haslo = new char[rozmiarSlowa];
                    char wpisanaLitera;
                    int czyZawiera;
                    //tabelka boczna
                    int szerTabelkiBocznej = ("O życie walczy: " + nazwaGracza).Length;
                    string[] iloscSpacji = new string[szerTabelkiBocznej];
                    iloscSpacji[0] = " ";
                    for(int i = 1; i < szerTabelkiBocznej; i++)
                    {
                        iloscSpacji[i] = iloscSpacji[i - 1] + " ";
                    }
                    List<int> pozycjeLiter = new List<int>();
                    for (int i = 0; i < rozmiarSlowa; i++)
                    {
                        haslo[i] = '?';
                    }
                    while (true)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(90,2);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lifes: " + graWisielec.GetLifes()+iloscSpacji[szerTabelkiBocznej-("Lifes: " + graWisielec.GetLifes()).Length-1]);
                        Console.SetCursorPosition(90, 3);
                        Console.Write("Points: " + graWisielec.GetPoints()+iloscSpacji[szerTabelkiBocznej- ("Points: " + graWisielec.GetPoints()).Length-1]);
                        Console.SetCursorPosition(90, 4);
                        Console.Write("O życie walczy: " + nazwaGracza);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.SetCursorPosition(4, 2);
                        Console.Write("Przed Tobą hasło na " + rozmiarSlowa + " liter z kategorii " + graWisielec.GetSlowo().GetKategoria()+ ".");
                        Console.SetCursorPosition(40, 6);
                        Console.Write("Hasło to: ");
                        Console.Write(haslo);
                        Console.WriteLine();
                        RysujWisielca(poziomTrudnosci, graWisielec.GetLifes());
                        
                        Console.SetCursorPosition(4, 14);
                        Console.WriteLine("Podaj litere: ");

                        try
                        {
                            Console.SetCursorPosition(4, 15);
                            wpisanaLitera = Convert.ToChar(Console.ReadLine());
                            Console.SetCursorPosition(4, 16);
                            Console.WriteLine("Wpisales litere: " + wpisanaLitera);
                            czyZawiera = graWisielec.SprawdzCzyJest(wpisanaLitera);
                            if (czyZawiera == 1)
                            {
                                Console.SetCursorPosition(4, 18);
                                Console.WriteLine("Niestety podana przez Ciebie litera" +
                                    " nie znajduje sie w hasle. Tracisz jedno zycie");

                            }
                            else if(czyZawiera==3)
                            {
                                pozycjeLiter = graWisielec.ZwrocIndexOdgadnietej(wpisanaLitera);
                                Console.SetCursorPosition(4, 18);
                                Console.WriteLine("Gratulacje! Podana litera znajduje sie w hasle!");
                                foreach(var value in pozycjeLiter)
                                {
                                    haslo[value] = wpisanaLitera;
                                }

                            }
                            else if(czyZawiera==2)
                            {
                                Console.SetCursorPosition(4, 18);
                                Console.WriteLine("Juz odgadles/as te litere! Wpisz inna!");
                            }

                            if (graWisielec.KoniecGry()==1)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(35, 4);
                                Console.WriteLine("Niestety przegrales/as :(");
                                Console.SetCursorPosition(35, 5);
                                Console.WriteLine("Haslem bylo: " + graWisielec.GetSlowo().GetSlowo());
                                Console.SetCursorPosition(35, 7);
                                Console.WriteLine("<<<NACISNIJ ENETER ABY POWROCIC DO MENU GLOWNEGO>>>");
                                RysujWisielca(poziomTrudnosci, graWisielec.GetLifes());
                                Console.ReadKey();
                                break;
                            }
                            if (graWisielec.KoniecGry()==2)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(35, 4);
                                Console.WriteLine("Gratulacje! Odgadles/as haslo :)");
                                Console.SetCursorPosition(35, 6);
                                Console.WriteLine("<<<NACISNIJ ENTER ABY POWROCIC DO MENU GLOWNEGO>>>");
                                Console.ReadKey();
                                break;
                            }
                        }
                        catch (Exception) 
                        {
                            Console.SetCursorPosition(4, 18);
                            Console.WriteLine("Wpisany znak to nie litera! Sprobuj ponownie!");
                            Console.SetCursorPosition(4, 20);
                            Console.WriteLine("Wciśnij <<ENTER>> aby kontynuować!");
                        }
                        Console.ReadKey();
                    }


                    break;
                case 1:
                    ZmianaPoziomuTrudnosci();
                    break;
                case 2:
                    RysujRanking();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }


        //uruchomienie metod generujących menu
        static void StartMenu()
        {
            Console.Title = "Gra Wisielec";
            Console.CursorVisible = false;
            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje();
            }
        }
        //ustawianie kolorów menu
        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"                          ____    __    ____  __       _______. __   _______  __       _______   ______ ");
            Console.WriteLine(@"                          \   \  /  \  /   / |  |     /       ||  | |   ____||  |     |   ____| /      |");
            Console.WriteLine(@"                           \   \/    \/   /  |  |    |   (----`|  | |  |__   |  |     |  |__   |  ,----'");
            Console.WriteLine(@"                            \            /   |  |     \   \    |  | |   __|  |  |     |   __|  |  |     ");
            Console.WriteLine(@"                             \    /\    /    |  | .----)   |   |  | |  |____ |  `----.|  |____ |  `----.");
            Console.WriteLine(@"                              \__/  \__/     |__| |_______/    |__| |_______||_______||_______| \______|");
            int x = 10;
            Console.SetCursorPosition(50, x);

            for (int i = 0; i < pozycjeMenu.Length; i++)
            {
                if (i == aktywnaPozycja)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (i == 0) Console.WriteLine("{0,-35}", pozycjeMenu[i] +" "+ poziomyTrudnosciString[poziomTrudnosci]);
                    else Console.WriteLine("{0, -35}", pozycjeMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    if (i == 0) Console.WriteLine(pozycjeMenu[i] + " " + poziomyTrudnosciString[poziomTrudnosci]);
                    else Console.WriteLine(pozycjeMenu[i]);
                }
                x++;
                Console.SetCursorPosition(50, x);
            }
        }
        //przechodzenie między opcjami
        static void WybieranieOpcji()
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow)
                {
                    aktywnaPozycja = (aktywnaPozycja > 0) ? aktywnaPozycja - 1 : pozycjeMenu.Length - 1;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.DownArrow)
                {
                    aktywnaPozycja = (aktywnaPozycja + 1) % pozycjeMenu.Length;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.Escape)
                {
                    aktywnaPozycja = pozycjeMenu.Length - 1;
                    break;
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }

        private static void RysujRanking()
        {
            int i = 1;// indeksowanie
            int x = 10;//przechodzenie do kolejnego wiersza w wypisywaniu graczy w rankingu
            Console.Clear();
            Console.SetCursorPosition(50, 2);
            Console.Write(RANKING);
            Console.BackgroundColor = ConsoleColor.Gray;
            Ranking ranking= new Ranking();
            var sortedRanking = ranking.GetSortedRanking();
            foreach (KeyValuePair<string, int> pair in sortedRanking)
            {
                Console.SetCursorPosition(55, x);
                Console.WriteLine(i+": {0}: {1}", pair.Key, pair.Value);
                i++;
                x++;
            }
            x++;
            Console.SetCursorPosition(35, x);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Wciśnij DOWOLNY PRZYCISK aby powrócić do menu głównego");
            Console.ReadKey();
        }

        private static void ZmianaPoziomuTrudnosci()
        {
            if(poziomTrudnosci == 0)
            {
                poziomTrudnosci = 1;
            }
            else
            {
                poziomTrudnosci = 0;
            }
        }

        private static void UstawImie()
        {
            do
            {
                Console.Clear();
                Console.SetCursorPosition(50, 13);
                Console.WriteLine("Przedstaw się skazańcze: ");
                Console.SetCursorPosition(50, 25);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(" Nie więcej niż 10 liter! ");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(50, 14);
                nazwaGracza = Console.ReadLine();
                if (nazwaGracza.Length > 10)
                {
                    Console.SetCursorPosition(35, 13);
                    Console.WriteLine("Nazwa za długa! Spróbuj jeszcze raz!<<PRESS ANY KEY>>");
                    Console.ReadKey();
                }
                else if (nazwaGracza.Length == 0)
                {
                    Console.SetCursorPosition(30, 13);
                    Console.WriteLine("Nazwa musi zawierać przynajmniej jeden znak!<<PRESS ANY KEY>>");
                    Console.ReadKey();
                }
            } while (nazwaGracza.Length > 10 || nazwaGracza.Length==0);
        }

        private static void RysujWisielca(int difficulty, int lifes)
        {
            if (difficulty == 1)
                RysujWisielcaEasy(lifes);
            else
            {
                RysujWisielcaHard(lifes);
            }
        }
        private static void RysujWisielcaEasy(int lifes)
        {
            //Pierwsze życie
            if (lifes <= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 4);
                Console.WriteLine("      ########");
                Console.WriteLine("       #    | ");
                Console.WriteLine("       #     ");
                Console.WriteLine("       #     ");
                Console.WriteLine("       #   ");
                Console.WriteLine("       #   ");
                Console.WriteLine("       #");
                Console.WriteLine("    ###########");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }

            if (lifes <= 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 6);
                Console.WriteLine("   #    O ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;

            }

            if (lifes <= 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 7);
                Console.WriteLine("   #    | ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            if (lifes <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 7);
                Console.WriteLine("   #   /| ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            if (lifes <= 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 7);
                Console.WriteLine("   #   /|\\ ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            if (lifes <= 1)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 8);
                Console.WriteLine("   #   /   ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            if (lifes <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 6);
                Console.WriteLine("   #    X ");
                Console.SetCursorPosition(4, 8);
                Console.WriteLine("   #   / \\ ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
        }
        private static void RysujWisielcaHard(int lifes)
        {
            //Pierwsze życie
            if (lifes <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 4);
                Console.WriteLine("      ########");
                Console.WriteLine("       #    | ");
                Console.WriteLine("       #     ");
                Console.WriteLine("       #     ");
                Console.WriteLine("       #   ");
                Console.WriteLine("       #   ");
                Console.WriteLine("       #");
                Console.WriteLine("    ###########");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }

            if (lifes <= 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 6);
                Console.WriteLine("   #    O ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;

            }

            if (lifes <= 1) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 7);
                Console.WriteLine("   #   /|\\ ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }

            if(lifes <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(4, 6);
                Console.WriteLine("   #    X ");
                Console.SetCursorPosition(4, 8);
                Console.WriteLine("   #   / \\ ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
        }
        static readonly string RANKING = @" 
                                    _____            _   _ _  _______ _   _  _____
                                   |  __ \     /\   | \ | | |/ /_   _| \ | |/ ____|
                                   | |__) |   /  \  |  \| | ' /  | | |  \| | |  __ 
                                   |  _  /   / /\ \ | . ` |  <   | | | . ` | | |_ |
                                   | | \ \  / ____ \| |\  | . \ _| |_| |\  | |__| |
                                   |_|  \_\/_/    \_\_| \_|_|\_\_____|_| \_|\_____|
                                                                                   ";
    }
}
