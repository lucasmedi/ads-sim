using System;
using System.Collections.Generic;
using System.Linq;

namespace ads_t1
{
    public class Simulador
    {
        private Agendador agendador;
        private Registro registro;

        private List<Fila> filas;

        public Simulador(int semente, int quantidade)
        {
            agendador = new Agendador(GerarNumerosAleatorios(semente, quantidade));
            filas = new List<Fila>();
        }

        public Simulador(decimal[] aleatorios)
        {
            agendador = new Agendador(aleatorios);
            filas = new List<Fila>();
        }

        public void Iniciar()
        {
            registro = new Registro(filas.Select(o => (IFila)o).ToList());

            var sair = false;
            do
            {
                var evento = agendador.ProximoEvento();
                if (evento != null)
                {
                    var fila = filas.Where(o => o.Id == evento.IdFila).First();

                    var agendamento = fila.Executa(evento);
                    if (agendamento.Count(o => o.Probabilidade > 0) > 0)
                    {
                        agendador.AgendarProbabilidade(fila.Id, agendamento.Where(o => o.Probabilidade > 0).ToList());
                        agendamento.RemoveAll(o => o.Probabilidade > 0);
                    }

                    foreach (var item in agendamento)
                    {
                        agendador.Agendar(fila.Id, item.Operacao, item.TempoAtual, item.vMin, item.vMax, item.IdFilaDestino, item.Probabilidade);
                    }

                    if (evento.Operacao == EnumOperacao.Passagem)
                    {
                        fila = filas.Where(o => o.Id == evento.IdFilaDestino).First();
                        agendamento = fila.Executa(evento);
                        if (agendamento.Count(o => o.Probabilidade > 0) > 0)
                        {
                            agendador.AgendarProbabilidade(fila.Id, agendamento.Where(o => o.Probabilidade > 0).ToList());
                            agendamento.RemoveAll(o => o.Probabilidade > 0);
                        }

                        foreach (var item in agendamento)
                        {
                            agendador.Agendar(fila.Id, item.Operacao, item.TempoAtual, item.vMin, item.vMax, item.IdFilaDestino, item.Probabilidade);
                        }
                    }

                    registro.AdicionaEvento(evento, filas);
                }
                else
                {
                    sair = true;
                }
            } while (!sair);

            Export.GeraExcel(filas, agendador, registro);
        }

        public void AdicionaFila(Fila fila)
        {
            filas.Add(fila);
        }

        public void AgendaInicio(int idFila, EnumOperacao op, decimal tempo)
        {
            agendador.Agendar(idFila, op, tempo);
        }

        private decimal[] GerarNumerosAleatorios(int semente, int quantidade)
        {
            var aleatorios = new List<decimal>();

            var r = new Random(semente);
            for (int i = 0; i < quantidade; i++)
            {
                aleatorios.Add(Math.Round((decimal)r.NextDouble(), 4));
            }

            return aleatorios.ToArray();
        }
    }
}