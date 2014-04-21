using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;

namespace ads_t1
{
    public class Fila : IFila
    {
        public int Id { get; set; }

        public int Servidores { get; set; }
        public int Capacidade { get; set; }
        public int Quantidade { get; set; }

        public int Perdas { get; set; }
        public int Chegadas { get; set; }

        public bool TemPassagem { get; set; }
        public bool TemSaida { get; set; }
        public bool TemProbabilidade { get; set; }
        
        public List<Operacao> Operacoes { get; set; }

        public List<Agendamento> Agendamento { get; set; }
        public decimal Tempo { get; set; }
   
        public Fila(int id, int servidores, int capacidade)
        {
            Operacoes = new List<Operacao>();

            Id = id;
            Servidores = servidores;
            Capacidade = capacidade;
        }

        public void AdicionaOperacao(EnumOperacao op, int tempoMin, int tempoMax, int idFilaDestino = 0, decimal probabilidade = 0)
        {
            Operacoes.Add(new Operacao(op, tempoMin, tempoMax, idFilaDestino, probabilidade));

            if (probabilidade > 0)
                TemProbabilidade = true;

            if (op == EnumOperacao.Saida)
                TemSaida = true;

            if (op == EnumOperacao.Passagem)
                TemPassagem = true;
        }

        public List<Agendamento> Executa(IEvento evento)
        {
            Agendamento = new List<Agendamento>();
            Tempo = evento.Tempo;

            switch (evento.Operacao)
            {
                case EnumOperacao.Chegada:
                    Chegada();
                    break;
                case EnumOperacao.Saida:
                    Saida();
                    break;
                case EnumOperacao.Passagem:
                    Passagem(evento.IdFila == this.Id);
                    break;
                default:
                    break;
            }

            return Agendamento;
        }

        public void Agenda(EnumOperacao op)
        {
            if (op != EnumOperacao.Probabilidade)
            {
                var operacao = Operacoes.Where(o => o.Op == op).First();

                Agendamento.Add(new Agendamento
                {
                    Operacao = op,
                    vMin = operacao.tMin,
                    vMax = operacao.tMax,
                    TempoAtual = Tempo,
                    IdFilaDestino = operacao.IdFilaDestino,
                    Probabilidade = operacao.Probabilidade
                });
            }
            else
            {
                var operacao = Operacoes.OrderByDescending(o => o.Probabilidade).First();

                foreach (var item in Operacoes.Where(o => o.Probabilidade > 0).OrderByDescending(o => o.Probabilidade))
                {
                    Agendamento.Add(new Agendamento
                    {
                        Operacao = item.Op,
                        vMin = item.tMin,
                        vMax = item.tMax,
                        TempoAtual = Tempo,
                        IdFilaDestino = item.IdFilaDestino,
                        Probabilidade = item.Probabilidade
                    });
                }
            }

            
        }

        private void Chegada(bool bAgendaChegada = true)
        {
            if (Capacidade == 0 || Quantidade < Capacidade)
            {
                Chegadas++;
                Quantidade++;
                if (Quantidade <= Servidores)
                {
                    if (TemProbabilidade)
                    {
                        Agenda(EnumOperacao.Probabilidade);
                    }
                    else
                    {
                        if (TemPassagem)
                        {
                            Agenda(EnumOperacao.Passagem);
                        }
                        else
                        {
                            Agenda(EnumOperacao.Saida);
                        }
                    }
                }
            }
            else
            {
                Perdas++;
            }

            if (bAgendaChegada)
                Agenda(EnumOperacao.Chegada);
        }

        private void Passagem(bool bSaida = true)
        {
            if (bSaida)
            {
                if (!TemPassagem)
                    return;

                Quantidade--;
                if (Quantidade >= Servidores)
                {
                    if (TemProbabilidade)
                    {
                        Agenda(EnumOperacao.Probabilidade);
                    }
                    else
                    {
                        Agenda(EnumOperacao.Passagem);
                    }
                }
            }
            else
            {
                Chegada(false);
            }
        }

        private void Saida()
        {
            if (!TemSaida)
                return;

            Quantidade--;
            if (Quantidade >= 1)
            {
                if (TemProbabilidade)
                {
                    Agenda(EnumOperacao.Probabilidade);
                }
                else
                {
                    Agenda(EnumOperacao.Saida);
                }
            }
        }

        public int ImprimeRelatorio(ISheet sheet, int rowIndex)
        {
            rowIndex++;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue(string.Format("Fila {0}", this.Id));
            row.CreateCell(1).SetCellValue(string.Format("G/G/{0}/{1}", this.Servidores, this.Capacidade));
            
            foreach (var item in this.Operacoes)
            {
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(string.Format("{0}{1}{2}", EnumHelper.Escreve(item.Op), this.Id, (item.IdFilaDestino > 0 ? item.IdFilaDestino.ToString() : string.Empty)));
                row.CreateCell(1).SetCellValue(string.Format("{0}..{1} u.t.", item.tMin, item.tMax));
                if (item.Probabilidade > 0)
                    row.CreateCell(2).SetCellValue(item.Probabilidade.ToString());
            }

            rowIndex++;
            row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Atendidos:");
            row.CreateCell(1).SetCellValue(this.Chegadas);

            rowIndex++;
            row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Perdidos:");
            row.CreateCell(1).SetCellValue(this.Perdas);

            rowIndex++;
            row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("%Perdas:");
            row.CreateCell(1).SetCellValue(string.Format("{0}%", Math.Round(((this.Perdas / (decimal)(this.Chegadas+this.Perdas)) * (decimal)100), 2).ToString()));

            return rowIndex;
        }
    }
}