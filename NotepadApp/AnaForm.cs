using System.Diagnostics;
using NotepadApp.Depolama;
using NotepadApp.Modeller;
using NotepadApp.Servisler;

namespace NotepadApp
{
    public partial class AnaForm : Form
    {
        private const string TumKategoriler = "(Tümü)";

        private readonly NotYoneticisi yonetici = new NotYoneticisi(JsonNotDeposu.Varsayilan());

        private Guid? seciliId;          // düzenleyicide açık not; null ise yeni not yazılıyor
        private bool kaydedilmemis;      // düzenleyicide kaydedilmemiş değişiklik var mı
        private bool programDolduruyor;  // alanlar kodla doldurulurken "değişti" sayılmasın

        public AnaForm()
        {
            InitializeComponent();
        }

        private void AnaForm_Load(object? sender, EventArgs e)
        {
            NotlariDosyadanYukle();
            YeniNotBaslat();
        }

        // ---------- Yükleme ----------

        private void NotlariDosyadanYukle()
        {
            try
            {
                yonetici.Yukle();
                lblKayitDurumu.Text = $"{yonetici.Notlar.Count} not yüklendi";
            }
            catch (BozukDosyaException hata)
            {
                // Bozuk dosyanın yedeği alındı; uygulama boş listeyle devam eder
                MessageBox.Show(this, hata.Message + "\n\nUygulama boş bir not listesiyle açılıyor.", "Not dosyası bozuk",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblKayitDurumu.Text = "Not dosyası bozuktu, yedeklendi";
            }
            catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
            {
                HataGoster($"Not dosyası okunamadı:\n{hata.Message}");
                lblKayitDurumu.Text = "Not dosyası okunamadı";
            }

            lblDosya.Text = KisaYol(yonetici.DosyaKonumu);
            ListeyiYenile();
        }

        private void mnuYenidenYukle_Click(object? sender, EventArgs e)
        {
            if (!DegisiklikleriSor())
                return;

            NotlariDosyadanYukle();
            YeniNotBaslat();
        }

        // ---------- Liste, arama ve filtre ----------

        private void ListeyiYenile()
        {
            string? kategori = cmbFiltre.SelectedItem as string;
            if (kategori == TumKategoriler)
                kategori = null;

            List<Not> gosterilecek = yonetici.Filtrele(txtAra.Text.Trim(), kategori);

            programDolduruyor = true;
            lstNotlar.BeginUpdate();
            lstNotlar.Items.Clear();
            foreach (Not not in gosterilecek)
            {
                lstNotlar.Items.Add(not);
                if (not.Id == seciliId)
                    lstNotlar.SelectedItem = not;
            }
            lstNotlar.EndUpdate();
            KategorileriYenile();
            programDolduruyor = false;

            lblNotSayisi.Text = gosterilecek.Count == yonetici.Notlar.Count
                ? $"{yonetici.Notlar.Count} not"
                : $"{gosterilecek.Count} / {yonetici.Notlar.Count} not";
        }

        private void KategorileriYenile()
        {
            List<string> kategoriler = yonetici.Kategoriler();

            object? oncekiFiltre = cmbFiltre.SelectedItem;
            cmbFiltre.Items.Clear();
            cmbFiltre.Items.Add(TumKategoriler);
            foreach (string k in kategoriler)
                cmbFiltre.Items.Add(k);
            cmbFiltre.SelectedItem = oncekiFiltre != null && cmbFiltre.Items.Contains(oncekiFiltre) ? oncekiFiltre : TumKategoriler;

            string yazilan = cmbKategori.Text;
            cmbKategori.Items.Clear();
            foreach (string k in kategoriler)
                cmbKategori.Items.Add(k);
            cmbKategori.Text = yazilan;
        }

        private void Filtre_Degisti(object? sender, EventArgs e)
        {
            if (!programDolduruyor)
                ListeyiYenile();
        }

        private void lstNotlar_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (programDolduruyor || lstNotlar.SelectedItem is not Not secilen || secilen.Id == seciliId)
                return;

            if (!DegisiklikleriSor())
            {
                ListeyiYenile(); // seçim geri alınır
                return;
            }
            NotuGoster(secilen);
        }

        // ---------- Düzenleyici ----------

        private void NotuGoster(Not not)
        {
            programDolduruyor = true;
            seciliId = not.Id;
            txtBaslik.Text = not.Baslik;
            cmbKategori.Text = not.Kategori;
            rtbIcerik.Text = not.Icerik;
            lblTarihler.Text = $"Oluşturma: {not.OlusturmaTarihi:dd.MM.yyyy HH:mm}   Düzenleme: {not.DuzenlemeTarihi:dd.MM.yyyy HH:mm}";
            programDolduruyor = false;
            KirliAyarla(false);
        }

        private void YeniNotBaslat()
        {
            programDolduruyor = true;
            seciliId = null;
            txtBaslik.Clear();
            cmbKategori.Text = "Genel";
            rtbIcerik.Clear();
            lblTarihler.Text = "Yeni not";
            lstNotlar.ClearSelected();
            programDolduruyor = false;
            KirliAyarla(false);
            txtBaslik.Focus();
        }

