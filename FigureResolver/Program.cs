using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            AlgorithmResolver algorithmResolver = new AlgorithmResolver();
            algorithmResolver.FindAllSolutions();
            Console.WriteLine(algorithmResolver.GetAllSolutions().Count());

            Console.ReadLine();
        }
    }
}
