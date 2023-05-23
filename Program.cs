using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace rgb
{
    class Pixel
    {
        public int r, g, b;
        public Pixel(string r, string g, string b)
        {
            this.r = int.Parse(r);
            this.g = int.Parse(g);
            this.b = int.Parse(b);
        }
        public string formated => $"RGB({this.r},{this.g},{this.b})";
        public int sum => this.r + this.g + this.b;
    }
    class Program
    {
        static bool hatar(int sorszam, int elteres)
        {
            for (int i = 0; i < pixels[sorszam-1].Count; i+=2)
            {
                int sub = Math.Abs(pixels[sorszam-1][i].b - pixels[sorszam-1][i + 1].b);
                if (sub > elteres) return true;                
            }
            return false;
        }

        static List<List<Pixel>> pixels = new List<List<Pixel>>();

        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("kep.txt");  
            
            for (int i = 0; i < file.Length; i++)
            {
                string[] sor = file[i].Split();
                List<Pixel> sorLista = new List<Pixel>();
                for (int j = 0; j < sor.Length; j+=3)
                {
                    sorLista.Add(new Pixel(sor[j], sor[j+1], sor[j+2]));                    
                }
                pixels.Add(sorLista);
            }

            // 2. feladat
            Console.WriteLine("2. feladat\nKérem egy képpont adatait! ");
            Console.Write("Sor: "); int inRow = int.Parse(Console.ReadLine());
            Console.Write("Oszlop: "); int inColumn = int.Parse(Console.ReadLine());

            Console.WriteLine($"A képpont színe {pixels[inRow - 1][inColumn - 1].formated}");

            // 3. feladat
            int bigger = pixels.SelectMany(row => row).Count(x => x.sum > 600);
            Console.WriteLine($"3. feladat:\nA világos képpontok száma: {bigger}");

            // 4. feladat
            int darkest = pixels.SelectMany(row => row).Min(x => x.sum);
            Console.WriteLine($"4. feladat:\nA legsötétebb pont RGB összege: {darkest}");
            Console.WriteLine("A legsötétebb pixelek színe: ");

            var matches = pixels.SelectMany(row => row).Where(x => darkest == x.sum).ToList();
            matches.ForEach(x => Console.WriteLine(x.formated));

            // 6. feladat
            int top = 1, bottom = 360;
            while (!hatar(top, 10)) top++;
            while (!hatar(bottom, 10)) bottom--;

            Console.WriteLine($"6. feladat\nA felhő legfelső sora: {top}");            
            Console.WriteLine($"A felhő legalsó sora: {bottom}");

            Console.ReadKey();
        }
    }
}
