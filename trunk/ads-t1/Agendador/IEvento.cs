
namespace ads_t1
{
    public interface IEvento
    {
        int Id { get; set; }
        int IdFila { get; set; }
        int IdFilaDestino { get; set; }
        EnumOperacao Operacao { get; set; }
        bool Consumido { get; set; }
        decimal Tempo { get; set; }
        decimal Probabilidade { get; set; }
    }
}