namespace NotepadApp.Depolama
{
    // Not dosyası okunabildi ama içeriği geçerli JSON değil
    public class BozukDosyaException : Exception
    {
        public string YedekDosya { get; }

        public BozukDosyaException(string mesaj, string yedekDosya, Exception icHata)
            : base(mesaj, icHata)
        {
            YedekDosya = yedekDosya;
        }
    }
}
