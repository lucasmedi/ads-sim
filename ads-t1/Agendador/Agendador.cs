using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ads_t1
{
    public class Agendador
    {
        private int _id;
        private int _Id
        {
            get
            {
                _id++;
                return _id;
            }
        }

        private Queue<decimal> Aleatorios { get; set; }
        private List<Evento> Eventos { get; set; }

        public Agendador(decimal[] aleatorios)
        {
            Eventos = new List<Evento>();

            _id = 0;
            RegistraAleatorios(aleatorios);
        }

        public bool Agendar(int idFila, EnumOperacao op, decimal tempo, decimal sorteio = 0, int idFilaDestino = 0, decimal probabilidade = 0)
        {
            var id = _Id;
            Eventos.Add(new Evento()
            {
                Id = id,
                IdFila = idFila,
                IdFilaDestino = idFilaDestino,
                Operacao = op,
                Sorteio = sorteio,
                Tempo = tempo,
                Probabilidade = probabilidade
            });

            return true;
        }

        public bool Agendar(int idFila, EnumOperacao op, decimal tempoAtual, int vMin, int vMax, int idFilaDestino = 0, decimal probabilidade = 0)
        {
            if (Aleatorios.Count == 0)
                return false;

            var aleatorio = Aleatorios.Dequeue();
            var tempoSorteio = vMin + (vMax - vMin) * aleatorio;

            return Agendar(idFila, op, (tempoAtual + tempoSorteio), tempoSorteio, idFilaDestino, probabilidade);
        }

        public bool AgendarProbabilidade(int idFila, List<Agendamento> agenda)
        {
            if (Aleatorios.Count == 0)
                return false;

            var aleatorio = Aleatorios.Dequeue();

            var prob = (decimal)0;
            Agendamento ag = null;
            foreach (var item in agenda.OrderByDescending(o => o.Probabilidade))
            {
                prob += item.Probabilidade;
                if (aleatorio < prob)
                {
                    ag = item;
                    break;
                }
            }

            return Agendar(idFila, ag.Operacao, ag.TempoAtual, ag.vMin, ag.vMax, ag.IdFilaDestino, ag.Probabilidade);
        }

        public IEvento ProximoEvento()
        {
            if (Aleatorios.Count == 0)
                return null;

            var evento = Eventos.Where(o => !o.Consumido).OrderBy(o => o.Tempo).FirstOrDefault();
            if (evento != null)
                evento.Consumido = true;

            return evento;
        }

        public void Imprime()
        {
            Console.WriteLine("\n**** Agenda de Eventos *****");
            foreach (var item in Eventos)
                Console.WriteLine("({0}) {1}{2}{3} | Tempo: {4} | Sorteio: {5}", item.Id, EnumHelper.Escreve(item.Operacao), item.IdFila, (item.IdFilaDestino > 0 ? item.IdFilaDestino.ToString() : string.Empty), item.Tempo, item.Sorteio);
        }

        public int ImprimeRelatorio(ISheet sheet, int rowIndex)
        {
            sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue("**** Agenda de Eventos ****");
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 3));

            rowIndex++;
            var row = sheet.CreateRow(rowIndex);

            row.CreateCell(0).SetCellValue("Evento");
            row.CreateCell(1).SetCellValue("Tempo");
            row.CreateCell(2).SetCellValue("Sorteio");
            row.CreateCell(3).SetCellValue("Utilizado");

            foreach (var item in Eventos)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(string.Format("({0}) {1}{2}{3}", item.Id, EnumHelper.Escreve(item.Operacao), item.IdFila, (item.IdFilaDestino > 0 ? item.IdFilaDestino.ToString() : string.Empty)));
                row.CreateCell(1).SetCellValue(item.Tempo.ToString());
                row.CreateCell(2).SetCellValue(item.Sorteio.ToString());
                row.CreateCell(3).SetCellValue(item.Consumido ? "Sim" : "Não");
            }

            return rowIndex;
        }

        private void RegistraAleatorios(decimal[] ale)
        {
            this.Aleatorios = new Queue<decimal>();
            for (int i = 0; i < ale.Length; i++)
                Aleatorios.Enqueue(ale[i]);
        }
    }
}