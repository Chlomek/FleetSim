using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FleetSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Flotila flotila = new Flotila();
            while (true)
            {
                Console.WriteLine("1 - Pridat lod");
                Console.WriteLine("2 - Vydat misi");
                Console.WriteLine("3 - Vypis info");
                Console.WriteLine("4 - Doplnit posadku");
                Console.WriteLine("5 - Vylepšit dolet")

                int.TryParse(Console.ReadLine(), out int volba);

                if (volba == 1)
                {
                    flotila.PridatLod();
                }
                else if (volba == 2)
                {
                    Console.WriteLine("Zadej cil");
                    string cil = Console.ReadLine();
                    Console.WriteLine("Zadej vzdalenost");
                    double vzdalenost = double.Parse(Console.ReadLine());
                    flotila.VydejMisi(cil, vzdalenost);
                }
                else if (volba == 3)
                {
                    foreach (Lod lod in flotila.list)
                    {
                        lod.VypisInfo();
                    }
                }
                else if (volba == 4)
                {
                    Console.WriteLine("Zadej nazev lode");
                    string nazev = Console.ReadLine();
                    Console.WriteLine("Zadej pocet posadky");
                    int pocet = int.Parse(Console.ReadLine());
                    foreach (Lod lod in flotila.list)
                    {
                        if (lod.Nazev == nazev)
                        {
                            lod.NalozPosadku(pocet);
                        }
                    }
                }
                else if (volba == 5)
                {
                    Console.WriteLine("Zadej nazev lode");
                    string nazev = Console.ReadLine();
                    Console.WriteLine("Zadej pridani doletu");
                    double pridaniLY = double.Parse(Console.ReadLine());
                    foreach (Lod lod in flotila.list)
                    {
                        if (lod.Nazev == nazev)
                        {
                            lod.UpgradujDolet(pridaniLY);
                        }
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
    class Lod
    {
        public string Nazev;
        public int KapacitaPosadky;
        public int AktualniPocetPosadky;
        public double DoletY;

        public enum TypLodeEnum { Velitelska, Transportni, Bojova, Pruzkumna };
        public TypLodeEnum TypLode;

        public void NalozPosadku(int pocet)
        {
            if (AktualniPocetPosadky + pocet <= KapacitaPosadky)
            {
                AktualniPocetPosadky += pocet;
            }
            else
            {
                Console.WriteLine("Nelze nalozit tolik posadky");
            }
        }

        public void VypisInfo()
        {
            Console.WriteLine("Nazev: " + this.Nazev);
            Console.WriteLine("Kapacita posadky: " + this.KapacitaPosadky);
            Console.WriteLine("Aktualni pocet posadky: " + this.AktualniPocetPosadky);
            Console.WriteLine("Dolet: " + this.DoletY);
            Console.WriteLine("Typ lode: " + this.TypLode);
            Console.WriteLine();
        }

        public void ProvedMisi(string cil, double vzdalenost)
        {
            if (AktualniPocetPosadky > 0)
            {
                if (this.DoletY > vzdalenost)
                {
                    Console.WriteLine("Lod " + this.Nazev + " provedla misi na cil " + cil);
                }
                else
                {
                    Console.WriteLine("Lod " + this.Nazev + " nema dostatecny dolet");
                }
            }
        }

        public void UpgradujDolet(double pridaniLY)
        {
            DoletY = +pridaniLY;
        }
    }

    class Flotila
    {
        public List<Lod> list = new List<Lod>();

        public void PridatLod()
        {
            Lod lod = new Lod();
            Console.WriteLine("Zadej typ lode");
            Console.WriteLine("1 - Velitelska");
            Console.WriteLine("2 - Transportni");
            Console.WriteLine("3 - Bojova");
            Console.WriteLine("4 - Pruzkumna");
            Console.WriteLine("5 - Vlastni");

            int.TryParse(Console.ReadLine(), out int volba);
            if (volba == 1)
            {
                VelitelskaLodPreset(lod);
            }
            else if (volba == 2)
            {
                TransportniLodPreset(lod);
            }
            else if (volba == 3)
            {
                BojovaLodPreset(lod);
            }
            else if (volba == 4)
            {
                PruzkumnaLodPreset(lod);
            }
            else
            {
                VlastniLod(lod);
            }
        }

        public void VydejMisi(string cil, double vzdalenost)
        {
            foreach (Lod lod in list)
            {
                lod.ProvedMisi(cil, vzdalenost);
            }
        }

        private void VelitelskaLodPreset(Lod lod)
        {
            lod.Nazev = "Velitelska lod";
            lod.KapacitaPosadky = 15;
            lod.AktualniPocetPosadky = 10;
            lod.DoletY = 100;
            lod.TypLode = Lod.TypLodeEnum.Velitelska;
            list.Add(lod);
        }
        private void BojovaLodPreset(Lod lod)
        {
            lod.Nazev = "Bojova lod";
            lod.KapacitaPosadky = 5;
            lod.AktualniPocetPosadky = 3;
            lod.DoletY = 50;
            lod.TypLode = Lod.TypLodeEnum.Bojova;
            list.Add(lod);
        }
        private void TransportniLodPreset(Lod lod)
        {
            lod.Nazev = "Transportni lod";
            lod.KapacitaPosadky = 20;
            lod.AktualniPocetPosadky = 2;
            lod.DoletY = 100;
            lod.TypLode = Lod.TypLodeEnum.Transportni;
            list.Add(lod);
        }
        private void PruzkumnaLodPreset(Lod lod)
        {
            lod.Nazev = "Pruzkumna lod";
            lod.KapacitaPosadky = 5;
            lod.AktualniPocetPosadky = 2;
            lod.DoletY = 250;
            lod.TypLode = Lod.TypLodeEnum.Pruzkumna;
            list.Add(lod);
        }
        private void VlastniLod(Lod lod)
        {
            Console.WriteLine("Zadej nazev lode");
            lod.Nazev = Console.ReadLine();
            Console.WriteLine("Zadej Typ lode");
            Console.WriteLine("1 - Velitelska");
            Console.WriteLine("2 - Transportni");
            Console.WriteLine("3 - Bojova");
            Console.WriteLine("4 - Pruzkumna");

            int.TryParse(Console.ReadLine(), out int volbaTyp);
            if (volbaTyp == 1)
            {
                lod.TypLode = Lod.TypLodeEnum.Velitelska;
            }
            else if (volbaTyp == 2)
            {
                lod.TypLode = Lod.TypLodeEnum.Transportni;
            }
            else if (volbaTyp == 3)
            {
                lod.TypLode = Lod.TypLodeEnum.Bojova;
            }
            else if (volbaTyp == 4)
            {
                lod.TypLode = Lod.TypLodeEnum.Pruzkumna;
            }
            Console.WriteLine("Zadej kapacitu posadky");
            lod.KapacitaPosadky = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadej dolet lode");
            lod.DoletY = double.Parse(Console.ReadLine());
            list.Add(lod);
        }
    }
}