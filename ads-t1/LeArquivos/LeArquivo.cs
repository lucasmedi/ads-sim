
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
namespace ads_t1
{

    public class LeArquivo
    {

        //StringWriter writer = new StringWriter();
        //      Fila fila3 = new Fila(id, 2, 3);

        //      List<Fila> filas = new List<Fila>();

        //      filas.Add(fila1);
        //      filas.Add(fila2);

        //      XmlSerializer serializer = new XmlSerializer(fila1.GetType());
        //      serializer.Serialize(writer, fila1);

        //      writer.ToString();

        //      List<Fila> filas2 = new List<Fila>();

        //      string file = @"C:\Users\Giovanni_2\Dropbox\PUCRS\Avaliação de desempenho de software\Trabalho 1\ads-sim\exemplo.xml";


        //      string[] path = Directory.GetFiles(@"C:\Users\Giovanni_2\Dropbox\PUCRS\Avaliação de desempenho de software\Trabalho 1\ads-sim\","*.xml");

        //      StreamReader objReader = new StreamReader(file);
        //      String a = objReader.ReadToEnd();
        //      //.Replace("\n", "").Replace("\r", "").Replace(@"\", "")
        //      StringReader reader = new StringReader(a);
        //      XmlSerializer serializer2 = new XmlSerializer(filas2.GetType());
        //      filas2 = (List<Fila>)serializer2.Deserialize(reader);


        public Queue<string> Arquivos { get; set; }

        public LeArquivo(string caminho)
        {
            string[] arquivosTemp = Directory.GetFiles(caminho, "*.xml");

            Arquivos = new Queue<string>();
            for (int i = 0; i < arquivosTemp.Length; i++)
                Arquivos.Enqueue(arquivosTemp[i]);
        }

        public List<Fila> carregaConteudo()
        {
            if (Arquivos.Count == 0)
                return null;

            List<Fila> filas = new List<Fila>();
            
            string arquivo = Arquivos.Dequeue();
            StreamReader objReader = new StreamReader(arquivo);
            string conteudo = objReader.ReadToEnd();
            StringReader reader = new StringReader(conteudo);
            XmlSerializer serializer = new XmlSerializer(filas.GetType());

            filas = (List<Fila>)serializer.Deserialize(reader);

            return filas;
        }




    }
}
