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

        public Simulador(int semente)
        {
            agendador = new Agendador(GerarNumerosAleatorios(semente));

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

            agendador.Imprime();
            registro.Imprime();

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

        private decimal[] GerarNumerosAleatorios(int semente)
        {
            // TODO: Externar
            return new decimal[]
            {
                #region Teste 1
                //Convert.ToDecimal(0.1195),
                //Convert.ToDecimal(0.3491),
                //Convert.ToDecimal(0.9832),
                //Convert.ToDecimal(0.7731),
                //Convert.ToDecimal(0.8935),
                //Convert.ToDecimal(0.2103),
                //Convert.ToDecimal(0.0392),
                //Convert.ToDecimal(0.1782)
                #endregion

                #region Teste 2
                //Convert.ToDecimal(0.9921),
                //Convert.ToDecimal(0.0004),
                //Convert.ToDecimal(0.5534),
                //Convert.ToDecimal(0.2761),
                //Convert.ToDecimal(0.3398),
                //Convert.ToDecimal(0.8963),
                //Convert.ToDecimal(0.9023),
                //Convert.ToDecimal(0.0132),
                //Convert.ToDecimal(0.4569),
                //Convert.ToDecimal(0.5121),
                //Convert.ToDecimal(0.9208),
                //Convert.ToDecimal(0.0171),
                //Convert.ToDecimal(0.2299),
                //Convert.ToDecimal(0.8545),
                //Convert.ToDecimal(0.6001),
                //Convert.ToDecimal(0.2921)
                #endregion

                #region Teste 3
                //Convert.ToDecimal(0.2176),
                //Convert.ToDecimal(0.0103),
                //Convert.ToDecimal(0.1109),
                //Convert.ToDecimal(0.3456),
                //Convert.ToDecimal(0.9910),
                //Convert.ToDecimal(0.2323),
                //Convert.ToDecimal(0.9211),
                //Convert.ToDecimal(0.0322),
                //Convert.ToDecimal(0.1211),
                //Convert.ToDecimal(0.5131),
                //Convert.ToDecimal(0.7208),
                //Convert.ToDecimal(0.9172),
                //Convert.ToDecimal(0.9922),
                //Convert.ToDecimal(0.8324),
                //Convert.ToDecimal(0.5011),
                //Convert.ToDecimal(0.2931)
                #endregion

                #region Teste 4
                Convert.ToDecimal(0.3281),
                Convert.ToDecimal(0.1133),
                Convert.ToDecimal(0.3332),
                Convert.ToDecimal(0.5634),
                Convert.ToDecimal(0.1099),
                Convert.ToDecimal(0.1221),
                Convert.ToDecimal(0.7271),
                Convert.ToDecimal(0.0301),
                Convert.ToDecimal(0.8291),
                Convert.ToDecimal(0.3131),
                Convert.ToDecimal(0.5232),
                Convert.ToDecimal(0.7291),
                Convert.ToDecimal(0.9129),
                Convert.ToDecimal(0.8723),
                Convert.ToDecimal(0.4101),
                Convert.ToDecimal(0.2209)
                #endregion
            };
        }
    }
}