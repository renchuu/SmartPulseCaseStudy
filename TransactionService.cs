using SmartPulseCaseStudy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartPulseCaseStudy
{
    class TransactionService
    {
        private string _TGT;

        public TransactionService(string TGT) {
            _TGT = TGT;
        }

        public void DisplayResult()
        {
            var results = TransactionProcessing();
 
            DisplayTable.OpenInBrowser(results); // tablo olarak tarayicida goruntuler
        }

        public List<TableContent> TransactionProcessing()
        {
            var list = GetTransactions().Result;


            //aldigmiz responce listesini yeni formata uyarlayarak filtreliyoruz
            var filteredList = list.GroupBy(c => c.contractName)
                .Select(g => new TableContent
                {
                    date = contractNameParser(g.Key),
                    totalPrice = Math.Round(g.Sum(x => (decimal) x.price) / 10, 2),
                    totalQuantity = Math.Round(g.Sum(x => (decimal) x.quantity) / 10, 2),                    
                    averagePrice = Math.Round(
                                    (g.Sum(x => (decimal)x.price)/10m) /
                                    (g.Sum(x => (decimal)x.quantity) / 10) , 2)
                }).OrderBy(o => o.date).ToList();

            return filteredList; 
        }

        public DateTime contractNameParser(string contractName)
        {
            int year = int.Parse(contractName.Substring(2, 2)); //PH kismini atladik e.g PH25012703
            int month = int.Parse(contractName.Substring(4, 2));
            int day = int.Parse(contractName.Substring(6, 2));
            int hour = int.Parse(contractName.Substring(8, 2));

            DateTime date = new DateTime(2000 + year, month, day, hour, 0, 0);
            return date;
        }

        public async Task<List<Responce>> GetTransactions()
        {
            //isteyecegimiz zaman araligi
            var payload = new
            {
                startDate = "2025-01-27T00:00:00+03:00",
                endDate = "2025-01-28T00:00:00+03:00",
            };
            var jsonPayload = JsonSerializer.Serialize(payload);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("TGT", _TGT);  // TGT'yi header'a ekliyoruz

            Console.WriteLine("2025-01-27 / 2025-01-28 tarihleri arası işlem verileri alınıyor...");
            
            var response = await client.PostAsync("https://seffaflik.epias.com.tr/electricity-service/v1/markets/idm/data/transaction-history", 
                new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            var json = await response.Content.ReadAsStringAsync();
            var body = JsonDocument.Parse(json).RootElement.GetProperty("items"); // "items" kısmını ayikliyoruz

            var transactions = JsonSerializer.Deserialize<List<Responce>>(body);

            Console.WriteLine("İşlem verileri alındı.");

            return transactions;
        }
    }
}
