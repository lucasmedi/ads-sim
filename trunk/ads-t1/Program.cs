
namespace ads_t1
{
    class Program
    {
        static void Main(string[] args)
        {
            var leitor = new LeArquivo(AppHelper.CaminhoLeitura);
            var simuladores = leitor.BuscaSimuladores();
            foreach (var simulador in simuladores)
            {
                simulador.Iniciar();
            }
        }
    }
}