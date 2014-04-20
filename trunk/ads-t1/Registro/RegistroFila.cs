using System.Collections.Generic;

namespace ads_t1
{
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