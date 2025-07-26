using SmartPulseCaseStudy.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPulseCaseStudy
{
    public class DisplayTable
    {
        public static void OpenInBrowser(List<TableContent> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"<html>
                                <head><meta charset='utf-8'/>
                                    <style>table{border-collapse:collapse;font-family:Arial}th,
                                        td{border:1px solid #999;padding:4px 8px;text-align:right}
                                        th{background:#eee}</style>
                                </head><body><table>");
            sb.AppendLine("<tr><th>Tarih</th>" +
                "               <th>Toplam İşlem Tutarı</th>" +
                "               <th>Toplam İşlem Miktarı</th>" +
                "               <th>Ağırlıklı Ortalama Fiyat</th></tr>");

            foreach (var r in rows)
            {
                sb.AppendLine(
                    $"<tr><td>{r.date:dd.MM.yyyy HH:mm}</td>" +
                    $"<td>{r.totalPrice:N2}</td>" +
                    $"<td>{r.totalQuantity:N1}</td>" +
                    $"<td>{r.averagePrice:N2}</td></tr>");
            }

            sb.AppendLine("</table></body></html>");

            var temp = Path.GetTempFileName() + ".html";
            File.WriteAllText(temp, sb.ToString());
            Process.Start(new ProcessStartInfo(temp) { UseShellExecute = true });
        }
    }
}
