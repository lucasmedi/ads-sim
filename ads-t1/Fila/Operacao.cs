
namespace ads_t1
{
    public class Operacao
    {
        public EnumOperacao Op { get; set; }
        public int tMin { get; set; }
        public int tMax { get; set; }
        public int IdFilaDestino { get; set; }
        public decimal Probabilidade { get; set; }

        public Operacao() { }

        public Operacao(EnumOperacao op, int tMin, int tMax, int idFilaDestino, decimal probabilidade)
        {
            this.Op = op;
            this.tMin = tMin;
            this.tMax = tMax;
            this.IdFilaDestino = idFilaDestino;
            this.Probabilidade = probabilidade;
        }
    }
}