using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosSanzio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grafo grafo = new Grafo();
            Console.WriteLine("Insira o vertice / -1 para sair");
            int vertice = int.Parse(Console.ReadLine());
            do
            {
                if (grafo.AdicionaVertice(vertice))
                {
                    Console.Clear();
                    Console.WriteLine("Adicionado com sucesso");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Vertice já existe");
                }
                Console.WriteLine("Insira o vertice / -1 para sair");
                vertice = int.Parse(Console.ReadLine());
            } while (vertice != -1);


            Console.WriteLine("Insira qual vértice deseja inserir aresta / -1 para sair");
            vertice = int.Parse(Console.ReadLine());
            Console.WriteLine("Insira o vertice de destino e em seguida seu peso");
            int destino = int.Parse(Console.ReadLine());
            int peso = int.Parse(Console.ReadLine());
            do
            {

                if (grafo.AdicionarAresta(vertice,destino,peso))
                {
                    Console.Clear();
                    Console.WriteLine("Adicionado com sucesso");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Vertice nao existe");
                }


                Console.WriteLine("Insira o vertice / -1 para sair");
                vertice = int.Parse(Console.ReadLine());
                if(vertice == -1)
                {
                    break; // Solucao pessima -> Consertar pfv
                }
                Console.WriteLine("Insira o vertice de destino");
                destino = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira o peso da Aresta");
                peso = int.Parse(Console.ReadLine());


            } while (vertice != -1);
            
            Console.Clear();
            listaAdjacência(grafo.GetAdj());
            Console.ReadLine();

        }
        public static void listaAdjacência(Dictionary<int, Dictionary<int, int>> grafo)
        {
            foreach (var vertice in grafo)
            {
                Console.WriteLine("Vertice: " + vertice.Key + " Adjacencias: ");
                foreach (var aresta in vertice.Value)
                {
                    Console.WriteLine("Aresta: " + aresta.Key + " Peso: " + aresta.Value);
                }
                Console.WriteLine("");
            }
        }
    }
}