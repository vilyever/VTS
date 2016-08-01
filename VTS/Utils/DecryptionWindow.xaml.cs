using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTS.Utils
{
    /// <summary>
    /// DecryptionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DecryptionWindow : Window
    {
        public DecryptionWindow()
        {
            InitializeComponent();
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = inputTextBox.Text;
            switch (inputTypeComboBox.SelectedIndex)
            {
                case 0:
                    int n;
                    bool isNumeric = int.TryParse(input, out n);
                    if (isNumeric)
                    {
                        resultTextBlock.Text = ByteArrayToString(decriptInt(n));
                    }
                    else
                    {
                        resultTextBlock.Text = "Value Not Int";
                    }
                    break;
                case 1:
                    resultTextBlock.Text = ByteArrayToString(decriptHex(HexStringToByteArray(input)));
                    break;
                case 2:
                    resultTextBlock.Text = ByteArrayToString(decriptString(input));
                    break;
            }
        }

        private byte[] decriptInt(int input)
        {
            int result = input ^ 0xAD;
            byte[] bytes = BitConverter.GetBytes(result);
            return bytes;
        }

        private byte[] decriptHex(byte[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = (byte) (input[i] ^ 0xAD);
            }
            return input;
        }

        private byte[] decriptString(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            for (int i = 0; i < inputBytes.Length; i++)
            {
                inputBytes[i] = (byte)(inputBytes[i] ^ 0xAD);
            }
            return inputBytes;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
