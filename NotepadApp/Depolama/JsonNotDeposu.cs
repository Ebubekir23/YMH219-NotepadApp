using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using NotepadApp.Modeller;

namespace NotepadApp.Depolama
{
    // Notları System.Text.Json ile tek bir JSON dosyasına serileştirir
    public class JsonNotDeposu : INotDeposu
    {
        private static readonly JsonSerializerOptions jsonAyarlari = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Türkçe karakterler dosyada okunur kalsın
        };

        public string Konum { get; }

        public JsonNotDeposu(string dosyaYolu)
        {
            Konum = dosyaYolu;
        }

        // Varsayılan konum: %AppData%\YMH219-NotepadApp\notlar.json
        public static JsonNotDeposu Varsayilan()
        {
            string klasor = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YMH219-NotepadApp");
            return new JsonNotDeposu(Path.Combine(klasor, "notlar.json"));
        }

        public void Kaydet(IReadOnlyList<Not> notlar)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Konum)!);

            // Önce geçici dosyaya yazılır, sonra asıl dosyanın yerine konur.
            // Yazma yarıda kesilirse eski notlar bozulmaz.
            string gecici = Konum + ".tmp";
            using (StreamWriter yazici = new StreamWriter(gecici, false, new UTF8Encoding(false)))
                yazici.Write(JsonSerializer.Serialize(notlar, jsonAyarlari));

            File.Move(gecici, Konum, overwrite: true);
        }

        // Dosya yoksa boş liste döner (ilk açılış). Dosya bozuksa yedeğini alıp BozukDosyaException fırlatır.
        public List<Not> Yukle()
        {
            if (!File.Exists(Konum))
                return new List<Not>();

            string json;
            using (StreamReader okuyucu = new StreamReader(Konum, Encoding.UTF8))
                json = okuyucu.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
                return new List<Not>();

            try
            {
                List<Not>? notlar = JsonSerializer.Deserialize<List<Not>>(json, jsonAyarlari);
                return notlar ?? new List<Not>();
            }
            catch (Exception hata) when (hata is JsonException || hata is ArgumentException)
            {
                // Bozuk dosyanın üzerine yazılıp veri kaybolmasın diye kopyası saklanır
                string yedek = Path.Combine(Path.GetDirectoryName(Konum)!, $"notlar_bozuk_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                File.Copy(Konum, yedek, overwrite: true);
                throw new BozukDosyaException($"Not dosyası okunamadı (geçersiz JSON).\nBozuk dosyanın yedeği alındı:\n{yedek}", yedek, hata);
            }
        }
    }
}
