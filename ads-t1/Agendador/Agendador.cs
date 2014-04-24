using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ads_t1
{
    /// <summary>
    /// Classe que gerencia a agenda de eventos
    /// </summary>
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

        /// <summary>
        /// Agenda um novo evento conforme parâmetros informados
        /// </summary>
        /// <param name="idFila"></param>
        /// <param name="op"></param>
        /// <param name="tempo"></param>
        /// <param name="sorteio"></param>
        /// <param name="idFilaDestino"></param>
        /// <param name="probabilidade"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Agenda um novo evento conforme parâmetros informados
        /// </summary>
        /// <param name="idFila"></param>
        /// <param name="op"></param>
        /// <param name="tempoAtual"></param>
        /// <param name="vMin"></param>
        /// <param name="vMax"></param>
        /// <param name="idFilaDestino"></param>
        /// <param name="probabilidade"></param>
        /// <returns></returns>
        public bool Agendar(int idFila, EnumOperacao op, decimal tempoAtual, int vMin, int vMax, int idFilaDestino = 0, decimal probabilidade = 0)
        {
            if (Aleatorios.Count == 0)
                return false;

            var aleatorio = Aleatorios.Dequeue();
            var tempoSorteio = (vMax - vMin) * aleatorio + vMin;

            return Agendar(idFila, op, (tempoAtual + tempoSorteio), tempoSorteio, idFilaDestino, probabilidade);
        }

        /// <summary>
        /// Agenda especial para casos com probabilidade, onde deve-se utilizar um aleatório para decidir o caminho
        /// </summary>
        /// <param name="idFila"></param>
        /// <param name="agenda"></param>
        /// <returns></returns>
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
                ag = item;
                if (aleatorio < prob)
                    break;
            }

            return Agendar(idFila, ag.Operacao, ag.TempoAtual, ag.vMin, ag.vMax, ag.IdFilaDestino, ag.Probabilidade);
        }

        /// <summary>
        /// Retorna próximo evento a ser executado conforme tempo atual (menor tempo da lista)
        /// </summary>
        /// <returns></returns>
        public IEvento ProximoEvento()
        {
            if (Aleatorios.Count == 0)
                return null;

            var evento = Eventos.Where(o => !o.Consumido).OrderBy(o => o.Tempo).FirstOrDefault();
            if (evento != null)
                evento.Consumido = true;

            return evento;
        }

        /// <summary>
        /// Imprime agenda de eventos no relatório de estatísticas da execução
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Registra aleatórios informados
        /// </summary>
        /// <param name="aleatorios"></param>
        private void RegistraAleatorios(decimal[] aleatorios)
        {
            this.Aleatorios = new Queue<decimal>();
            for (int i = 0; i < aleatorios.Length; i++)
                Aleatorios.Enqueue(aleatorios[i]);
        }
    }
}