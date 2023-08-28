Bu proje, Görev 13'deki ödevin bir sonraki aşamasını içeriyor. 
Bu aşamada, önceki ödevde oluşturulan yapıyı katmanlı bir mimariye taşıyarak, 
MVC projesini daha organize ve yönetilebilir bir hale getiriyoruz.

KULLANIM
Örnek CRUD işlemleri için aşağıdaki adımları takip edebilirsiniz:

Ana sayfada mevcut öğeleri listeleyin.
Yeni öğeler eklemek için Add to cart tıklayın.
Öğeleri düzenlemek veya silmek için My carts sayfasına gidin.

**ÖZET**
Kullanıcı birden fazla sepet ekleyebilir.
Kullanıcı eklediği sepetleri ya da sepetteki ürünleri silebilir.
İstenilen sepete istenilen adette ürün eklenebilir.
Kullanıcılar farklı kullanıcıların sepetlerini göremez.

**DETAYLAR**
Bu proje katmanlı mimari, bağımlılık enjeksiyonu, yetkilendirme ve kimlik doğrulama gibi temel teknikleri içermektedir.
Modern yazılım geliştirme yaklaşımlarıyla şekillendirilmiş olan ASP.NET Core tabanlı bir uygulamayı temsil ediyor. Uygulamanın ana odak noktaları arasında, 
güvenli kullanıcı kimlik doğrulama ve yetkilendirme mekanizmalarının etkin bir şekilde yönetilmesi, ayrıca dinamik ve özelleştirilebilir bir ürün sepeti yönetiminin sağlanması yer almaktadır.
 Veri erişim operasyonları ise, Entity Framework Core'un sağladığı ORM yetenekleri ile soyut bir yapı üzerinde yürütülerek, 
veritabanı entegrasyonunun etkin ve düzenli bir biçimde gerçekleştirilmesine yönelik bir yaklaşım benimsenmiştir.

MVC tasarım kalıbına uygun olarak tasarlanmış, kullanıcı oturum açma, kaydolma, oturumu sonlandırma gibi 
kimlik doğrulama süreçlerini ele alan ve kullanıcının sepet içeriğini etkili bir şekilde yönetmesini sağlayan bileşenler olarak öne çıkmaktadır.

IRepository ve DataRepository yapısı, arayüzleri ve uygulamaları ayırarak, veritabanı işlemlerini soyutlar ve iş mantığı ile veri erişimi arasındaki bağı azaltır.
Aynı şekilde, CartController ve AuthController gibi denetleyiciler, sunum katmanını temsil eder ve iş mantığı ile kullanıcı arayüzünü bağımsız hale getirir.

"Services" koleksiyonuna eklenen hizmetler, tüm uygulama boyunca erişilebilir ve gerekli bağımlılıkları otomatik olarak enjekte eder. 
"AppDbContext" gibi bağımlılıklar, constructor enjeksiyonu ile denetleyicilere geçirilir. 
Aynı zamanda, servisleri yaşam döngülerine göre ayarlamak için "Scoped" kapsamı kullanılır.

AddAuthentication ve AddCookie gibi metodlar ile çerez tabanlı kimlik doğrulama ayarları yapılırken, 
denetleyicilerde [Authorize] ve [AllowAnonymous] nitelikleri ile belirli alanlara erişim yetkisi kontrol edilir.



