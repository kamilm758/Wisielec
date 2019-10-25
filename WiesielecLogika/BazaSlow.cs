using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WiesielecLogika
{
   class BazaSlow
    {
        //kolekcja słów do zgadnięcia w łatwym poziomie trudności
        private List<Slowo> easyWords = new List<Slowo>();
        //kolekcja słów do zgadnięcia w trudnym poziomie trudności
        private List<Slowo> hardWords = new List<Slowo>();
        private static Random rnd= new Random();

        //losowanie i zwrócenie słowa z kategorii "łatwe"
        public Slowo GetLatweSlowo()
        {
            int losowanie = rnd.Next(0, easyWords.Count);
            return easyWords[losowanie];
        }
        //losowanie i zwrócenie słowa z kategorii "trudne"
        public Slowo GetTrudneSlowo()
        {
            int losowanie = rnd.Next(0, hardWords.Count);
            return hardWords[losowanie];
        }

        //konstruktor- wczytywanie słów z pliku. trzeba za każdym razem zmieniać ścieżki.
        // da się coś z tym zrobić?
        public BazaSlow()
        {
            //wczytywanie z pliku łatwych słów
            try
            {
                using(StreamReader sr=new StreamReader(@"C:\Users\kamil\Desktop\KCK-projekt\latwe.txt"))
                {
                    string line;
                    string[] split;
                    Slowo pom;
                    while ((line = sr.ReadLine()) != null)
                    {
                        split = line.Split(' ');
                        pom = new Slowo(split[0], split[1]);
                        easyWords.Add(pom);
                        
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            //wczytywanie z pliku trudnych słów
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\kamil\Desktop\KCK-projekt\trudne.txt"))
                {
                    string line;
                    string[] split;
                    Slowo pom;
                    while ((line = sr.ReadLine()) != null)
                    {
                        split = line.Split(' ');
                        pom = new Slowo(split[0], split[1]);
                        hardWords.Add(pom);

                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public List<Slowo> GetLatwe()
        {
            return easyWords;
        }

    }
}
