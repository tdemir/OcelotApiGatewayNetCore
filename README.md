# OcelotApiGatewayNetCore

Çalıştırma işlemini uygulamadan önce localhost içerisinde postgresql ve rabbitmq yüklü olması gerekmektedir.
Postgresqlde postgres kullanıcısının şifresi 123 olmalıdır. Herhangi bir veritabanı oluşturmanıza gerek yok. Uygulama peopleDb ve reportDb adında iki veritabanını kendisi oluşturacaktır.
Konsol açıp, projenin root klasörüne gidin. Sonrasında alttaki kodu çalıştırın.

```bash
dotnet restore
dotnet build
```

Ardından 3 ayrı konsol açıp her birinde aşağıdaki scriptleri çalıştırın.

```bash
cd ./ApiGateway
dotnet run
```

```bash
cd ./PersonApi
dotnet run
```

```bash
cd ./ReportApi
dotnet run
```

Sonrasında projede olan postman_files klasöründeki collection ve environment dosyalarını postman'e yükleyin.
Yükledikten sonra tüm requestleri görebilirsiniz.

Not: Uygulama 4000, 4001 ve 4002 portlarını kullanmaktadır. Lütfen bu portları kontrol edin.
