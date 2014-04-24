using System.Configuration;

namespace ads_t1
{
    /// <summary>
    /// Classe para auxiliar o acesso as configurações da aplicação (app.config)
    /// </summary>
    public class AppHelper
    {
        public static string CaminhoLeitura
        {
            get
            {
                return Fonte("caminho.leitura");
            }
        }

        public static string CaminhoEscrita
        {
            get
            {
                return Fonte("caminho.escrita");
            }
        }

        private static string Fonte(string chave)
        {
            return ConfigurationSettings.AppSettings[chave];
        }
    }
}