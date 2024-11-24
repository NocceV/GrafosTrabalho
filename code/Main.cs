using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Dictionary<int, int>> grafo = new Dictionary<int, Dictionary<int,int>>();
            Console.WriteLine("Insira o vertice / -1 para sair");
            int vertice = int.Parse(Console.ReadLine());
            do
            {
                if (!grafo.TryGetValue(vertice, out Dictionary<int, int> vertices))
                {
                    grafo.Add(vertice, new Dictionary<int, int>());
                }
                else
                {
                    Console.WriteLine("Vertice ja existe");
                }
                Console.WriteLine("Insira o vertice / -1 para sair");
                vertice = int.Parse(Console.ReadLine());
            } while(vertice != -1);


            Console.WriteLine("Insira qual vértice deseja inserir aresta / -1 para sair");
            vertice = int.Parse(Console.ReadLine());
            do
            {
                if (grafo.TryGetValue(vertice, out Dictionary<int, int> vertices))
                {
                    Console.WriteLine("Insira o vertice de destino e em seguida seu peso");
                    int destino = int.Parse(Console.ReadLine());
                    int peso = int.Parse(Console.ReadLine());
                    grafo[vertice].Add(destino, peso);
                }
                else
                {
                    Console.WriteLine("Vertice não existe");
                }
                Console.WriteLine("Insira o vertice / -1 para sair");
                vertice = int.Parse(Console.ReadLine());
            } while (vertice != -1);

            listaAdjacencia(grafo);
            Console.ReadLine();

        }
        public static void listaAdjacencia(Dictionary<int, Dictionary<int, int>> grafo)
        {
            foreach (var vertice in grafo)
            {
                Console.WriteLine("Vertice: " + vertice.Key);
                foreach (var aresta in vertice.Value)
                {
                    Console.WriteLine("Aresta: " + aresta.Key + " Peso: " + aresta.Value);
                }
            }
        }
    }
}
