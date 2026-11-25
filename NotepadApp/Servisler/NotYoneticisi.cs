using NotepadApp.Depolama;
using NotepadApp.Modeller;

namespace NotepadApp.Servisler
{
    // Notların listesini yönetir; her ekleme/güncelleme/silmeden sonra depoya otomatik kaydeder
    public class NotYoneticisi
    {
        private readonly INotDeposu depo;
        private List<Not> notlar = new List<Not>();

        public bool OtomatikKaydet { get; set; } = true;
        public DateTime? SonKayitZamani { get; private set; }

        public NotYoneticisi(INotDeposu depo)
        {
            this.depo = depo;
        }

        public string DosyaKonumu
        {
            get { return depo.Konum; }
        }

        public IReadOnlyList<Not> Notlar
        {
            get { return notlar; }
        }

        public void Yukle()
        {
            notlar = depo.Yukle();
        }

        public void Kaydet()
        {
            depo.Kaydet(notlar);
            SonKayitZamani = DateTime.Now;
        }

        public Not Ekle(string baslik, string icerik, string kategori)
        {
            Not yeni = new Not(baslik, icerik, kategori); // başlık geçersizse ArgumentException
            notlar.Add(yeni);
            DegisiklikSonrasi();
            return yeni;
        }

        public void Guncelle(Guid id, string baslik, string icerik, string kategori)
        {
            Not not = Bul(id);
            not.Guncelle(baslik, icerik, kategori);
            DegisiklikSonrasi();
        }

        public void Sil(Guid id)
        {
            notlar.Remove(Bul(id));
            DegisiklikSonrasi();
        }

        // Arama ve kategori filtresi; en son düzenlenen not en üstte
        public List<Not> Filtrele(string aranan, string? kategori)
        {
            List<Not> sonuc = new List<Not>();
            foreach (Not not in notlar)
            {
                bool kategoriUygun = string.IsNullOrEmpty(kategori) || not.Kategori == kategori;
                if (kategoriUygun && not.Iceriyor(aranan))
                    sonuc.Add(not);
            }
            sonuc.Sort((a, b) => b.DuzenlemeTarihi.CompareTo(a.DuzenlemeTarihi));
            return sonuc;
        }

        public List<string> Kategoriler()
        {
            SortedSet<string> kume = new SortedSet<string>(StringComparer.CurrentCulture);
            foreach (Not not in notlar)
                kume.Add(not.Kategori);
            return kume.ToList();
        }

        private Not Bul(Guid id)
        {
            Not? not = notlar.Find(n => n.Id == id);
            if (not == null)
                throw new KeyNotFoundException("Not bulunamadı; silinmiş olabilir.");
            return not;
        }

        private void DegisiklikSonrasi()
        {
            if (OtomatikKaydet)
                Kaydet();
        }
    }
}
