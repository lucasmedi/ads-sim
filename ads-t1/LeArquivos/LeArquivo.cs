using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ads_t1
{
    /// <summary>
    /// Classe responsável pelo carregamento dos arquivos da pasta indicada
    /// </summary>
    public class LeArquivo
    {
        private Queue<string> Arquivos;

        public LeArquivo(string caminho)
        {
            // Busca arquivos xml da pasta indicada
            var arquivosTemp = Directory.GetFiles(caminho, "*.xml");

            Arquivos = new Queue<string>();
            for (int i = 0; i < arquivosTemp.Length; i++)
                Arquivos.Enqueue(arquivosTemp[i]);
        }

        public List<Simulador> BuscaSimuladores()
        {
            var lista = new List<Simulador>();

            var leArquivo = new LeArquivo(@"entrada\");

            while (leArquivo.TemArquivo())
            {
                var xDoc = leArquivo.CarregaConteudo();

                var filasDocs = xDoc.Descendants("filas").Elements("fila").ToList();
                var chegadasDocs = xDoc.Descendants("chegadas").Elements("chegada").ToList();
                var configuracaoDocs = xDoc.Descendants("configuracao").First();

                // tag semente
                var semente = Int32.Parse(configuracaoDocs.Element("semente").Value);
                var nroaleatorios = Int32.Parse(configuracaoDocs.Element("nroaleatorios").Value);

                var simulador = new Simulador(semente, nroaleatorios);

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
                        if (operacao.Element("tmin") != null)
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

                lista.Add(simulador);
            }

            return lista;
        }

        private bool TemArquivo()
        {
            return (Arquivos.Count > 0);
        }

        private XDocument CarregaConteudo()
        {
            try
            {
                if (Arquivos.Count == 0)
                    return null;

                return XDocument.Load(Arquivos.Dequeue());
            }
            catch (XmlException e)
            {
                throw e;
            }
        }
    }
}