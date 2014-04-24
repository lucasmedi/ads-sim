
namespace ads_t1
{
    /// <summary>
    /// Classe que especifica um evento
    /// </summary>
    public class Evento : IEvento
    {
        public int Id { get; set; }
        public int IdFila { get; set; }
        public int IdFilaDestino { get; set; }
        public EnumOperacao Operacao { get; set; }
        public decimal Probabilidade { get; set; }
        public bool Consumido { get; set; }
        public decimal Tempo { get; set; }
        public decimal Sorteio { get; set; }
    }
}