using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class Program
    {
        /// <summary>
        /// Metodo main que inicia a aplicação/sistema
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                start();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }

        }

        /// <summary>
        /// Start para criar o grafo
        /// </summary>
        public static void start()
        {
            int codigoEscolha;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(menuInicial());
                    if (!int.TryParse(Console.ReadLine(), out codigoEscolha))
                    {
                        Console.Clear();
                        Console.WriteLine("Por favor, insira um número válido.");
                        continue;
                    }
                    Console.Clear();
                    switch (codigoEscolha)
                    {
                        case 1:
                            criarGrafoDigitando();
                            break;
                        case 2:
                            criarGrafoDimac();
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
                    codigoEscolha = -1;
                }
            } while (codigoEscolha != 0);
        }

        /// <summary>
        /// Menu inicial para crair grafo
        /// </summary>
        /// <returns>Retorna um stringbuilder com o menu inicial</returns>
        public static string menuInicial()
        {
            StringBuilder construtorMenu = new StringBuilder();
            construtorMenu.AppendLine("Bem vindo ao sistema de Grafos!");
            construtorMenu.AppendLine("1) Criar grafo digitando");
            construtorMenu.AppendLine("2) Criar grafo com arquivo DIMAC");
            construtorMenu.AppendLine("0) Sair");
            return construtorMenu.ToString();
        }

        /// <summary>
        /// Após escolher criar o grafo, o usuário informa os detalhes para criar o grafo
        /// (numero de vertices, numero de arestas,peso das arestas)
        /// </summary>
        public static void criarGrafoDigitando()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Informe a quantidade de vértices:");
                if (!int.TryParse(Console.ReadLine(), out int numVertices) || numVertices <= 0)
                {
                    Console.WriteLine("Número de vértices inválido.");
                    return;
                }

                Console.WriteLine("Informe a quantidade de arestas:");
                if (!int.TryParse(Console.ReadLine(), out int numArestas) || numArestas < 0)
                {
                    Console.WriteLine("Número de arestas inválido.");
                    return;
                }

                Console.Clear();
                List<List<int>> dimic = criarDimac(numVertices, numArestas);

                IGrafo grafo;

                if (calcularDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafosMatriz(numVertices, dimic);
                }
                else
                {
                    grafo = new GrafosLista(numVertices, dimic);
                }

                Console.Clear();
                Console.WriteLine("Grafo criado :D");
                Console.WriteLine("Presione Enter...");
                Console.ReadLine();
                Console.Clear();
                menuGrafo(grafo);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria as informações do grafo com base no modelo dimic
        /// </summary>
        /// <param name="numVertices">Número de vértices</param>
        /// <param name="numArestas">Número de arestas</param>
        /// <returns>Retorna lista com base no modelo dimic</returns>
        public static List<List<int>> criarDimac(int numVertices, int numArestas)
        {
            List<List<int>> dimic = new List<List<int>>();

            for (int i = 1; i <= numArestas; i++)
            {
                try
                {
                    Console.WriteLine($"Informe o vértice de origem da aresta {i}:");
                    if (!int.TryParse(Console.ReadLine(), out int verticeOrigem) || verticeOrigem < 0 || verticeOrigem >= numVertices)
                    {
                        Console.WriteLine("Vértice de origem inválido.");
                        i--;
                        continue;
                    }

                    Console.WriteLine($"Informe o vértice de destino da aresta {i}:");
                    if (!int.TryParse(Console.ReadLine(), out int verticeDestino) || verticeDestino < 0 || verticeDestino >= numVertices)
                    {
                        Console.WriteLine("Vértice de destino inválido.");
                        i--;
                        continue;
                    }

                    Console.WriteLine($"Informe o peso da aresta {i}:");
                    if (!int.TryParse(Console.ReadLine(), out int peso) || peso < 0)
                    {
                        Console.WriteLine("Peso inválido.");
                        i--;
                        continue;
                    }

                    dimic.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar a aresta {i}: {ex.Message}");
                    i--;
                }
            }

            return dimic;
        }

        /// <summary>
        /// Cria grafo a partir de arquivo DIMAC.
        /// </summary>
        public static void criarGrafoDimac()
        {
            try
            {
                Console.WriteLine("Insira o caminho do arquivo DIMACS:");
                string caminhoArquivo = Console.ReadLine();
                if (!File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("Arquivo não encontrado.");
                    return;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo);

                if (linhas.Length < 1)
                {
                    Console.WriteLine("Arquivo DIMACS inválido.");
                    return;
                }

               
                string[] primeiraLinha = linhas[0].Split(' ');
                if (primeiraLinha.Length < 2 ||
                    !int.TryParse(primeiraLinha[0], out int numVertices) ||
                    !int.TryParse(primeiraLinha[1], out int numArestas))
                {
                    Console.WriteLine("Formato inválido na primeira linha.");
                    return;
                }

                List<List<int>> dimic = new List<List<int>>();

                for (int i = 1; i < linhas.Length; i++)
                {
                    string[] dadosAresta = linhas[i].Split(' ');
                    if (dadosAresta.Length < 3 ||
                        !int.TryParse(dadosAresta[0], out int verticeOrigem) ||
                        !int.TryParse(dadosAresta[1], out int verticeDestino) ||
                        !int.TryParse(dadosAresta[2], out int peso))
                    {
                        Console.WriteLine($"Formato inválido na linha {i + 1}.");
                        return;
                    }

                    dimic.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }

                IGrafo grafo;
                if (calcularDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafosMatriz(numVertices, dimic);
                }
                else
                {
                    grafo = new GrafosLista(numVertices, dimic);
                }

                Console.Clear();
                Console.WriteLine("Grafo criado :D");
                Console.WriteLine("Presione Enter...");
                Console.ReadLine();
                Console.Clear();
                menuGrafo(grafo);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
            }
        }

        /// <summary>
        /// Metodo de calcular densidade do grafo
        /// </summary>
        /// <param name="numVertices">Número de vértices</param>
        /// <param name="numArestas">Número de arestas</param>
        /// <returns>Peso do grafo</returns>
        public static double calcularDensidade(int numVertices, int numArestas)
        {
            double densidade = (2 * numArestas) / (numVertices * (numVertices - 1));
            return densidade;
        }

      

        /// <summary>
        /// Menu do grafo que leva a várias funcionalidades do sistema
        /// </summary>
        /// <param name="grafo">Grafo em forma de lista ou matriz</param>
        public static void menuGrafo(IGrafo grafo)
        {
            bool repetidor = true;
            while (repetidor)
            {
                Console.Clear();
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Adicionar aresta");
                Console.WriteLine("2. Listar grafo");
                Console.WriteLine("3. Imprimir arestas adjacentes");
                Console.WriteLine("4. Imprimir vertices adjacentes");
                Console.WriteLine("5. Imprimir arestas incidentes");
                Console.WriteLine("6. Imprimir vertices incidentes");
                Console.WriteLine("7. Imprimir grau do vértice");
                Console.WriteLine("8. Determinar adjacencia de vértices");
                Console.WriteLine("9. Substituir peso de aresta");
                Console.WriteLine("10. Trocar dois vértices.");
                Console.WriteLine("11. Fazer busca.");
                Console.WriteLine("12. Determinar caminho mínimo.");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                int escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        addAresta(grafo);
                        break;
                    case 2:
                        listarGrafo(grafo);
                        break;
                    case 3:
                        imprimirArestasAdjacentes(grafo);
                        break;
                    case 4:
                        imprimirVerticesAdjacentes(grafo);
                        break;
                    case 5:
                        imprimirArestasIncidentes(grafo);
                        break;
                    case 6:
                        imprimirVerticesIncidentes(grafo);
                        break;
                    case 7:
                        imprimirGrauVertice(grafo);
                        break;
                    case 8:
                        determinarAdjacencia(grafo);
                        break;
                    case 9:
                        substituirPesos(grafo);
                        break;
                    case 10:
                        trocarAdjacencia(grafo);
                        break;
                    case 11:
                        //peitos
                        break;
                    case 12:
                        //boobs
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        repetidor = false;
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;

                }
                Console.WriteLine("Presione Enter");
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Metodo de adicionar aresta no grafo
        /// </summary>
        /// <param name="grafo">Grafo em forma de lista ou matriz</param>
        /// <returns>Retorna true caso adicionar a aresta tenha sido sucesso</returns>
        public static bool addAresta(IGrafo grafo)
        {
            try
            {
                Console.WriteLine("Insira o número do vértice de origem:");
                if (!int.TryParse(Console.ReadLine(), out int verticeOrigem))
                {
                    Console.WriteLine("Vértice de origem inválido.");
                    return false;
                }

                Console.WriteLine("Insira o número do vértice de destino:");
                if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                {
                    Console.WriteLine("Vértice de destino inválido.");
                    return false;
                }

                Console.WriteLine("Insira o peso da aresta:");
                if (!int.TryParse(Console.ReadLine(), out int peso))
                {
                    Console.WriteLine("Peso inválido.");
                    return false;
                }

                if (grafo.AdicionarAresta(verticeOrigem, verticeDestino, peso))
                {
                    Console.Clear();
                    Console.WriteLine("Aresta adicionada com sucesso.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erro ao adicionar a aresta. Verifique os vértices.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar aresta: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Metodo de listar o grafo
        /// </summary>
        /// <param name="grafo">Grafo em forma de lista ou matriz</param>
        public static void listarGrafo(IGrafo grafo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Grafo: ");
                Console.WriteLine(grafo.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar o grafo: {ex.Message}");
            }
        }

        /// <summary>
        /// Imprime as arestas adjacentes de determinada aresta.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void imprimirArestasAdjacentes(IGrafo grafo)
        {
            Adjacencia adjacencia = criarAdjacencia();
            List<Adjacencia> lista = grafo.ArestasAdjacentes(adjacencia);

            Console.Clear();
            Console.WriteLine("Arestas adjacentes: ");
            foreach (Adjacencia aresta in lista){
                aresta.ToString();
            }
            
        }

        /// <summary>
        /// Imprimeos vértices adjacentes de determinada aresta.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void imprimirVerticesAdjacentes(IGrafo grafo)
        {

        }

        /// <summary>
        /// Imprime as arestas incidentes de determinado vértice.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void imprimirArestasIncidentes(IGrafo grafo){

            int vertice = informeVertice();
            List<Adjacencia> lista = grafo.ArestasIncidentes(vertice);

            Console.Clear();
            Console.WriteLine($"Arestas incidentes ao vértice: {vertice} ");
            foreach (Adjacencia aresta in lista){
                aresta.ToString();
            }
        }

        /// <summary>
        /// Imprime os vértices incidentes de cada vértice passado.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void imprimirVerticesIncidentes(IGrafo grafo)
        {
            Console.WriteLine("Informe origem");
            int origem = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe destino");
            int destino = int.Parse(Console.ReadLine());

            List<int> lista = grafo.VerticesIncidentes(origem, destino);

            Console.Clear();
            Console.WriteLine("Vértices incidentes: ");
            foreach (int vertices in lista){
              Console.WriteLine(vertices.ToString());   
            }
        }

        /// <summary>
        /// Imprime o grau do vértice.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void imprimirGrauVertice(IGrafo grafo)
        {        
            int vertice = informeVertice();

            Console.Clear();
            Console.WriteLine("Grau do vértice: ");
            Console.WriteLine(grafo.GrauVertice(vertice));
        }


        /// <summary>
        /// Informa se dois vértices são adjacentes.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void determinarAdjacencia(IGrafo grafo){

            int vertice1 = informeVertice();
            Console.Write(" 1");

            int vertice2 = informeVertice();
            Console.Write(" 2");

            Console.Clear();
            bool resposta = grafo.VerificarVizinhos(vertice1, vertice2);
            if (resposta){
                Console.WriteLine("Vértices são vizinhos");
            }
            else{
                Console.WriteLine("Vértices não são vizinhos"); 
            }

        }

        /// <summary>
        /// Troca o peso de uma aresta existente
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void substituirPesos(IGrafo grafo)
        {
            int origem = informeVertice();
            Console.Write(" de origem");

            int destino = informeVertice();
            Console.Write(" de destino");

            Console.WriteLine("Informe o peso");
            int peso = int.Parse(Console.ReadLine());

            bool resultado = grafo.TrocarPeso(origem,destino, peso);

            Console.Clear();
            if (resultado)
            {
                Console.WriteLine("Peso substituido com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao substituir peso");
            }
        }

        /// <summary>
        /// Troca as adjacentes entre dois vértices.
        /// </summary>
        /// <param name="grafo">Interface Grafo.</param>
        public static void trocarAdjacencia(IGrafo grafo)
        {
            int vertice1 = informeVertice();
            Console.Write(" 1");

            int vertice2 = informeVertice();
            Console.Write(" 2");

            bool sucesso = grafo.TrocarAdjacencias(vertice1, vertice2);
            Console.Clear();
            if (sucesso)
            {
                Console.WriteLine("Troca feita com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao fazer troca");
            }
        }

        /// <summary>
        /// Metodo para capturar um vértice determinado pelo usuário.
        /// </summary>
        /// <returns>Número do vértice</returns>
        public static int informeVertice()
        {
            Console.WriteLine("Informe o vértice");
            int vertice = int.Parse(Console.ReadLine());
            return vertice;
        }

        /// <summary>
        /// Criar uma aresta.
        /// </summary>
        /// <returns>Retorna uma aresta</returns>
        public static Adjacencia criarAdjacencia()
        {
            Console.Clear();
            Console.WriteLine("Informe o vértice de origem da aresta");
            int origem = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o vértice de destino da aresta");
            int destino = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o peso da aresta");
            int peso = int.Parse(Console.ReadLine());

            Adjacencia adjacencia = new Adjacencia(origem, destino, peso);

            return adjacencia;
        } 
    }
}
