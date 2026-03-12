using System.Linq;

namespace rotyconsole
{
    internal class Program
    {
        public class Szuperhos
        {
            public string nev {  get; set; }
            public string kepesseg { get; set; }
            public int eroszint { get; set; }
            public string csapatneve { get; set; }
            public int kuldetesek_szama {  get; set; }

            public Szuperhos(string forras)
            {
                string[] forras_strings = forras.Split(';');
                this.nev = forras_strings[0];
                this.kepesseg = forras_strings[1];
                this.eroszint = Convert.ToInt32(forras_strings[2]);
                this.csapatneve = forras_strings[3];
                this.kuldetesek_szama = Convert.ToInt32(forras_strings[4]);

            }
        }
        static void Main(string[] args)
        {
            string[] hosok_text = File.ReadAllLines("hosok.txt");
            List<Szuperhos> hosok = new List<Szuperhos>();
            foreach (var item in hosok_text){
                hosok.Add(new Szuperhos(item));
            }


            Console.WriteLine("3. feladat");
            foreach (var item in hosok.Where(x => x.kepesseg == "Viltrumite"))
            {
                Console.WriteLine(item.nev);
            }

            Console.WriteLine();

            Console.WriteLine("4. feladat");
            foreach (var item in hosok.GroupBy(x=> x.csapatneve).OrderByDescending(x => x.Count()))
            {
                Console.WriteLine();
                Console.WriteLine(item.ToArray()[0].csapatneve);
                Console.Write(item.Count());
            }

            Console.WriteLine();

            Console.WriteLine("5. feladat");
            foreach (var item in hosok.Where(x=> x.eroszint>85))
            {
                Console.WriteLine($"NEVE: {item.nev} CSAPATA: {item.csapatneve} ERŐSZINTJE: {item.eroszint}");
            }

            Console.WriteLine();

            Console.WriteLine("6. feladat");

            Console.WriteLine(hosok.Where(x=> x.kepesseg == "Mutáns").Select(x=> x.eroszint).Average());

            Console.WriteLine();

            Console.WriteLine("7. feladat");

            Console.WriteLine(hosok.Where(x => x.kepesseg == "Mutáns").Select(x => x.kuldetesek_szama).OrderBy(x => x).ToArray()[0]);

            Console.WriteLine();

            Console.WriteLine("8. feladat");



            Console.WriteLine("ELSŐ HARCOS:");
            string elsoharcos = Console.ReadLine();
            Console.WriteLine("MÁSODIK HARCOS:");
            string masodharcos = Console.ReadLine();

            Console.WriteLine(Harcol(hosok,elsoharcos,masodharcos));

        }

        static string Harcol(List<Szuperhos> hosok, string elso, string masodik)
        {
            if (hosok.Where(x => x.nev == elso).Count() == 1 && hosok.Where(x => x.nev == masodik).Count() == 1)
            {
                if (hosok.Where(x => x.nev == elso).ToArray()[0].eroszint > hosok.Where(x => x.nev == masodik).ToArray()[0].eroszint)
                {
                    return $"A NYERTES: {elso}!";
                }
                else if(hosok.Where(x => x.nev == elso).ToArray()[0].eroszint < hosok.Where(x => x.nev == masodik).ToArray()[0].eroszint)
                {
                    return $"A NYERTES: {masodik}!";
                }
                else
                {
                    return $"DÖNTETLEN!";
                }
            }
            else
            {
                return "Galibába kerültünk";
            }
        }

    }
}
