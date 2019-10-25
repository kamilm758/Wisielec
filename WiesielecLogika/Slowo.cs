using System;
using System.Collections.Generic;
using System.Text;

namespace WiesielecLogika
{
    class Slowo
    {
        private string slowo; //pole słowa(wczytywane pierwsze słowo każdej linijki z pliku)
        private string kategoria;// kategoria słowa
        //kostruktor
        public Slowo(string slowoPar,string kategoriaPar)
        {
            this.slowo = slowoPar;
            this.kategoria = kategoriaPar;
        }
        //gettery i settery
        public void SetSlowo(string slowoPar)
        {
            this.slowo = slowoPar;
        }
        public void SetKategoria(string kategoriaPar)
        {
            this.kategoria = kategoriaPar;
        }
        public string GetSlowo()
        {
            return this.slowo;
        }
        public string GetKategoria()
        {
            return this.kategoria;
        }
    }
}
