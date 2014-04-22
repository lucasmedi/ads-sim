
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace ads_t1
{

    public class LeArquivo
    {
        public Queue<string> Arquivos { get; set; }

        public LeArquivo(string caminho)
        {
            string[] arquivosTemp = Directory.GetFiles(caminho, "*.xml");

            Arquivos = new Queue<string>();
            for (int i = 0; i < arquivosTemp.Length; i++)
                Arquivos.Enqueue(arquivosTemp[i]);
        }

        public bool temArquivo()
        {
            if (Arquivos.Count > 0)
                return true;
            else
                return false;
        }

        public string carregaConteudo()
        {
            if (Arquivos.Count == 0)
                return null;

            return Arquivos.Dequeue();
        }

    }
}
