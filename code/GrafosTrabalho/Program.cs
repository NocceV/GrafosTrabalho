using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using GrafosTrabalho.model2;
using System.Net.NetworkInformation;

namespace GrafosTrabalho
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            start();
        }

        public static string menuInicial()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Crie um grafo");
            stringBuilder.AppendLine("1) Criar grafo");
            stringBuilder.AppendLine("0) Sair");
            return stringBuilder.ToString();
        }

        public static void start()
        {
            int codigo;
            do
            {
                try
                {
                    Console.WriteLine(menuInicial());
                    codigo = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (codigo)
                    {
                        case 1:
                            criarGrafo();
                            break;
                        case 0:
                            Console.WriteLine("Adeus!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                    codigo = -1;
                }
            } while (codigo != 0);
        }

      

        public static void criarGrafo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Console.WriteLine("Informe a quantidade de vertices");
            int numVertices = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe a quantidade de arestas");
            int numArestas = int.Parse(Console.ReadLine());

            List<(int origem, int destino, double peso)> pesoArestas = new List<(int, int, double)>();

            for (int i = 1; i <= numArestas; i++)
            {
                Console.WriteLine($"Informe o vértice de origem da aresta {i}:");
                int verticeOrigem = int.Parse(Console.ReadLine());

                Console.WriteLine($"Informe o vértice de destino da aresta {i}:");
                int verticeDestino = int.Parse(Console.ReadLine());

                Console.WriteLine($"Informe o peso da aresta {i}:");
                double peso = double.Parse(Console.ReadLine());

                
                pesoArestas.Add((verticeOrigem, verticeDestino, peso));
            }


            Grafo grafo = new Grafo(numVertices,numArestas,pesoArestas);
            Console.Clear();
            Console.WriteLine("Grafo criado");
            menuGrafo(grafo);
        }

        public static void menuGrafo(Grafo grafo)
        {
            bool repetidor = true;
            while (repetidor)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Adicionar vértice");
                Console.WriteLine("2. Adicionar aresta");
                Console.WriteLine("3. Listar grafo");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                int escolha = Console.ReadLine();

                switch (escolha)
                {
                    case 1:
                        addVertice(grafo);
                        break;
                    case 2:
                        addAresta(grafo);
                        break;
                    case 3:
                        grafo.listar();
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        repetidor = false;  
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        public static bool addVertice(Grafo grafo)
        {
            Console.WriteLine("Insira o número do vertice");
            int vertice = int.Parse(Console.ReadLine());
            if (grafo.AdicionaVertice(vertice) && vertice>0)
            {
                Console.Clear();
                Console.WriteLine("Adicionado com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vertice nao existe");
                return false;
            }
            return true;
        }

        public static bool addAresta(Grafo grafo)
        {
            Console.WriteLine("Insira o número do vertice de origem");
            int verticeOrigem = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira o número do vertice de destino");
            int verticeDestino = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira o peso da aresta");
            double peso = double.Parse(Console.ReadLine());

            if(grafo.AdicionarAresta(verticeOrigem, verticeDestino, peso){
                Console.Clear();
                Console.WriteLine("Adicionado com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Um ou os dois vértices não existem");
                return false;
            }
            return true;
        }
    }
