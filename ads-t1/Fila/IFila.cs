
namespace ads_t1
{
    /// <summary>
    /// Interface de fila
    /// </summary>
    public interface IFila
    {
        int Id { get; set; }
        int Servidores { get; set; }
        int Capacidade { get; set; }
        int Quantidade { get; set; }
    }
}