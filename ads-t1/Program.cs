using System;
using System.Linq;
using System.Xml.Linq;

namespace ads_t1
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 0;

            Simulador simulador = null;
            Fila fila1 = null;
            Fila fila2 = null;

            #region Teste 1

            // Teste 1
            // Fila 1: G/G/1/4
            // 2..3 ut CH1
            // 3..5 ut SA1

            //simulador = new Simulador(new decimal[] {
            //    Convert.ToDecimal(0.1195),
            //    Convert.ToDecimal(0.3491),
            //    Convert.ToDecimal(0.9832),
            //    Convert.ToDecimal(0.7731),
            //    Convert.ToDecimal(0.8935),
            //    Convert.ToDecimal(0.2103),
            //    Convert.ToDecimal(0.0392),
            //    Convert.ToDecimal(0.1782)
            //});

            //id++;
            //fila1 = new Fila(id, 1, 4);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila1);

            //simulador.AgendaInicio(id, EnumOperacao.Chegada, (decimal)2.5);

            //simulador.Iniciar();

            #endregion

            #region Teste 2

            // Teste 2
            // Fila 1: G/G/2/3
            // 2..3 ut CH1
            // 2..5 ut P12
            // Fila 2: G/G/1/3
            // 3..5 ut SA2

            //id = 0;
            //simulador = new Simulador(new decimal[] {
            //    Convert.ToDecimal(0.9921),
            //    Convert.ToDecimal(0.0004),
            //    Convert.ToDecimal(0.5534),
            //    Convert.ToDecimal(0.2761),
            //    Convert.ToDecimal(0.3398),
            //    Convert.ToDecimal(0.8963),
            //    Convert.ToDecimal(0.9023),
            //    Convert.ToDecimal(0.0132),
            //    Convert.ToDecimal(0.4569),
            //    Convert.ToDecimal(0.5121),
            //    Convert.ToDecimal(0.9208),
            //    Convert.ToDecimal(0.0171),
            //    Convert.ToDecimal(0.2299),
            //    Convert.ToDecimal(0.8545),
            //    Convert.ToDecimal(0.6001),
            //    Convert.ToDecimal(0.2921)
            //});

            //id++;
            //fila1 = new Fila(id, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //simulador.AdicionaFila(fila1);

            //id++;
            //fila2 = new Fila(id, 1, 3);
            //fila2.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila2);

            //fila1.AdicionaOperacao(EnumOperacao.Passagem, 2, 5, fila2.Id);

            //simulador.AgendaInicio(id - 1, EnumOperacao.Chegada, (decimal)2.5);

            //simulador.Iniciar();

            #endregion

            #region Teste 3

            // Teste 3
            // Fila 1: G/G/2/4
            // 2..3 ut CH1
            // 0,7 2..5 ut P12
            // 0,3 4..7 ut SA1
            // Fila 2: G/G/1/0
            // 4..8 ut SA2

            //id = 0;
            //simulador = new Simulador(new decimal[] {
            //    Convert.ToDecimal(0.2176),
            //    Convert.ToDecimal(0.0103),
            //    Convert.ToDecimal(0.1109),
            //    Convert.ToDecimal(0.3456),
            //    Convert.ToDecimal(0.9910),
            //    Convert.ToDecimal(0.2323),
            //    Convert.ToDecimal(0.9211),
            //    Convert.ToDecimal(0.0322),
            //    Convert.ToDecimal(0.1211),
            //    Convert.ToDecimal(0.5131),
            //    Convert.ToDecimal(0.7208),
            //    Convert.ToDecimal(0.9172),
            //    Convert.ToDecimal(0.9922),
            //    Convert.ToDecimal(0.8324),
            //    Convert.ToDecimal(0.5011),
            //    Convert.ToDecimal(0.2931)
            //});

            //id++;
            //fila1 = new Fila(id, 2, 4);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Saida, 4, 7, 0, (decimal)0.3);
            //simulador.AdicionaFila(fila1);

            //id++;
            //fila2 = new Fila(id, 1, 0);
            //fila2.AdicionaOperacao(EnumOperacao.Saida, 4, 8);
            //simulador.AdicionaFila(fila2);

            //fila1.AdicionaOperacao(EnumOperacao.Passagem, 4, 7, fila2.Id, (decimal)0.7);

            //simulador.AgendaInicio(id - 1, EnumOperacao.Chegada, (decimal)3.0);

            //simulador.Iniciar();

            #endregion

            #region Teste 4

            // Teste 4
            // Fila 1: G/G/2/3
            // 1..2 ut CH1
            // 2..3 ut P12
            // Fila 2: G/G/3/5
            // 1..2 ut CH2
            // 0,6 4..5 ut SA2
            // 0,4 4..5 ut P21

            //id = 0;
            //simulador = new Simulador(new decimal[] {
            //    Convert.ToDecimal(0.3281),
            //    Convert.ToDecimal(0.1133),
            //    Convert.ToDecimal(0.3332),
            //    Convert.ToDecimal(0.5634),
            //    Convert.ToDecimal(0.1099),
            //    Convert.ToDecimal(0.1221),
            //    Convert.ToDecimal(0.7271),
            //    Convert.ToDecimal(0.0301),
            //    Convert.ToDecimal(0.8291),
            //    Convert.ToDecimal(0.3131),
            //    Convert.ToDecimal(0.5232),
            //    Convert.ToDecimal(0.7291),
            //    Convert.ToDecimal(0.9129),
            //    Convert.ToDecimal(0.8723),
            //    Convert.ToDecimal(0.4101),
            //    Convert.ToDecimal(0.2209)
            //});

            //id++;
            //fila1 = new Fila(id, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 1, 2);
            //simulador.AdicionaFila(fila1);

            //id++;
            //fila2 = new Fila(id, 3, 5);
            //fila2.AdicionaOperacao(EnumOperacao.Chegada, 1, 2);
            //fila2.AdicionaOperacao(EnumOperacao.Saida, 4, 5, 0, (decimal)0.6);
            //simulador.AdicionaFila(fila2);

            //fila1.AdicionaOperacao(EnumOperacao.Passagem, 2, 3, fila2.Id);
            //fila2.AdicionaOperacao(EnumOperacao.Passagem, 4, 5, fila1.Id, (decimal)0.4);

            //simulador.AgendaInicio(fila1.Id, EnumOperacao.Chegada, (decimal)2.0);
            //simulador.AgendaInicio(fila2.Id, EnumOperacao.Chegada, (decimal)1.0);

            //simulador.Iniciar();

            #endregion

            #region Teste 5

            // Teste 1
            // Fila 1: G/G/1/4
            // 2..3 ut CH1
            // 3..5 ut SA1

            //id = 0;
            //simulador = new Simulador(5670, 8);

            //id++;
            //fila1 = new Fila(id, 1, 4);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila1);

            //simulador.AgendaInicio(id, EnumOperacao.Chegada, (decimal)2.5);

            //simulador.Iniciar();

            #endregion

            #region Teste 6

            // Teste 1
            // Fila 1: G/G/1/4
            // 2..3 ut CH1
            // 3..5 ut SA1

            //id = 0;
            //simulador = new Simulador(5670, 500);

            //id++;
            //fila1 = new Fila(id, 1, 4);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila1);

            //simulador.AgendaInicio(id, EnumOperacao.Chegada, (decimal)2.5);

            //simulador.Iniciar();

            #endregion

            #region Teste 7

            var leArquivo = new LeArquivo(@"entrada\");

            do
            {
                simulador = new Simulador(572, 200);

                var xDoc = leArquivo.carregaConteudo();

                var filasDocs = xDoc.Descendants("filas").Elements("fila").ToList();
                var chegadasDocs = xDoc.Descendants("chegadas").Elements("chegada").ToList();
                var configuracaoDocs = xDoc.Descendants("configuracao").First();

                // tag semente
                var semente = configuracaoDocs.Element("semente").Value;
                var nroaleatorios = configuracaoDocs.Element("nroaleatorios").Value;

                // tag fila
                foreach (var filaDoc in filasDocs)
                {
                    var idfila = Int32.Parse(filaDoc.Element("id").Value);
                    var servidores = Int32.Parse(filaDoc.Element("servidores").Value);
                    var capacidade = Int32.Parse(filaDoc.Element("capacidade").Value);
                    
                    var fila = new Fila(idfila, servidores, capacidade);

                    simulador.AdicionaFila(fila);

                    // tag operacoes
                    var operacoesDocs = filaDoc.Descendants("operacoes").Elements("operacao").ToList();
                    foreach (var operacao in operacoesDocs)
                    {
                        var op = operacao.Element("op").Value;

                        int tempoMin = 0;
                        if(operacao.Element("tmin") != null)
                        {
                            tempoMin = Convert.ToInt32(operacao.Element("tmin").Value);
                        }

                        int tempoMax = 0;
                        if (operacao.Element("tmax") != null)
                        {
                            tempoMax = Convert.ToInt32(operacao.Element("tmax").Value);
                        }

                        int idfiladestino = 0;
                        if (operacao.Element("idfiladestino") != null)
                        {
                            idfiladestino = Int32.Parse(operacao.Element("idfiladestino").Value);
                        }

                        decimal probabilidade = 0;
                        if (operacao.Element("probabilidade") != null)
                        {
                            probabilidade = Decimal.Parse(operacao.Element("probabilidade").Value.Replace(".", ","));
                        }

                        fila.AdicionaOperacao((EnumOperacao)Int32.Parse(op), tempoMin, tempoMax, idfiladestino, probabilidade);
                    }
                }

                // tag chegada
                foreach (var chegada in chegadasDocs)
                {
                    var idfila = Int32.Parse(chegada.Element("idfila").Value);
                    var tempo = Decimal.Parse(chegada.Element("tempo").Value.Replace(".", ","));

                    simulador.AgendaInicio(idfila, EnumOperacao.Chegada, tempo);
                }

                simulador.Iniciar();

            } while (leArquivo.temArquivo());

            #endregion
        }
    }
}