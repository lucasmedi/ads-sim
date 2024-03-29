﻿using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;

namespace ads_t1
{
    /// <summary>
    /// Classe responsável pela geração do excel com as estatísticas da simulação
    /// </summary>
    public class Export
    {
        public static void GeraExcel(List<Fila> filas, Agendador agendador, Registro registro)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet");

            var rowIndex = 0;
            sheet.CreateRow(0).CreateCell(0).SetCellValue("ADS-T1 Simulador");
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 1)); 

            rowIndex += 2;
            foreach (var fila in filas)
            {
                rowIndex = fila.ImprimeRelatorio(sheet, rowIndex);
            }
            //rowIndex += 3;
            // rowIndex = agendador.ImprimeRelatorio(sheet, rowIndex);
            rowIndex += 3;
            registro.ImprimeRelatorio(sheet, rowIndex);

            var file = new FileStream(string.Format(@"{0}\relatorio_{1}.xls", AppHelper.CaminhoEscrita, DateTime.Now.Ticks), FileMode.Create);
            workbook.Write(file);
            file.Close();
        }
    }
}