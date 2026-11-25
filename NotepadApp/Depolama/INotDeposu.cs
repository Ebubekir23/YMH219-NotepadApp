using NotepadApp.Modeller;

namespace NotepadApp.Depolama
{
    // Notların nereye ve hangi biçimde kaydedileceğini soyutlar (JSON, XML, veritabanı...)
    public interface INotDeposu
    {
        string Konum { get; }
        void Kaydet(IReadOnlyList<Not> notlar);
        List<Not> Yukle();
    }
}
