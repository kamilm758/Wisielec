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
                "Nowa gra", "Ranking" ,"Wyjscie"
            };
        static int aktywnaPozycja = 0;
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
                    GraWisielec graWisielec = new GraWisielec(); //inicjalizacja gry
                    graWisielec.NowaGra(2,"Piotr");
                    int rozmiarSlowa = graWisielec.GetSlowo().GetSlowo().Length;//wielkość wylosowanego słowa
                    char[] haslo = new char[rozmiarSlowa];
                    char wpisanaLitera;
                    int czyZawiera;
                    List<int> pozycjeLiter = new List<int>();
                    for (int i = 0; i < rozmiarSlowa; i++)
                    {
                        haslo[i] = '?';
                    }
                    while (true)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(80,2);
                        Console.WriteLine("Lifes: " + graWisielec.GetLifes());
                        Console.SetCursorPosition(80, 4);
                        Console.WriteLine("Points: " + graWisielec.GetPoints());
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Oto hasło, posiada "+rozmiarSlowa+" liter. Haslo o kategorii "+graWisielec.GetSlowo().GetKategoria());
                        Console.WriteLine("Hasło to: ");
                        Console.Write(haslo);
                        Console.WriteLine();
                        Console.WriteLine("Podaj litere");
                        try
                        {
                            wpisanaLitera = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("Wpisales litere: " + wpisanaLitera);
                            czyZawiera = graWisielec.SprawdzCzyJest(wpisanaLitera);
                            if (czyZawiera == 1)
                            {
                                Console.WriteLine("Niestety podana przez Ciebie litera" +
                                    " nie znajduje sie w hasle. Tracisz jedno zycie");

                            }
                            else if(czyZawiera==3)
                            {
                                pozycjeLiter = graWisielec.ZwrocIndexOdgadnietej(wpisanaLitera);
                                Console.WriteLine("Gratulacje! Podana litera znajduje sie w hasle!");
                                foreach(var value in pozycjeLiter)
                                {
                                    haslo[value] = wpisanaLitera;
                                }

                            }
                            else if(czyZawiera==2)
                            {
                                Console.WriteLine("Juz odgadles/as te litere! Wpisz inna!");
                            }

                            if (graWisielec.KoniecGry()==1)
                            {
                                Console.Clear();
                                Console.WriteLine("Niestety przegrales/as :(");
                                Console.WriteLine("Haslem bylo: " + graWisielec.GetSlowo().GetSlowo());
                                Console.WriteLine("<<<NACISNIJ ENETER ABY POWROCIC DO MENU GLOWNEGO>>>");
                                Console.ReadKey();
                                break;
                            }
                            if (graWisielec.KoniecGry()==2)
                            {
                                Console.Clear();
                                Console.WriteLine("Gratulacje! Odgadles/as haslo :)");
                                Console.WriteLine("<<<NACISNIJ ENTER ABY POWROCIC DO MENU GLOWNEGO>>>");
                                Console.ReadKey();
                                break;
                            }
                        }
                        catch (Exception) 
                        {
                            Console.WriteLine("Wpisany znak to nie litera! Sprobuj ponownie!");
                        }
                        Console.ReadKey();
                    }


                    break;
                case 1:
                    RysujRanking();
                    break;
                case 2:
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
            Console.WriteLine(@"____    __    ____  __       _______. __   _______  __       _______   ______ ");
            Console.WriteLine(@"\   \  /  \  /   / |  |     /       ||  | |   ____||  |     |   ____| /      |");
            Console.WriteLine(@" \   \/    \/   /  |  |    |   (----`|  | |  |__   |  |     |  |__   |  ,----'");
            Console.WriteLine(@"  \            /   |  |     \   \    |  | |   __|  |  |     |   __|  |  |     ");
            Console.WriteLine(@"   \    /\    /    |  | .----)   |   |  | |  |____ |  `----.|  |____ |  `----.");
            Console.WriteLine(@"    \__/  \__/     |__| |_______/    |__| |_______||_______||_______| \______|");
            int x = 10;
            Console.SetCursorPosition(20, x);
            for (int i = 0; i < pozycjeMenu.Length; i++)
            {
                if (i == aktywnaPozycja)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0,-35}", pozycjeMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(pozycjeMenu[i]);
                }
                x++;
                Console.SetCursorPosition(20, x);
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
            Console.Clear();
            Ranking ranking= new Ranking();
            var sortedRanking = ranking.GetSortedRanking();
            foreach (KeyValuePair<string, int> pair in sortedRanking)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }
    }
}
