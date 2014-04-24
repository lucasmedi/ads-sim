
namespace ads_t1
{
    /// <summary>
    /// Classe utilitária para retornar string de exibição do EnumOperacao
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Retorna a descrição de cada operação do EnumOperacao
        /// </summary>
        /// <param name="op"></param>
        /// <returns>String com descrição da operação</returns>
        public static string Escreve(EnumOperacao op)
        {
            switch (op)
            {
                case EnumOperacao.Chegada:
                    return "CH";
                case EnumOperacao.Saida:
                    return "SA";
                case EnumOperacao.Passagem:
                    return "P";
                default:
                    return "";
            }
        }
    }
}