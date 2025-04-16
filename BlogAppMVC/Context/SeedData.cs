using BlogAppMVC.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Context
{
    public class SeedData
    {
        public static void PushData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Text = "Web Geliştirme", Url = "web-gelistirme", Color = CategoryColors.primary },
                        new Category { Text = "Backend Geliştirme", Url = "backend-gelistirme", Color = CategoryColors.danger },
                        new Category { Text = "Frontend Geliştirme", Url = "frontend-gelistirme", Color = CategoryColors.secondary },
                        new Category { Text = "Oyun Geliştirme", Url = "oyun-gelistirme", Color = CategoryColors.success },
                        new Category { Text = "Makine Öğrenmesi", Url = "makine-ogrenmesi", Color = CategoryColors.info },
                        new Category { Text = "Veri Bilimi", Url = "veri-bilimi", Color = CategoryColors.warning },
                        new Category { Text = "Yapay Zeka", Url = "yapay-zeka", Color = CategoryColors.primary },
                        new Category { Text = "DevOps", Url = "devops", Color = CategoryColors.primary },
                        new Category { Text = "Siber Güvenlik", Url = "siber-guvenlik", Color = CategoryColors.secondary },
                        new Category { Text = "Bulut Bilişim", Url = "bulut-bilisim", Color = CategoryColors.success }
                    );
                    context.SaveChanges();
                }
                // user şifre: 123456 admin şifre:170509
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "ahmetkaya", Image = "p1.jpg", Name = "Ahmet Kaya", Email = "ahmetkaya@example.com", Password = "AQAAAAIAAYagAAAAEJhSjv3KumguuDJKqrZ4S7hYOOWTr7VnrwGqW3nY8+bYgTW82gpwmhx+yZXj3K3LRQ==" },
                        new User { UserName = "iremalgul", Image = "p2.jpg", Name = "İrem Algül", Email = "iremalgul@example.com", Password = "AQAAAAIAAYagAAAAEJhSjv3KumguuDJKqrZ4S7hYOOWTr7VnrwGqW3nY8+bYgTW82gpwmhx+yZXj3K3LRQ==" },
                        new User { UserName = "denizkara", Image = "default.jpg", Name = "Deniz Kara", Email = "denizkara@example.com", Password = "AQAAAAIAAYagAAAAEJhSjv3KumguuDJKqrZ4S7hYOOWTr7VnrwGqW3nY8+bYgTW82gpwmhx+yZXj3K3LRQ==" },
                        new User { UserName = "esrabekir", Image = "default.jpg", Name = "Esra Bekir", Email = "esrabekir@example.com", Password = "AQAAAAIAAYagAAAAEJhSjv3KumguuDJKqrZ4S7hYOOWTr7VnrwGqW3nY8+bYgTW82gpwmhx" },
                        new User { UserName = "büşra", Image = "default.jpg", Name = "Büşra", Email = "büşra@example.com", Password = "AQAAAAIAAYagAAAAEJhSjv3KumguuDJKqrZ4S7hYOOWTr7VnrwGqW3nY8+bYgTW82gpwmhx+yZXj3K3LRQ==" },
                        new User { UserName = "admin", Image = "default.jpg", Name = "admin", Email = "admin@example.com", Password = "AQAAAAIAAYagAAAAEKmQyoI7NJYt5hsdbwgXq0RG7e0oHJrKfPo3vPLxdbIwlyF4U6AV/ueTNGXjPHr+bA==" }


                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "Web Geliştirme Bootcamp'i",
                            Content = "<p class=\"\" data-start=\"188\" data-end=\"536\"><strong data-start=\"188\" data-end=\"217\">Web Geliştirme Bootcamp&rsquo;i</strong>, yazılım d&uuml;nyasına adım atmak isteyen ya da mevcut bilgilerini ileri seviyeye taşımak isteyen bireyler i&ccedil;in hazırlanmış yoğun ve kapsamlı bir eğitim programıdır. Bu bootcamp s&uuml;recinde katılımcılar, modern web teknolojileri ile tanışarak hem frontend hem de backend geliştirme konularında uygulamalı deneyim kazanırlar.</p>\r\n<p class=\"\" data-start=\"538\" data-end=\"911\">Eğitim boyunca HTML, CSS ve JavaScript gibi temel teknolojilerin yanı sıra, React, Angular gibi pop&uuml;ler frontend k&uuml;t&uuml;phaneleri ve Node.js, .NET Core gibi backend teknolojileri &uuml;zerinde &ccedil;alışmalar ger&ccedil;ekleştirilir. Veritabanı y&ouml;netimi, RESTful API tasarımı, kullanıcı kimlik doğrulama, responsive tasarım ve deployment gibi konular da programın &ouml;nemli par&ccedil;aları arasındadır.</p>\r\n<p class=\"\" data-start=\"913\" data-end=\"1264\">Bootcamp, proje tabanlı bir yaklaşımla ilerlediği i&ccedil;in katılımcılar &ouml;ğrendiklerini ger&ccedil;ek d&uuml;nya uygulamaları geliştirerek pekiştirirler. B&ouml;ylece portf&ouml;ylerine g&uuml;&ccedil;l&uuml; projeler ekleyerek kariyerlerinde &ouml;nemli bir adım atma fırsatı yakalarlar. Ayrıca mentorluk desteği ve kod incelemeleri sayesinde kaliteli yazılım geliştirme prensipleri de kazandırılır.</p>\r\n<p class=\"\" data-start=\"1266\" data-end=\"1550\">Sonu&ccedil; olarak Web Geliştirme Bootcamp&rsquo;i, kısa s&uuml;rede yoğun bir bilgi birikimi edinmek ve teknoloji alanında kendini geliştirmek isteyen herkes i&ccedil;in ideal bir &ouml;ğrenme yolculuğudur. Katılımcılar bu s&uuml;re&ccedil; sonunda hem teknik bilgiye hem de sekt&ouml;rel farkındalığa sahip olarak mezun olurlar.</p>",
                            Description = "Web geliştirme temellerini öğrenin ve bir frontend geliştiricisi olun.",
                            Url = "web-gelistirme-bootcamp",
                            IsActive = true,
                            Image = "web-gelistirme.jpg",
                            PublishedOn = DateTime.Now.AddDays(-30),
                            Categories = context.Categories.Take(3).ToList(),
                            UserId = 1,
                            Comments = new List<Comment>
                            {
                                new Comment { Text = "Harika bir kurs, React hakkında çok şey öğrendim!", PublishedOn = DateTime.Now.AddDays(-25), UserId = 2 },
                                new Comment { Text = "Bir sonraki modülü sabırsızlıkla bekliyorum!", PublishedOn = DateTime.Now.AddDays(-20), UserId = 3 }
                            }
                        },
                        new Post
                        {
                            Title = "ASP.NET Core ile Backend Geliştirme",
                            Content = "<p class=\"\" data-start=\"171\" data-end=\"512\"><strong data-start=\"171\" data-end=\"210\">ASP.NET Core ile Backend Geliştirme</strong>, modern, &ouml;l&ccedil;eklenebilir ve y&uuml;ksek performanslı web uygulamaları geliştirmek isteyen geliştiriciler i&ccedil;in g&uuml;&ccedil;l&uuml; bir &ccedil;&ouml;z&uuml;md&uuml;r. Microsoft tarafından geliştirilen ASP.NET Core, a&ccedil;ık kaynaklı ve platformlar arası (cross-platform) bir framework olarak, hem Windows hem de Linux sistemlerde &ccedil;alışabilmektedir.</p>\r\n<p class=\"\" data-start=\"514\" data-end=\"881\">Bu teknolojiyi kullanarak geliştiriciler; RESTful API&rsquo;ler oluşturabilir, MVC mimarisi ile dinamik web sayfaları geliştirebilir ve modern web uygulamaları i&ccedil;in sağlam bir altyapı inşa edebilirler. Katmanlı mimari, bağımlılıkların y&ouml;netimi (Dependency Injection), middleware kullanımı ve g&uuml;&ccedil;l&uuml; routing &ouml;zellikleri ASP.NET Core&rsquo;un sunduğu başlıca avantajlar arasındadır.</p>\r\n<p class=\"\" data-start=\"883\" data-end=\"1238\">Veritabanı işlemleri i&ccedil;in genellikle Entity Framework Core kullanılır ve bu sayede verilerle nesne y&ouml;nelimli şekilde kolayca &ccedil;alışmak m&uuml;mk&uuml;n hale gelir. Kimlik doğrulama, yetkilendirme ve g&uuml;venlik gibi konular da ASP.NET Core ile entegre bir şekilde y&ouml;netilebilir. Bu da onu kurumsal projelerde tercih edilen sağlam bir backend teknolojisi haline getirir.</p>\r\n<p class=\"\" data-start=\"1240\" data-end=\"1582\"><strong data-start=\"1240\" data-end=\"1287\">ASP.NET Core ile Backend Geliştirme eğitimi</strong>, katılımcılara temel CRUD işlemlerinden, kullanıcı y&ouml;netimine, token tabanlı kimlik doğrulamadan, servis mimarisine kadar geniş bir yelpazede bilgi sunar. Katılımcılar bu s&uuml;re&ccedil;te, modern yazılım geliştirme prensiplerini &ouml;ğrenerek sekt&ouml;rde kullanılabilir projeler geliştirme becerisi kazanırlar.</p>",
                            Description = "ASP.NET Core ile ölçeklenebilir, yüksek performanslı backend sistemleri geliştirmeyi öğrenin.",
                            Url = "backend-gelistirme-aspnet-core",
                            IsActive = true,
                            Image = "backend-gelistirme.jpg",
                            PublishedOn = DateTime.Now.AddDays(-45),
                            Categories = context.Categories.Where(t => t.Text.Contains("Backend")).Take(2).ToList(),
                            UserId = 2,
                            Comments = new List<Comment>
                            {
                                new Comment { Text = "Bu kurs çok öğretici, çok faydalı!", PublishedOn = DateTime.Now.AddDays(-40), UserId = 1 },
                                new Comment { Text = "Kariyerimi değiştirmem için tam ihtiyacım olan kurs!", PublishedOn = DateTime.Now.AddDays(-35), UserId = 3 }
                            }
                        },
                        new Post
                        {
                            Title = "Makine Öğrenmesine Giriş",
                            Content = "<p class=\"\" data-start=\"112\" data-end=\"531\"><strong data-start=\"112\" data-end=\"140\">Makine &Ouml;ğrenmesine Giriş</strong>, veri odaklı d&uuml;ş&uuml;nebilen ve geleceğin teknolojilerini anlamak isteyen bireyler i&ccedil;in olduk&ccedil;a &ouml;nemli bir başlangı&ccedil; noktasıdır. Makine &ouml;ğrenmesi (Machine Learning), bilgisayarların a&ccedil;ık&ccedil;a programlanmadan veri &uuml;zerinden &ouml;ğrenmesini sağlayan bir yapay zeka alt alanıdır. Bu yaklaşım sayesinde sistemler, verilerdeki &ouml;r&uuml;nt&uuml;leri (pattern) keşfederek tahminlerde ya da karar vermelerde bulunabilir.</p>\r\n<p class=\"\" data-start=\"533\" data-end=\"953\">Bu alanda kullanılan algoritmalar genellikle &uuml;&ccedil; ana başlıkta incelenir: denetimli &ouml;ğrenme (supervised learning), denetimsiz &ouml;ğrenme (unsupervised learning) ve pekiştirmeli &ouml;ğrenme (reinforcement learning). Denetimli &ouml;ğrenmede sistem, etiketlenmiş verilerle eğitilirken; denetimsiz &ouml;ğrenmede etiketlenmemiş verilerden yapılar &ccedil;ıkarılır. Pekiştirmeli &ouml;ğrenmede ise sistem, &ouml;d&uuml;l-ceza mekanizması ile kararlarını geliştirir.</p>\r\n<p class=\"\" data-start=\"955\" data-end=\"1264\">Makine &ouml;ğrenmesi, g&uuml;n&uuml;m&uuml;zde finans, sağlık, e-ticaret, g&uuml;venlik ve daha bir&ccedil;ok alanda yaygın şekilde kullanılmaktadır. &Ouml;rneğin, &ouml;neri sistemleri, sahtecilik tespiti, m&uuml;şteri segmentasyonu veya otomatik metin sınıflandırma gibi uygulamalar, bu teknolojinin g&uuml;nl&uuml;k hayattaki karşılıklarına &ouml;rnek g&ouml;sterilebilir.</p>\r\n<p class=\"\" data-start=\"1266\" data-end=\"1627\">Bu eğitimin amacı, temel makine &ouml;ğrenmesi kavramlarını tanıtmak, yaygın algoritmaları tanımak ve ger&ccedil;ek veri setleri &uuml;zerinde uygulamalar yaparak katılımcıların problem &ccedil;&ouml;zme yetkinliğini artırmaktır. Ayrıca Python programlama dili ve scikit-learn k&uuml;t&uuml;phanesi gibi ara&ccedil;larla model oluşturma ve değerlendirme pratikleri de bu giriş s&uuml;recinin &ouml;nemli par&ccedil;alarıdır.</p>",
                            Description = "Makine öğrenmesi algoritmalarını öğrenin, model eğitimi ve değerlendirmesi hakkında bilgi edinin.",
                            Url = "makine-ogrenmesine-giris",
                            IsActive = true,
                            Image = "makine-ogrenmesi.jpg",
                            PublishedOn = DateTime.Now.AddDays(-60),
                            Categories = context.Categories.Where(t => t.Text.Contains("Makine Öğrenmesi")).Take(1).ToList(),
                            UserId = 3
                        },
                        new Post
                        {
                            Title = "Yeni Başlayanlar İçin Oyun Geliştirme",
                            Content = "<p class=\"\" data-start=\"116\" data-end=\"539\"><strong data-start=\"116\" data-end=\"157\">Yeni Başlayanlar İ&ccedil;in Oyun Geliştirme</strong>, hem teknik hem de yaratıcı y&ouml;nleri bir arada barındıran heyecan verici bir &ouml;ğrenme s&uuml;recidir. Oyun geliştirme, sadece kodlama bilgisi değil; aynı zamanda tasarım, hik&acirc;ye anlatımı ve kullanıcı deneyimi gibi pek &ccedil;ok becerinin birleşimini gerektirir. Bu alan, programlamaya yeni başlayanların &ouml;ğrenmesini eğlenceli h&acirc;le getirdiği gibi, &uuml;retici y&ouml;nlerini de ortaya koymalarını sağlar.</p>\r\n<p class=\"\" data-start=\"541\" data-end=\"937\">Giriş seviyesinde bir oyun geliştiricisi, genellikle <strong data-start=\"594\" data-end=\"603\">Unity</strong> (C#) veya <strong data-start=\"614\" data-end=\"623\">Godot</strong> (GDScript veya C#) gibi kullanıcı dostu oyun motorlarını tercih eder. Bu ara&ccedil;lar sayesinde temel oyun mekaniği, karakter hareketleri, skor sistemleri ve sahne ge&ccedil;işleri gibi temel yapılar kolaylıkla inşa edilebilir. G&ouml;rsel ve ses &ouml;ğeleri ile desteklenen projeler, oyunların daha ilgi &ccedil;ekici h&acirc;le gelmesini sağlar.</p>\r\n<p class=\"\" data-start=\"939\" data-end=\"1350\">Eğitim s&uuml;recinde, katılımcılar &ouml;nce 2D oyunlar &uuml;zerinden mantığı kavrar, ardından isterlerse 3D oyunlara ge&ccedil;iş yapabilirler. &ldquo;Top topla&rdquo; oyunları, sonsuz koşu oyunları veya platform tarzı oyunlar, bu s&uuml;re&ccedil;te yapılan ilk projelere &ouml;rnek olarak g&ouml;sterilebilir. Oyun geliştirmenin en g&uuml;zel yanı ise, yapılan her k&uuml;&ccedil;&uuml;k değişikliğin anında g&ouml;r&uuml;lebilmesidir; bu da &ouml;ğrencilerde hızlı bir motivasyon kaynağı oluşturur.</p>\r\n<p class=\"\" data-start=\"1352\" data-end=\"1654\">Bu alana adım atmak isteyenler i&ccedil;in temel programlama bilgisi, algoritmik d&uuml;ş&uuml;nme ve bolca deneme-yanılma s&uuml;reci olduk&ccedil;a faydalıdır. Yeni başlayanlar i&ccedil;in hazırlanan bu oyun geliştirme eğitimi, katılımcıların kendi fikirlerini dijital d&uuml;nyaya taşıyabilmeleri i&ccedil;in sağlam bir temel oluşturmayı hedefler.</p>",
                            Description = "Unity ile basit 2D oyunlar yapın ve oyun döngüleri ile fizik hakkında bilgi edinin.",
                            Url = "oyun-gelistirme-begeni",
                            IsActive = true,
                            Image = "oyun-gelistirme.jpg",
                            PublishedOn = DateTime.Now.AddDays(-75),
                            Categories = context.Categories.Where(t => t.Text.Contains("Oyun")).Take(1).ToList(),
                            UserId = 4
                        },
                        new Post
                        {
                            Title = "Yapay Zeka'nın Geleceği",
                            Content = "<p class=\"\" data-start=\"118\" data-end=\"431\"><strong data-start=\"118\" data-end=\"145\">Yapay Zeka&rsquo;nın Geleceği</strong>, teknolojinin şekil verdiği yeni d&uuml;nyada hem heyecan verici hem de merak uyandıran bir konudur. G&uuml;n&uuml;m&uuml;zde sesli asistanlar, &ouml;neri sistemleri ve otomasyon ara&ccedil;ları gibi alanlarda aktif olarak kullanılan yapay zeka, gelecekte yaşamın her alanında &ccedil;ok daha belirleyici bir rol &uuml;stlenecek.</p>\r\n<p class=\"\" data-start=\"433\" data-end=\"844\">Sağlık alanında erken teşhis sistemlerinden, otonom ara&ccedil;lara kadar uzanan geniş bir yelpazede AI (Artificial Intelligence) &ccedil;&ouml;z&uuml;mleri hayatımıza entegre olmaya devam ediyor. Eğitimden finansa, tarımdan g&uuml;venliğe kadar hemen her sekt&ouml;rde yapay zek&acirc; destekli sistemlerin verimliliği artırması bekleniyor. Bununla birlikte, AI'ın insan iş g&uuml;c&uuml;ne olan etkisi ve etik sınırları da tartışmaların merkezinde yer alıyor.</p>\r\n<p class=\"\" data-start=\"846\" data-end=\"1119\">Gelecekte yapay zekanın daha otonom, &ouml;ğrenme yeteneği daha g&uuml;&ccedil;l&uuml; ve karar mekanizmalarında daha etkili olması &ouml;ng&ouml;r&uuml;l&uuml;yor. Bu s&uuml;re&ccedil;te, insan ve makine arasındaki etkileşim daha da derinleşecek ve bu iş birliği, veriye dayalı karar alma mekanizmalarını yeniden tanımlayacak.</p>\r\n<p class=\"\" data-start=\"1121\" data-end=\"1458\">Yapay zek&acirc;nın geleceği sadece teknolojiyle değil; aynı zamanda etik, hukuk ve toplumsal bilin&ccedil; ile şekillenecek. Bu nedenle, sadece yazılım geliştiricilerin değil, t&uuml;m bireylerin yapay zek&acirc; okuryazarlığı kazanması b&uuml;y&uuml;k &ouml;nem taşıyor. Geleceğe hazır olmak i&ccedil;in yapay zekayı anlamak, sadece bir tercih değil; bir gereklilik h&acirc;line geliyor.</p>",
                            Description = "Yapay zekanın sağlık, finans ve ulaşım gibi sektörlere etkilerini keşfedin.",
                            Url = "yapay-zeka-gelegi",
                            IsActive = true,
                            Image = "yapay-zeka.jpg",
                            PublishedOn = DateTime.Now.AddDays(-90),
                            Categories = context.Categories.Where(t => t.Text.Contains("Yapay Zeka")).Take(1).ToList(),
                            UserId = 5
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
