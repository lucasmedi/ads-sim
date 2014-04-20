using System;

namespace ads_t1
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 0;

            var simulador = new Simulador(0);

            #region Teste 1

            // Teste 1
            // Fila 1: G/G/1/4
            // 2..3 ut CH1
            // 3..5 ut SA1

            //id++;
            //var fila = new Fila(id, 1, 4);
            //fila.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila);

            //simulador.AgendaInicio(id, EnumOperacao.Chegada, (decimal)2.5);

            #endregion

            #region Teste 2

            // Teste 2
            // Fila 1: G/G/2/3
            // 2..3 ut CH1
            // 2..5 ut P12
            // Fila 2: G/G/1/3
            // 3..5 ut SA2

            //id++;
            //var fila1 = new Fila(id, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //simulador.AdicionaFila(fila1);

            //id++;
            //var fila2 = new Fila(id, 1, 3);
            //fila2.AdicionaOperacao(EnumOperacao.Saida, 3, 5);
            //simulador.AdicionaFila(fila2);

            //fila1.AdicionaOperacao(EnumOperacao.Passagem, 2, 5, fila2.Id);

            //simulador.AgendaInicio(id - 1, EnumOperacao.Chegada, (decimal)2.5);

            #endregion

            #region Teste 3

            // Teste 3
            // Fila 1: G/G/2/4
            // 2..3 ut CH1
            // 0,7 2..5 ut P12
            // 0,3 4..7 ut SA1
            // Fila 2: G/G/1/0
            // 4..8 ut SA2

            //id++;
            //var fila1 = new Fila(id, 2, 4);
            //fila1.AdicionaOperacao(EnumOperacao.Chegada, 2, 3);
            //fila1.AdicionaOperacao(EnumOperacao.Saida, 4, 7, 0, (decimal)0.3);
            //simulador.AdicionaFila(fila1);

            //id++;
            //var fila2 = new Fila(id, 1, 0);
            //fila2.AdicionaOperacao(EnumOperacao.Saida, 4, 8);
            //simulador.AdicionaFila(fila2);

            //fila1.AdicionaOperacao(EnumOperacao.Passagem, 4, 7, fila2.Id, (decimal)0.7);

            //simulador.AgendaInicio(id - 1, EnumOperacao.Chegada, (decimal)3.0);

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

            id++;
            var fila1 = new Fila(id, 2, 3);
            fila1.AdicionaOperacao(EnumOperacao.Chegada, 1, 2);
            simulador.AdicionaFila(fila1);

            id++;
            var fila2 = new Fila(id, 3, 5);
            fila2.AdicionaOperacao(EnumOperacao.Chegada, 1, 2);
            fila2.AdicionaOperacao(EnumOperacao.Saida, 4, 5, 0, (decimal)0.6);
            simulador.AdicionaFila(fila2);

            fila1.AdicionaOperacao(EnumOperacao.Passagem, 2, 3, fila2.Id);
            fila2.AdicionaOperacao(EnumOperacao.Passagem, 4, 5, fila1.Id, (decimal)0.4);

            simulador.AgendaInicio(fila1.Id, EnumOperacao.Chegada, (decimal)2.0);
            simulador.AgendaInicio(fila2.Id, EnumOperacao.Chegada, (decimal)1.0);

            #endregion

            simulador.Iniciar();

            Console.ReadKey();
        }
    }
}