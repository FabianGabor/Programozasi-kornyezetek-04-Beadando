using System;

namespace Programozasi_kornyezetek_04_Beadando {
    // 1. Felsorolás típus segítségével állapítsuk meg egy bekért pontszámról, hogy az melyik magyarkártya-lapnak felel
    // meg! Próbáld ki fordítva is! (alsó 2, felső 3, király 4, hetes 7, stb., ász 11)

    class Kartya {
        private int _ertek;
        private string _nev;
        enum Kartyak {
            Asz = 11,
            Kiraly = 4,
            Felso = 3,
            Also = 2
        }
        public int Ertek {
            get => _ertek;
            set => _ertek = value;
        }

        public string Nev {
            get => _nev;
            set => _nev = value;
        }

        private string KartyaNeve() {
            if (_ertek > 0 && _ertek < 12) {
                var nev = (Kartyak) _ertek;
                return nev.ToString();
            }
            return "Nem letezik ilyen kartya";
        }

        private int KartyaErteke() {
            try {
                return (int) Enum.Parse(typeof(Kartyak), _nev);
            }
            catch (ArgumentException e) {
                Console.WriteLine("Nincs ilyen kartya!");
                return 0;
            }
        }

        public static void RunKartya() {
            Kartya kartya = new Kartya();

            do {
                Console.Write("Kartya szama: ");
                try {
                    kartya.Ertek = int.Parse(Console.ReadLine());
                }
                catch (Exception e) {
                    Console.WriteLine("Számot!");
                }
            } while (kartya.Ertek < 1 || kartya.Ertek > 11);
            
            Console.WriteLine("Neve: " + kartya.KartyaNeve());
            
            Console.Write("Kartya neve: ");
            kartya.Nev = Console.ReadLine();
            Console.WriteLine(kartya.Nev + " erteke: " + kartya.KartyaErteke());
        }
    }
    
    internal class Program {
        public static void Main(string[] args) {
            Kartya.RunKartya();
        }
    }
}