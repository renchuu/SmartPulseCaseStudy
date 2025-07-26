# SmartPulse Case Study
EPIAS verilerini istenilen tarih aralığına uygun çeker ve CaseStudy'de belirtilen formatta yazdırır.

- API requestleri için token alır.
- İki tarih arası verileri işlemek üzere çeker
- Contract tarihi ve saatini gruplayarak, toplam tutar, miktar ve ağırlıklı ortalama fiyatları hesaplar
- Sonucu tarayıcıda basit bir HTML tablosunda sunar

## Kullanmadan önce
Projeye **appsettings taslağı** eklenmiştir. Burada belirtilen alanlara [EPIAS Kayıt](https://kayit.epias.com.tr/epias-transparency-platform-registration-form) üzerinden açtığınız hesabın şifresini ve kulanıcı adını eklemelisiniz. 

Dosyanın adını **appsettings.json** olarak değiştirdikten sonra çalıştırabilirsiniz.

Tarih aralığını değiştirmek için TransactionService.GetTransaction içindeki payload objesini düzenleyiniz.
