using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ads_t1
{
    public class Registro
    {
        private List<RegistroItem> itens;

        public Registro(List<IFila> filas)
        {
            itens = new List<RegistroItem>();
            itens.Add(new RegistroItem(filas));
        }

        public void AdicionaEvento(IEvento evento, List<Fila> filas)
        {
            var item = new RegistroItem
            {
                Id = string.Format("({0}) {1}{2}{3}", evento.Id, EnumHelper.Escreve(evento.Operacao), evento.IdFila, (evento.IdFilaDestino > 0 ? evento.IdFilaDestino.ToString() : string.Empty)),
                TempoTotal = evento.Tempo
            };

            var last = itens.Last();
            var tempoLast = last.TempoTotal;
            foreach (var reg in last.Filas)
            {
                var fila = filas.Where(o => o.Id == reg.IdFila).First();

                var regFila = new RegistroFila
                {
                    IdFila = fila.Id,
                    Quantidade = fila.Quantidade
                };

                foreach (var key in reg.Tempos.Keys)
                {
                    regFila.Tempos.Add(key, reg.Tempos[key]);
                }

                if (!regFila.Tempos.ContainsKey(reg.Quantidade))
                    regFila.Tempos.Add(reg.Quantidade, 0);

                regFila.Tempos[reg.Quantidade] += evento.Tempo - tempoLast;
                item.Filas.Add(regFila);
            }

            itens.Add(item);
        }

        public int ImprimeRelatorio(ISheet sheet, int rowIndex)
        {
            sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue("**** Tabela de Eventos ****");
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 3));

            rowIndex++;
            var row = sheet.CreateRow(rowIndex);

            row.CreateCell(0).SetCellValue("Id");
            row.CreateCell(1).SetCellValue("Tempo Total");

            var bFirstRow = true;

            foreach (var item in itens)
            {
                var cellIndex = 0;
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(cellIndex).SetCellValue(item.Id);

                cellIndex++;
                row.CreateCell(cellIndex).SetCellValue(item.TempoTotal.ToString());

                foreach (var subitem in item.Filas)
                {
                    cellIndex++;
                    if (bFirstRow)
                    {
                        sheet.GetRow(rowIndex - 1).CreateCell(cellIndex).SetCellValue(string.Format("Fila {0}", subitem.IdFila));
                    }

                    row.CreateCell(cellIndex).SetCellValue(subitem.Quantidade);
                    foreach (var tempo in subitem.Tempos.Keys)
                    {
                        cellIndex++;
                        if (bFirstRow)
                        {
                            sheet.GetRow(rowIndex - 1).CreateCell(cellIndex).SetCellValue(tempo);
                        }
                        
                        row.CreateCell(cellIndex).SetCellValue(subitem.Tempos[tempo].ToString());
                    }
                }

                if (bFirstRow)
                    bFirstRow = false;
            }

            rowIndex += 2;
            var proporcao = itens.Last();
            foreach (var fila in proporcao.Filas)
            {
                bFirstRow = true;

                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                if (bFirstRow)
                {
                    row.CreateCell(0).SetCellValue(string.Format("Fila {0}", fila.IdFila));
                    row.CreateCell(1).SetCellValue("Tempo");
                    row.CreateCell(2).SetCellValue("%");
                    bFirstRow = false;
                }

                foreach (var key in fila.Tempos.Keys)
                {
                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(key);
                    row.CreateCell(1).SetCellValue(fila.Tempos[key].ToString());
                    row.CreateCell(2).SetCellValue(string.Format("{0}%", Math.Round((fila.Tempos[key] / proporcao.TempoTotal) * 100, 4).ToString()));
                }

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue("Tempo Total");
                row.CreateCell(1).SetCellValue(proporcao.TempoTotal.ToString());
                row.CreateCell(2).SetCellValue("100.0%");
                
                rowIndex++;
            }

            return rowIndex;
        }
    }
}