        private void Duzenleyici_Degisti(object? sender, EventArgs e)
        {
            if (!programDolduruyor)
                KirliAyarla(true);
        }

        private void KirliAyarla(bool deger)
        {
            kaydedilmemis = deger;
            Text = (seciliId == null ? "Yeni not" : txtBaslik.Text) + (kaydedilmemis ? " *" : "") + " - Not Defteri";
            btnSil.Enabled = seciliId != null;
            btnDisaAktar.Enabled = seciliId != null;
            mnuDisaAktar.Enabled = seciliId != null;
        }

        // ---------- İşlemler ----------

        private void btnYeni_Click(object? sender, EventArgs e)
        {
            if (DegisiklikleriSor())
                YeniNotBaslat();
        }

        private void btnKaydet_Click(object? sender, EventArgs e)
        {
            NotuKaydet();
        }

        // Başarılıysa true döner
        private bool NotuKaydet()
        {
            try
            {
                if (seciliId == null)
                {
                    Not yeni = yonetici.Ekle(txtBaslik.Text, rtbIcerik.Text, cmbKategori.Text);
                    seciliId = yeni.Id;
                }
                else
                {
                    yonetici.Guncelle(seciliId.Value, txtBaslik.Text, rtbIcerik.Text, cmbKategori.Text);
                }
            }
            catch (ArgumentException hata)
            {
                MessageBox.Show(this, hata.Message, "Eksik bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBaslik.Focus();
                return false;
            }
            catch (KeyNotFoundException hata)
            {
                HataGoster(hata.Message);
                YeniNotBaslat();
                ListeyiYenile();
                return false;
            }
            catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
            {
                // Not bellekte güncellendi ama dosyaya yazılamadı
                ListeyiYenile();
                HataGoster($"Not dosyaya kaydedilemedi:\n{hata.Message}");
                return false;
            }

            ListeyiYenile();
            Not kaydedilen = yonetici.Notlar.First(n => n.Id == seciliId);
            NotuGoster(kaydedilen);
            lblKayitDurumu.Text = $"Kaydedildi: {yonetici.SonKayitZamani:HH:mm:ss}";
            return true;
        }

        private void btnSil_Click(object? sender, EventArgs e)
        {
            if (seciliId == null)
                return;

            DialogResult cevap = MessageBox.Show(this, $"\"{txtBaslik.Text}\" notu silinsin mi? Bu işlem geri alınamaz.",
                "Notu sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (cevap != DialogResult.Yes)
                return;

            try
            {
                yonetici.Sil(seciliId.Value);
                lblKayitDurumu.Text = $"Not silindi: {yonetici.SonKayitZamani:HH:mm:ss}";
            }
            catch (KeyNotFoundException hata)
            {
                HataGoster(hata.Message);
            }
            catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
            {
                HataGoster($"Silme dosyaya kaydedilemedi:\n{hata.Message}");
            }

            YeniNotBaslat();
            ListeyiYenile();
        }

        private void btnDisaAktar_Click(object? sender, EventArgs e)
        {
            if (seciliId == null)
                return;

            // Dışa aktarılan dosya ile ekrandaki not aynı olsun diye önce kaydedilir
            if (kaydedilmemis && !NotuKaydet())
                return;

            Not not = yonetici.Notlar.First(n => n.Id == seciliId);
            using (SaveFileDialog pencere = new SaveFileDialog())
            {
                pencere.Filter = "Metin dosyası (*.txt)|*.txt";
                pencere.FileName = TxtDisaAktarici.OnerilenDosyaAdi(not);
                if (pencere.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    TxtDisaAktarici.DisaAktar(not, pencere.FileName);
                    lblKayitDurumu.Text = $"Dışa aktarıldı: {Path.GetFileName(pencere.FileName)}";
                }
                catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
                {
                    HataGoster($"Dosya yazılamadı:\n{hata.Message}");
                }
            }
        }

        private void mnuKonumuAc_Click(object? sender, EventArgs e)
        {
            string dosya = yonetici.DosyaKonumu;
            if (File.Exists(dosya))
                Process.Start("explorer.exe", $"/select,\"{dosya}\"");
            else
                MessageBox.Show(this, $"Not dosyası henüz oluşturulmadı. İlk not kaydedildiğinde şurada oluşacak:\n{dosya}",
                    "Not dosyası", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuCikis_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void AnaForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!DegisiklikleriSor())
                e.Cancel = true;
        }

        // Kaydedilmemiş değişiklik varsa sorar. Devam edilebilirse true döner.
        private bool DegisiklikleriSor()
        {
            if (!kaydedilmemis)
                return true;

            DialogResult cevap = MessageBox.Show(this, "Bu notta kaydedilmemiş değişiklikler var. Kaydedilsin mi?",
                "Kaydedilmemiş değişiklik", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
                return NotuKaydet();
            return cevap == DialogResult.No;
        }

        // Durum çubuğunda kullanıcı klasörü yerine %AppData% gösterilir
        private static string KisaYol(string yol)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return yol.StartsWith(appData, StringComparison.OrdinalIgnoreCase) ? "%AppData%" + yol.Substring(appData.Length) : yol;
        }

        private void HataGoster(string mesaj)
        {
            MessageBox.Show(this, mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
