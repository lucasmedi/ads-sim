
namespace ads_t1
{
    /// <summary>
    /// Classe que define um agendamento
    /// </summary>
    public class Agendamento
    {
        public EnumOperacao Operacao { get; set; }
        public int vMin { get; set; }
        public int vMax { get; set; }
        public decimal TempoAtual { get; set; }
        public int IdFilaDestino { get; set; }
        public decimal Probabilidade { get; set; }
    }
}