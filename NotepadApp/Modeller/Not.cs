namespace NotepadApp.Modeller
{
    // Tek bir not. JSON'a bu sınıfın özellikleri yazılır.
    public class Not
    {
        public const int MaksBaslikUzunlugu = 100;

        private string baslik = "";

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Baslik
        {
            get { return baslik; }
            set
            {
                string temiz = (value ?? "").Trim();
                if (temiz.Length == 0)
                    throw new ArgumentException("Not başlığı boş olamaz.");
                if (temiz.Length > MaksBaslikUzunlugu)
                    throw new ArgumentException($"Not başlığı en fazla {MaksBaslikUzunlugu} karakter olabilir.");
                baslik = temiz;
            }
        }

        public string Icerik { get; set; } = "";
        public string Kategori { get; set; } = "Genel";
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlemeTarihi { get; set; } = DateTime.Now;

        // JSON'dan okurken boş yapıcı gerekir
        public Not()
        {
        }

        public Not(string baslik, string icerik, string kategori)
        {
            Baslik = baslik;
            Icerik = icerik;
            Kategori = KategoriDuzenle(kategori);
        }

        public void Guncelle(string yeniBaslik, string yeniIcerik, string yeniKategori)
        {
            Baslik = yeniBaslik;
            Icerik = yeniIcerik;
            Kategori = KategoriDuzenle(yeniKategori);
            DuzenlemeTarihi = DateTime.Now;
        }

        // Arama: başlıkta, içerikte veya kategoride geçiyorsa (büyük/küçük harf fark etmez)
        public bool Iceriyor(string aranan)
        {
            if (string.IsNullOrWhiteSpace(aranan))
                return true;

            StringComparison karsilastirma = StringComparison.CurrentCultureIgnoreCase;
            return Baslik.Contains(aranan, karsilastirma)
                || Icerik.Contains(aranan, karsilastirma)
                || Kategori.Contains(aranan, karsilastirma);
        }

        public static string KategoriDuzenle(string? kategori)
        {
            string temiz = (kategori ?? "").Trim();
            return temiz.Length == 0 ? "Genel" : temiz;
        }

        // Listede görünen metin
        public override string ToString()
        {
            return $"{Baslik}   [{Kategori}]  -  {DuzenlemeTarihi:dd.MM.yyyy HH:mm}";
        }
    }
}
