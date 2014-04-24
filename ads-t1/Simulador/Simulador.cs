using System;
using System.Collections.Generic;
using System.Linq;

namespace ads_t1
{
    /// <summary>
    /// Classe que controla o fluxo da simulação
    /// </summary>
    public class Simulador
    {
        private List<Fila> filas;
        private Agendador agendador;
        private Registro registro;

        public Simulador(decimal[] aleatorios)
        {
            agendador = new Agendador(aleatorios);
            filas = new List<Fila>();
        }

        public Simulador(int semente, int quantidade)
        {
            agendador = new Agendador(GerarNumerosAleatorios(semente, quantidade));
            filas = new List<Fila>();
        }

        /// <summary>
        /// Método responsável pelo início do processamento
        /// </summary>
        public void Iniciar()
        {
            registro = new Registro(filas.Select(o => (IFila)o).ToList());

            var sair = false;
            do
            {
                // Busca próximo evento a ser executado
                var evento = agendador.ProximoEvento();
                if (evento != null)
                {
                    // Busca a fila referenciada pelo evento
                    var fila = filas.Where(o => o.Id == evento.IdFila).First();

                    // Executa evento
                    ExecutaEvento(evento, fila);

                    // Caso evento seja uma passagem, executar a chegada na fila de destino
                    if (evento.Operacao == EnumOperacao.Passagem)
                    {
                        // Busca a fila de destino referenciada pelo evento
                        fila = filas.Where(o => o.Id == evento.IdFilaDestino).First();

                        // Executa evento
                        ExecutaEvento(evento, fila);
                    }

                    // Adiciona execução do evento ao registro
                    registro.AdicionaEvento(evento, filas);
                }
                else
                {
                    sair = true;
                }
            } while (!sair);

            // Gera relatório de estatística do processamento
            Export.GeraExcel(filas, agendador, registro);
        }

        /// <summary>
        /// Adiciona fila à lista de filas
        /// </summary>
        /// <param name="fila"></param>
        public void AdicionaFila(Fila fila)
        {
            filas.Add(fila);
        }

        /// <summary>
        /// Agenda as operações de inicío do processamento
        /// </summary>
        /// <param name="idFila"></param>
        /// <param name="op"></param>
        /// <param name="tempo"></param>
        public void AgendaInicio(int idFila, EnumOperacao op, decimal tempo)
        {
            agendador.Agendar(idFila, op, tempo);
        }

        /// <summary>
        /// Executa evento para a fila informada
        /// </summary>
        /// <param name="evento"></param>
        /// <param name="fila"></param>
        private void ExecutaEvento(IEvento evento, Fila fila)
        {
            // Executa evento e retorna os eventos a serem agendados
            var agendamento = fila.Executa(evento);

            // Caso haja probabilidade envolvida, tratar especialmente para tomada de decisão
            if (agendamento.Count(o => o.Probabilidade > 0) > 0)
            {
                agendador.AgendarProbabilidade(fila.Id, agendamento.Where(o => o.Probabilidade > 0).ToList());
                agendamento.RemoveAll(o => o.Probabilidade > 0);
            }

            // Realiza o agendamento dos próximos eventos
            foreach (var item in agendamento)
            {
                agendador.Agendar(fila.Id, item.Operacao, item.TempoAtual, item.vMin, item.vMax, item.IdFilaDestino, item.Probabilidade);
            }
        }

        /// <summary>
        /// Gera os números aleatórios a serem utilizados pelo agendador
        /// </summary>
        /// <param name="semente"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
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