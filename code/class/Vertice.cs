namespace code
{
    internal class Vertice
    {
        private static int _ultimoId = 0;
        private int _id;
        private List<Aresta> _arestasVertice;
        private dynamic _objeto;

        //Construtor Padrão Classe Vertice
        public Vertice()
        {
            _id = ++_ultimoId;
            _arestasVertice = new List<Aresta>();
            dynamic _objeto = null;
        }

        //Construtor com Instâcia de Objeto Dinâmico
        public Vertice(dynamic objeto)
        {
            _id = ++_ultimoId;
            _arestasVertice = new List<Aresta>();
            dynamic _objeto = objeto;
        }

        public override string ToString()
        {
            return "v" + Id;
        }
    }
}