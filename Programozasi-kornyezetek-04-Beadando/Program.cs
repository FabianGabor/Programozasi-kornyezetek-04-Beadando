using System;
using System.Collections;
using System.Linq;

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
            catch (ArgumentException) {
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
                catch (Exception) {
                    Console.WriteLine("Számot!");
                }
            } while (kartya.Ertek < 1 || kartya.Ertek > 11);
            
            Console.WriteLine("Neve: " + kartya.KartyaNeve());
            
            Console.Write("Kartya neve: ");
            kartya.Nev = Console.ReadLine();
            Console.WriteLine(kartya.Nev + " erteke: " + kartya.KartyaErteke());
        }
    }
    
    // 2. Egy cégnél 5 dolgozó megadja felsorolás segítségével, hogy a hét melyik napján ér rá egy megbeszélésre.
    // a) Összeségében mely napokat jelölték be a dolgozók?
    // b) Jelölt-e be valaki a pénteket?
    // c) Mely napo(ka)t jelölték be a legtöbben?

    class Ceg {
        private enum Napok {
            Hetfo,Kedd,Szerda,Csutortok,Pentek
        }
        //private int[] megbeszelesNapok = new int[5];
        private static int[] megbeszelesNapok = new int[5];

        private class Dolgozo {
            string napok;
            string[] nap;

            public string Napok {
                get => napok;
                set => napok = value;
            }

            public string[] Nap {
                get => nap;
                set => nap = value;
            }
        }

        private static string LegnepszerubbNapok() {
            int count = 0;
            string napok = null;

            int max = megbeszelesNapok.Prepend(0).Max();

            for (var index = 0; index < megbeszelesNapok.Length; index++) {
                if (megbeszelesNapok[index] == max) {
                    napok += (count > 0) ? ", " : "";
                    napok += (Napok) index;
                    count++;
                }
            }

            return napok;
        }

        public static void RunCeg() {
            ArrayList dolgozok = new ArrayList();

            string[] dolgozoValasztottNapok = new string[5];
            dolgozoValasztottNapok[0] += (Napok) 0 + ",";
            dolgozoValasztottNapok[0] += (Napok) 3;
            
            dolgozoValasztottNapok[1] += (Napok) 0 + ",";
            dolgozoValasztottNapok[1] += (Napok) 3;
            
            dolgozoValasztottNapok[2] += (Napok) 0;
            
            dolgozoValasztottNapok[3] += (Napok) 0 + ",";
            dolgozoValasztottNapok[3] += (Napok) 3 + ",";
            dolgozoValasztottNapok[3] += (Napok) 4;
            
            dolgozoValasztottNapok[4] += (Napok) 3 + ",";
            dolgozoValasztottNapok[4] += (Napok) 4;

            for (int i = 0; i < 5; i++) {
                var dolgozo = new Dolgozo();

                dolgozo.Napok += dolgozoValasztottNapok[i];
                dolgozok.Add(dolgozo);
            }

            for (var index = 0; index < dolgozok.Count; index++) {
                var dolgozo = (Dolgozo) dolgozok[index];
                dolgozo.Nap = dolgozo.Napok.Split(',');
                
                Console.Write("Dolgozo " + index + ": ");
                foreach (var nap in dolgozo.Nap) {
                    Console.Write(nap + " ");
                    int i = (int) Enum.Parse(typeof(Napok), nap);

                    megbeszelesNapok[i]++;
                }
                Console.WriteLine();
            }

            // a) Összeségében mely napokat jelölték be a dolgozók?
            Console.WriteLine("Megbeszelesre alkalmas napok:");
            for (var index = 0; index < megbeszelesNapok.Length; index++) {
                if (megbeszelesNapok[index] > 0)
                    Console.Write((Napok) index + " ");
            }
            Console.WriteLine();

            // b) Jelölt-e be valaki a pénteket?
            Console.WriteLine("Valasztott-e valaki penteket?");
            Console.WriteLine(((int) Enum.Parse(typeof(Napok), "Pentek") != 0)?"Igen":"Nem");
            
            // c) Mely napo(ka)t jelölték be a legtöbben?
            Console.WriteLine("Legnepszerubb napok:");

            Console.WriteLine(LegnepszerubbNapok());
        }
    }
    
    // 3. Készíts struktúrát kerékpár adatainak a tárolására: vázméret (12,14,16,20 felsoroltak valamelyike),
    // márka, ár! Készíts konstruktort, az árat tulajdonsággal határozd meg! Készíts ToString-et a jellemzők
    // leírására!
    class Kerekpar {
        internal enum Vazmeretek { _12 = 12, _14 = 14, _16 = 16, _18 = 18, _20 = 20 };
        internal enum Markak { Cannondale, Trek, Giant, Specialized, Merida, Scott};

        private Vazmeretek Vazmeret { get; }

        private Markak Marka { get; }

        private int Ar { get; }

        private static int SetAr(Markak marka) {
            switch (marka) {
                case Markak.Cannondale: { return 300000; }
                case Markak.Trek: { return 250000; }
                case Markak.Giant: { return 220000; }
                case Markak.Specialized: { return 270000; }
                case Markak.Merida: { return 260000; }
                case Markak.Scott: { return 280000; }
                default:
                    throw new ArgumentOutOfRangeException(nameof(marka), marka, "Nem valaszthato marka!");
            }
        }
        public Kerekpar(Vazmeretek vazmeret, string marka) {
            Vazmeret = vazmeret;
            if(Enum.TryParse<Markak>(marka, out var result))
                Marka = result;
            else
                throw new ArgumentOutOfRangeException(nameof(marka), marka, "Nem valaszthato marka!");

            Ar = SetAr(Marka);
        }

        public Kerekpar(Vazmeretek vazmeret, Markak marka) {
            Vazmeret = vazmeret;
            Marka = marka;
            Ar = SetAr(Marka);
        }

        public override string ToString() {
            return "Marka:    " + Marka + "\n" +
                   "Vazmeret: " + (int) Vazmeret + "\n" +
                   "Ar:       " + Ar + "\n";
        }
    }

    public readonly struct Kerekpar2 {
        public enum Vazmeretek { _12 = 12, _14 = 14, _16 = 16, _18 = 18, _20 = 20 };

        public enum Markak { Cannondale, Trek, Giant, Specialized, Merida, Scott};

        private Vazmeretek Vazmeret { get; }

        private Markak Marka { get; }

        private int Ar { get; }
        
        private static int SetAr(Markak marka) {
            switch (marka) {
                case Markak.Cannondale: { return 300000; }
                case Markak.Trek: { return 250000; }
                case Markak.Giant: { return 220000; }
                case Markak.Specialized: { return 270000; }
                case Markak.Merida: { return 260000; }
                case Markak.Scott: { return 280000; }
                default:
                    throw new ArgumentOutOfRangeException(nameof(marka), marka, "Nem valaszthato marka!");
            }
        }

        public Kerekpar2(Vazmeretek vazmeret, Markak marka) : this() {
            Vazmeret = vazmeret;
            Marka = marka;
            Ar = SetAr(Marka);
        }

        public override string ToString() {
            return "Marka:    " + Marka + "\n" +
                   "Vazmeret: " + (int) Vazmeret + "\n" +
                   "Ar:       " + Ar + "\n";
        }
    }

    internal class Program {
        public static void Main(string[] args) {
            //Kartya.RunKartya();
            //Ceg.RunCeg();
            Kerekpar kerekpar0 = new Kerekpar(Kerekpar.Vazmeretek._18, Kerekpar.Markak.Merida);
            Kerekpar kerekpar1 = new Kerekpar(Kerekpar.Vazmeretek._16, "Specialized");
            Kerekpar2 kerekpar2 = new Kerekpar2(Kerekpar2.Vazmeretek._14, Kerekpar2.Markak.Cannondale);
            
            Console.WriteLine(kerekpar0.ToString());
            Console.WriteLine(kerekpar1.ToString());
            Console.WriteLine(kerekpar2.ToString());
        }
    }
}