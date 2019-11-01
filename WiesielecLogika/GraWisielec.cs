using System;
using System.Collections.Generic;
using System.Text;

namespace WiesielecLogika
{
    class GraWisielec
    {
        private Slowo slowo; //słowo wylosowane do danej gry
        private int difficultyLevel; //1-easy 2-hard
        private string playerName; //imie gracza
        private int points; //ilosc punktów zdobytych w grze
        private int lifes; //ilosc żyć
        private int odgadnieteLitery; // ilość odgadniętych liter w haśle
        private BazaSlow BazaSlow = new BazaSlow(); //baza słów do losowania
        private List<char> wpisaneLitery; // lista liter które w danej grze były już odgadnięte
        //tworzenie nowej gry- inicjalizacja pól/ wylosowanie hasła zgodnie z wybranym poziomem
        //trudności
        private Ranking ranking = new Ranking();
        public void NowaGra(int difficultyLevelPar, string playerNamePar)
        {
            this.difficultyLevel = difficultyLevelPar;
            this.playerName = playerNamePar;
            this.points = ranking.GetPoints(playerNamePar);
            this.odgadnieteLitery = 0;
            this.wpisaneLitery = new List<char>();
            if (difficultyLevel == 1)
            {
                this.lifes = 5;
                slowo = BazaSlow.GetLatweSlowo();
            }
            else
            {
                this.lifes = 3;
                slowo = BazaSlow.GetTrudneSlowo();
            }
        }
        // zwraca 1, jeśli nie ma litery w hasle,2 jesli gracz juz odkryl te literę, 
        //ale znów ją wpisał, 3 jeśli litera występuje i gracz wpisuje pierwszy raz
        public int SprawdzCzyJest(char litera) 
        {
            if (slowo.GetSlowo().IndexOf(litera) == -1)
            {
                lifes--;
                return 1;
            }
            if ((wpisaneLitery.IndexOf(litera) != -1))
            {
                return 2;
            }
            wpisaneLitery.Add(litera);
            return 3;
        }
        //zwraca indeksy odgadniętych liter(aby w miejsce znaków zapytania pojawiły się 
        //odgadnięte litery)
        public List<int> ZwrocIndexOdgadnietej(char litera)
        {
            List<int> pozycje = new List<int>();
            string slowo = this.slowo.GetSlowo();
            for(int i = 0; i < slowo.Length; i++)
            {
                if (slowo[i] == litera)
                {
                    pozycje.Add(i);
                    points++;
                    odgadnieteLitery++;
                }
            }
            return pozycje;
        }

        public int KoniecGry()
        {
            if (lifes == 0)
            {
                points = ranking.GetPoints(playerName);
                points -= slowo.GetSlowo().Length;
                ranking.SetPoints(playerName, points);
                ranking.ZapiszStan();
                return 1; // przegrana
            }
            else if (odgadnieteLitery == slowo.GetSlowo().Length)
            {
                ranking.SetPoints(playerName, points);
                ranking.ZapiszStan();
                return 2; //wygrana
            }
            return 3; // gra się nie skończyła
        }

        //gettery
        public int GetLifes()
        {
            return lifes;
        }
        public int GetPoints()
        {
            return points;
        }
        public int GetOdgadnieteLitery()
        {
            return odgadnieteLitery;
        }
        public Slowo GetSlowo()
        {
            return slowo;
        }

    }
}
