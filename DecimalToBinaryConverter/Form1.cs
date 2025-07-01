using System;
using System.Drawing;
using System.Windows.Forms;

namespace DecimalToBinaryConverter
{
    public partial class Form1 : Form
    {
        private bool altFunctionEnabled = false;

        public Form1()
        {
            InitializeComponent();

            decimalTextBox.KeyPress += new KeyPressEventHandler(decimalTextBox_KeyPress);
            decimalTextBox.TextChanged += new EventHandler(decimalTextBox_TextChanged);
            this.decimalTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.decimalTextBox_KeyDown);
            utf8CheckBox.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            utf8CheckBox.Checked = false;

            altFunctionCheckBox.CheckedChanged += new EventHandler(altFunctionCheckBox_CheckedChanged);
        }

        private void decimalTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateBinaryTextBox();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (utf8CheckBox.Checked)
            {
                altFunctionCheckBox.Checked = false;
            }
            decimalTextBox.Clear();
            UpdateBinaryTextBox();
        }

        private void altFunctionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (altFunctionCheckBox.Checked)
            {
                utf8CheckBox.Checked = false;
            }
            altFunctionEnabled = altFunctionCheckBox.Checked;
            decimalTextBox.Clear();
            UpdateBinaryTextBox();
        }

        private void decimalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (utf8CheckBox.Checked && decimalTextBox.Text.Length > 0)
                {
                    try
                    {
                        string lastChar = decimalTextBox.Text.Substring(decimalTextBox.Text.Length - 1);
                        if (char.IsHighSurrogate(lastChar[0]))
                        {
                            decimalTextBox.Text = decimalTextBox.Text.Remove(decimalTextBox.Text.Length - 2);
                            e.Handled = true;
                        }
                    }
                    catch (Exception)
                    {
                        // Handle exceptions
                    }
                }
            }

            if (e.Control && e.Shift)
            {
                utf8CheckBox.Checked = !utf8CheckBox.Checked;
                if (utf8CheckBox.Checked)
                {
                    altFunctionCheckBox.Checked = false;
                }
                e.Handled = true;
            }

            if (e.Control && e.Alt)
            {
                altFunctionCheckBox.Checked = !altFunctionCheckBox.Checked;
                if (altFunctionCheckBox.Checked)
                {
                    utf8CheckBox.Checked = false;
                }
                e.Handled = true;
            }
        }

        private void decimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (utf8CheckBox.Checked)
            {
                // UTF-8 Mode
                if (!char.IsControl(e.KeyChar) &&
                    !((decimalTextBox.Text.Length == 0 && e.KeyChar != '+') ||
                      (decimalTextBox.Text.Length == 1 && decimalTextBox.Text == "U" && e.KeyChar == '+') ||
                      (decimalTextBox.Text.StartsWith("U+") && IsHexDigit(e.KeyChar))))
                {
                    e.Handled = true;
                }

                if ((decimalTextBox.Text.Length >= 6 && decimalTextBox.Text.StartsWith("U+")) ||
                    (decimalTextBox.Text.Length >= 2 && !decimalTextBox.Text.StartsWith("U") && !char.IsControl(e.KeyChar)))
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (altFunctionEnabled)
            {
                // AltFunction Mode (Binary to Decimal)
                if (!char.IsControl(e.KeyChar) && e.KeyChar != '0' && e.KeyChar != '1')
                {
                    e.Handled = true;
                }

                if (decimalTextBox.Text.Length >= 8 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else
            {
                // Standard ASCII Mode with restrictions
                if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !(e.KeyChar == '+' && decimalTextBox.Text.StartsWith("ILY")))
                {
                    e.Handled = true;
                }

                // Number range restriction
                if (decimalTextBox.Text.Length > 0 && char.IsDigit(e.KeyChar))
                {
                    string newText = decimalTextBox.Text + e.KeyChar;
                    int number;
                    if (int.TryParse(newText, out number))
                    {
                        if ((decimalTextBox.Text.StartsWith("0") && (number < 0 || number > 99)) ||
                            (!decimalTextBox.Text.StartsWith("0") && (number < 1 || number > 255)))
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        // Handle parsing error (e.g., number too large)
                        e.Handled = true;
                    }
                }

                // Allow only one alphabetical character UNLESS it's part of the Easter egg
                if (char.IsLetter(e.KeyChar) && decimalTextBox.Text.Length > 0 && !char.IsControl(e.KeyChar)
                    && !((decimalTextBox.Text == "I" && e.KeyChar == 'L') || (decimalTextBox.Text == "IL" && e.KeyChar == 'Y')))
                {
                    e.Handled = true;
                }

                // ... (rest of your standard ASCII mode logic, INCLUDING the Easter egg handling) ...
            }

            // ... (your existing code for handling highlighted text) ...
        }

        private void UpdateBinaryTextBox()
        {
            if (decimalTextBox.Text.Length > 0)
            {
                try
                {
                    string binaryString = "";
                    float originalFontSize = binaryTextBox.Font.Size;

                    if (utf8CheckBox.Checked)
                    {
                        // UTF-8 conversion logic... 
                        if (decimalTextBox.Text == "U+")
                        {
                            binaryString = "reverse";
                        }
                        else if (decimalTextBox.Text.StartsWith("U+"))
                        {
                            try
                            {
                                string hexString = decimalTextBox.Text.Substring(2);
                                int charCode = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
                                binaryString = char.ConvertFromUtf32(charCode);
                            }
                            catch (Exception)
                            {
                                binaryString = "Invalid Codepoint";
                            }
                        }
                        else
                        {
                            int charCode = char.ConvertToUtf32(decimalTextBox.Text, 0);
                            binaryString = "U+" + charCode.ToString("X4");
                        }
                    }
                    else if (altFunctionEnabled)
                    {
                        // AltFunction (Binary to Decimal) conversion logic...
                        if (decimalTextBox.Text.Length == 8)
                        {
                            try
                            {
                                binaryString = Convert.ToInt32(decimalTextBox.Text, 2).ToString();
                            }
                            catch (Exception)
                            {
                                binaryString = "Invalid Binary Input";
                            }
                        }
                    }
                    else
                    {
                        // Standard ASCII conversion logic...
                        if (decimalTextBox.Text == "IL")
                        {
                            binaryString = "Dear Miss";
                        }
                        else if (decimalTextBox.Text == "ILY")
                        {
                            binaryString = "Dreadful";
                        }
                        else if (decimalTextBox.Text == "ILY+")
                        {
                            binaryString = "ilyrachel";
                        }
                        else if (decimalTextBox.Text == "ILY++")
                        {
                            binaryString = "w/allmy<3";
                        }
                        else if (decimalTextBox.Text.StartsWith("ILY+++"))
                        {
                            binaryString = "R&J 4VR :)";
                        }
                        else if (decimalTextBox.Text.Length == 1 && char.IsLetter(decimalTextBox.Text[0]))
                        {
                            int asciiValue = (int)decimalTextBox.Text[0];
                            binaryString = asciiValue.ToString("D3");
                        }
                        else
                        {
                            int asciiCode = int.Parse(decimalTextBox.Text);
                            binaryString = Convert.ToString(asciiCode, 2).PadLeft(8, '0');
                        }
                    }

                    binaryTextBox.Text = binaryString;
                    binaryTextBox.Font = new Font(binaryTextBox.Font.FontFamily, originalFontSize);
                }
                catch (Exception)
                {
                    binaryTextBox.Text = "Invalid input";
                }
            }
            else
            {
                binaryTextBox.Text = "";
            }
        }

        private bool IsHexDigit(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }
    }
}