using Microsoft.Extensions.Configuration;
using SmartPulseCaseStudy;
using System.Threading.Tasks;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builder.Build();

//Appsettings.json dosyasindan login bilgilerini TGTRequestInfo objectine donustur
var loginInfo = configuration.GetSection("Login");

TGTService _TGTservice = new TGTService(loginInfo);

string TGT = await _TGTservice.GetTGT();

TransactionService transactionService = new TransactionService(TGT);

transactionService.DisplayResult();
