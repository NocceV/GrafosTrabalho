namespace code
{
    public class Aresta
    {
        private static int _ultimoId = 0;
        private int _id;
        private int? _peso;
        private Vertice _verticeOrigem;
        private Vertice _verticeDestino;

        public Vertice VerticeOrigem { get { return _verticeOrigem; } }


        //Construtor Padrao Classe Aresta
        public Aresta(Vertice verticeOrigem, Vertice verticeDestino, int? peso = null)
        {
            _id = ++_ultimoId;
            _verticeOrigem = verticeOrigem;
            _verticeDestino = verticeDestino;
            _peso = peso;
        }

        public override string ToString()
        {
            return _verticeOrigem + " -> " + _verticeDestino;
        }
    }
}
