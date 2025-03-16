using System;

namespace onaltimart
{
    public partial class ScientificPage : ContentPage
    {
        private string currentEntry = ""; // Kullanıcının girdiği sayı
        private string operation = ""; // Matematiksel işlem
        private double firstNumber; // İlk sayı
        private bool isWaitingForSecondNumber = false; // İkinci sayıyı bekleme durumu

        public ScientificPage()
        {
            InitializeComponent();
        }

        // Sayı butonlarına tıklandığında ekranı güncelle
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            currentEntry += button.Text; // Tıklanan sayıyı ekle
            Display.Text = currentEntry; // Ekranda göster
        }

        // İşlem butonuna tıklandığında çalışır
        private void OnOperationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (double.TryParse(currentEntry, out firstNumber)) // İlk sayıyı al
            {
                operation = button.Text; // İşlemi kaydet
                Display.Text = $"{firstNumber} {operation}"; // İşlemi ekrana yaz
                currentEntry = ""; // Mevcut girdiyi sıfırla
                isWaitingForSecondNumber = true; // İkinci sayıyı bekle
            }
        }

        // "=" butonuna tıklandığında sonucu hesapla
        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double secondNumber)) // İkinci sayıyı al
            {
                double result = 0;
                bool isError = false;

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            Display.Text = "Error: Div by 0"; // Division by zero error
                            isError = true;
                        }
                        else
                        {
                            result = firstNumber / secondNumber;
                        }
                        break;
                    case "mod":
                        result = firstNumber % secondNumber;
                        break;
                    case "x^y":
                        result = Math.Pow(firstNumber, secondNumber); // Exponentiation
                        break;
                    default:
                        isError = true;
                        break;
                }

                if (!isError)
                {
                    Display.Text = $"{firstNumber} {operation} {secondNumber} = {result}"; // Sonucu göster
                }
                else
                {
                    currentEntry = ""; // Reset if error
                }

                isWaitingForSecondNumber = false; // Reset waiting flag
            }
        }

        // Bilimsel işlemleri ele alan metod
        private void OnScientificOperationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string sciOperation = button.Text;

            if (double.TryParse(Display.Text, out double number))
            {
                double result = 0;
                bool isError = false;

                switch (sciOperation)
                {
                    case "2^x":
                        result = Math.Pow(2, number);
                        break;
                    case "x²":
                        result = Math.Pow(number, 2); // Kare alma
                        break;
                    case "karekok":
                        result = Math.Sqrt(number); // Karekök
                        break;
                    case "x^y":
                        firstNumber = number; // Üs alma için ilk sayıyı kaydet
                        Display.Text = $"{number}^"; // İkinci sayı bekleniyor
                        isWaitingForSecondNumber = true;
                        return; // Return early to wait for second number
                    case "10^x":
                        result = Math.Pow(10, number); // 10'un kuvveti
                        break;
                    case "log":
                        result = Math.Log10(number); // Logaritma (taban 10)
                        break;
                    case "ln":
                        result = Math.Log(number); // Doğal logaritma
                        break;
                    case "1/x":
                        result = 1 / number; // Tersi
                        break;
                    case "|x|":
                        result = Math.Abs(number); // Mutlak değer
                        break;
                    case "pi":
                        result = Math.PI; // Pi sayısı
                        break;
                    case "e":
                        result = Math.E; // e sayısı
                        break;
                    case "exp":
                        result = Math.Exp(number); // e^x
                        break;
                    case "!":
                        result = Factorial((int)number); // Faktöriyel hesaplama
                        break;
                    default:
                        isError = true;
                        break;
                }

                if (isError)
                {
                    Display.Text = "Error"; // Error in scientific operation
                }
                else
                {
                    Display.Text = result.ToString(); // Sonucu göster
                }
            }
        }

        // Faktöriyel hesaplama
        private double Factorial(int number)
        {
            if (number < 0)
                throw new ArgumentException("Faktöriyel için negatif sayı kullanılamaz!");

            double result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        // Girdiyi temizleme
        private void OnClearEntryClicked(object sender, EventArgs e)
        {
            currentEntry = ""; // Geçerli girdiyi sıfırla
            Display.Text = ""; // Ekranı temizle
        }

        // Her şeyi temizleme
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentEntry = "";
            firstNumber = 0;
            operation = "";
            Display.Text = ""; // Ekranı temizle
        }

        // Silme (?): Son karakteri kaldırma
        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (currentEntry.Length > 0)
            {
                currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
                Display.Text = currentEntry; // Güncel girdiyi ekrana yansıt
            }
        }

        // İşaret değiştirme
        private void OnChangeSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double number))
            {
                number = -number; // İşareti değiştir
                currentEntry = number.ToString();
                Display.Text = currentEntry;
            }
        }
    }
}
