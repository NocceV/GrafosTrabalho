using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class Adjacencia
    {
        private int origem;
        private int destino;
        private int peso;
        public Adjacencia(int origem, int destino, int peso)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }
        public int getDestino()
        {
            return destino;
        }
        public int getPeso()
        {
            return peso;
        }
        public int getOrigem()
        {
            return peso;
        }
        public void setOrigem(int origem)
        {
            this.origem = origem;
        }
        public void setDestino(int destino)
        {
            this.destino = destino;
        }
        public void setPeso(int peso)
        {
            this.peso = peso;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("----------------------------");
            str.AppendLine("Origem - "+origem);
            str.AppendLine("Destino - "+destino);
            str.AppendLine("Peso - "+peso);
            str.AppendLine("----------------------------");
            return str.ToString();
        }
    }
}
