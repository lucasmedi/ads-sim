using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ads_t1
{
    public class LeArquivo
    {
        public Queue<string> Arquivos { get; set; }

        public LeArquivo(string caminho)
        {
            var arquivosTemp = Directory.GetFiles(caminho, "*.xml");

            Arquivos = new Queue<string>();
            for (int i = 0; i < arquivosTemp.Length; i++)
                Arquivos.Enqueue(arquivosTemp[i]);
        }

        public bool temArquivo()
        {
            return (Arquivos.Count > 0);
        }

        public XDocument carregaConteudo()
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