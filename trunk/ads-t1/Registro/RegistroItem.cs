using System.Collections.Generic;

namespace ads_t1
{
    public class RegistroItem
    {
        public string Id { get; set; }
        public decimal TempoTotal { get; set; }
        public List<RegistroFila> Filas { get; set; }

        public RegistroItem()
        {
            Filas = new List<RegistroFila>();
        }

        public RegistroItem(List<IFila> filas) : this()
        {
            Id = "-";
            TempoTotal = 0;

            foreach (var fila in filas)
            {
                var item = new RegistroFila
                {
                    IdFila = fila.Id,
                    Quantidade = fila.Quantidade
                };

                for (int i = 0; i <= fila.Capacidade; i++)
                {
                    item.Tempos.Add(i, 0);
                }

                Filas.Add(item);
            }
        }
    }
}