using System.Collections.Generic;

namespace ads_t1
{
    /// <summary>
    /// Classe que mantem o registro dos tempos individuais dos estados da fila
    /// </summary>
    public class RegistroFila
    {
        public int IdFila { get; set; }
        public int Quantidade { get; set; }
        public Dictionary<int, decimal> Tempos { get; set; }

        public RegistroFila()
        {
            Tempos = new Dictionary<int, decimal>();
        }
    }
}