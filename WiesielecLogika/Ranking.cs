using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace WiesielecLogika
{
    class Ranking
    {
        private Dictionary<string, int> gracze=new Dictionary<string, int>();

        public int GetPoints(string player_name)
        {
            if (gracze.ContainsKey(player_name))
                return gracze[player_name];
            gracze.Add(player_name, 0);
            return 0;
        }
        public void SetPoints(string player_name, int points)
        {
            if (gracze.ContainsKey(player_name))
            {
                gracze[player_name] = points;
            }
        }

        public Ranking()
        {
            try
            {
                using (StreamReader sr = new StreamReader("ranking.txt"))
                {
                    string line;
                    string[] split;
                    while ((line = sr.ReadLine()) != null)
                    {
                        split = line.Split(' ');
                        gracze.Add(split[0], System.Convert.ToInt32(split[1]));
                    }
                    
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

        }

        public void ZapiszStan()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter("ranking.txt"))
                {
                    foreach(var value in gracze)
                    {
                        sr.WriteLine(value.Key + " " + value.Value);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }
        public IOrderedEnumerable<KeyValuePair<string,int>> GetSortedRanking()
        {
            var newDict = from pair in gracze
                          orderby pair.Value descending
                          select pair;
            return newDict;
        }
    }
}
