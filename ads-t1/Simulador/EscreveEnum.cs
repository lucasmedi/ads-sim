
namespace ads_t1
{
    public class EnumHelper
    {
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