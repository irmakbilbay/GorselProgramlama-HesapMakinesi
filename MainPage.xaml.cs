using System;

namespace onaltimart
{
    public partial class MainPage : ContentPage
    {
        private string currentEntry = ""; // Kullanıcının girdiği sayılar
        private double firstNumber; // İlk sayı
        private string operation; // Matematiksel işlem (+, -, *, /, mod)

        public MainPage()
        {
            InitializeComponent();
        }

        // Sayı butonlarına tıklandığında ekranı güncelle
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            currentEntry += button.Text;
            Display.Text = currentEntry; // Girdiyi hemen ekrana yaz
        }

        // İşlem (örneğin +, -, *, /, mod) seçildiğinde
        private void OnOperationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (double.TryParse(currentEntry, out firstNumber))
            {
                operation = button.Text; // İşlem kaydedilir
                currentEntry = ""; // Girdi sıfırlanır
                Display.Text = ""; // Ekran temizlenir
            }
        }

        // İşlemi sonlandır ve sonucu göster
        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double secondNumber))
            {
                if (operation == "/" && secondNumber == 0) // Sıfıra bölme kontrolü
                {
                    Display.Text = "Hata: Sıfıra bölünemez!"; // Uyarı mesajı
                    currentEntry = ""; // Mevcut girdiyi sıfırla
                    return; // Hata nedeniyle işlemi sonlandır
                }

                double result = operation switch
                {
                    "+" => firstNumber + secondNumber,
                    "-" => firstNumber - secondNumber,
                    "*" => firstNumber * secondNumber,
                    "/" => firstNumber / secondNumber, // Sıfıra bölme kontrolü geçtiği için güvenli
                    "mod" => firstNumber % secondNumber,
                    _ => 0
                };

                Display.Text = $"{firstNumber} {operation} {secondNumber} = {result}"; // Sonucu göster
                currentEntry = ""; // Girdi sıfırlanır
                // displayText = ""; // Yeni işlem için geçmiş sıfırlanır
            }

        }

        // "C" butonu için: Her şeyi sıfırla
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentEntry = "";
            firstNumber = 0;
            operation = "";
            Display.Text = "";
        }

        // "CE" butonu için: Sadece mevcut girdiyi temizle
        private void OnClearEntryClicked(object sender, EventArgs e)
        {
            currentEntry = "";
            Display.Text = "";
        }

        // Silme (⌫) işlevi için: Son karakteri kaldır
        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (currentEntry.Length > 0)
            {
                currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
                Display.Text = currentEntry;
            }
        }

        // İşaret değiştirme (+/-) işlevi için
        private void OnChangeSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double number))
            {
                number = -number; // İşareti tersine çevirir
                currentEntry = number.ToString();
                Display.Text = currentEntry; // Değiştirilen işareti göster
            }
        }
    }
}
