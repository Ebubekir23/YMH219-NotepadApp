using System.Text;
using NotepadApp.Modeller;

namespace NotepadApp.Servisler
{
    // Bir notu düz metin (.txt) dosyasına yazar
    public static class TxtDisaAktarici
    {
        public static void DisaAktar(Not not, string dosyaYolu)
        {
            using (StreamWriter yazici = new StreamWriter(dosyaYolu, false, new UTF8Encoding(true)))
            {
                yazici.WriteLine(not.Baslik);
                yazici.WriteLine(new string('=', Math.Max(10, not.Baslik.Length)));
                yazici.WriteLine($"Kategori       : {not.Kategori}");
                yazici.WriteLine($"Oluşturulma    : {not.OlusturmaTarihi:dd.MM.yyyy HH:mm}");
                yazici.WriteLine($"Son düzenleme  : {not.DuzenlemeTarihi:dd.MM.yyyy HH:mm}");
                yazici.WriteLine();
                yazici.Write(not.Icerik);
            }
        }

        // Başlıktan dosya adında kullanılamayan karakterleri temizleyerek önerilen ad üretir
        public static string OnerilenDosyaAdi(Not not)
        {
            string ad = not.Baslik;
            foreach (char c in Path.GetInvalidFileNameChars())
                ad = ad.Replace(c, '_');
            return ad + ".txt";
        }
    }
}
