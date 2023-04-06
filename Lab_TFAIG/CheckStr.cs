using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_TFAIG
{
    public class Checker
    {
        //public Checker(string str,System.Windows.Forms.TextBox textBox1, System.Windows.Forms.ListBox listBox1, System.Windows.Forms.ListBox listBox2, System.Windows.Forms.TextBox textBox2)
        //{
            
        //}
        enum states { S, A, B, C, D, E0, G, H, J, A14, B1, C1, D1, E1, F1, G1, A2, B2, C2, D2, E2, F2, A37, B37, A3, B3, C3, B4, C4, A5, B5, C5, D5, E5, F5, A6, B6, C6, D6, F6, H6, I60, I61, I62, J6, K6, L6, N60, N61, N62, O6, P6, A8, B8, C8, D8, E8, F8, G8, A9, B9, C9, D9, E9, F9, G9, H9, I9, A10, C10, D10, E10, F10, G10, A11, D11, E11, F0, D7, E7, F7, G7, H7, J7, K7, M7, A141, B19, C19, D19, E19, F19, G19, A21, B21, C21, D21, E21, F21, A31, B31, C31, B41, C41, A51, B51, C51, D51, E51, F51, A101, C101, D101, E101, F101, G101, A111, D111, E111, N8, N7, O7, P7, Q7, F, E }

        bool is_number(char ch)
        {
            return ch >= '1' && ch <= '9';
        }
        public bool check(string Str, System.Windows.Forms.TextBox textBox1, System.Windows.Forms.ListBox listBox1, System.Windows.Forms.ListBox listBox2, System.Windows.Forms.TextBox textBox2)
        {
            bool x = true;
            //char[] Z = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            //int[] Y = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            //int[] X = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            //char.IsDigit
            //UInt16.TryParse(...)
            Str = Str.ToUpper();
            states state = states.S;
            int n = 0;//Переменная индекса текущего символа
            string c = ""; //Переменная для длины id (не более 8 символов)
            string c2 = ""; //Перенос первого id в другую переменную для RECORD
            List<string> cm = new List<string>();//Список всех id для проверки на повторение
            string L = ""; //Случай с запятой для RECORD
            string g = ""; //Переменная для uint от 1 до 65535
            string f = ""; //Переменная для int от -32768 до 32767
            string f2 = "";//Переменная для int [], чтобы считать разность между границами, так же вывод правой границы в listbox2
            int i1 = 1;//Итоговый ответ для вывода размера байт
            int i2 = 0;//Переменная для pointer
            int i3 = 0;//Подсчет для ()
            int i4 = 0;//Подсчет для RECORD
            int i5 = 1;//Произведение границ []
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.Focus();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox2.Clear();
            while ((state != states.F) && (n < Str.Length))
            {
                switch (state)
                {
                    case states.S:
                        if (Str[n] == ' ')
                            state = states.S;
                        else if (Str[n] == 'T')
                            state = states.A;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.A:
                        if (Str[n] == 'Y')
                            state = states.B;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось Y";
                        }
                        break;
                    case states.B:
                        if (Str[n] == 'P')
                            state = states.C;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось P";
                        }
                        break;
                    case states.C:
                        if (Str[n] == 'E')
                            state = states.D;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.D:
                        if (Str[n] == ' ')
                            state = states.E0;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидался пробел";
                        }
                        break;
                    case states.E0:
                        if (Str[n] == ' ')
                            state = states.E0;
                        else if (Str[n] == '_' || Char.IsLetter(Str[n]))
                        {
                            state = states.G;
                            c = c + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в имени идентификатора";
                        }
                        break;
                    case states.G:
                        if (Char.IsLetter(Str[n]) || Char.IsDigit(Str[n]) || Str[n] == '_')
                        {
                            state = states.G;
                            c = c + Str[n];
                            if ((c.Length > 8) || (c == "TYPE") || (c == "CARDINAL") || (c == "INTEGER") || (c == "REAL") || (c == "CHAR") || (c == "BOOLEAN") || (c == "ARRAY") || (c == "OF") || (c == "RECORD") || (c == "END") || (c == "SET") || (c == "OF") || (c == "POINTER") || (c == "TO"))
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Идентификатор слишком длинный или является зарезервированным словом";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.H;
                            if (cm.Contains(c))
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Повторное использование идентификатора слева";
                            }
                        }
                        else if (Str[n] == '=')
                        {
                            state = states.J;
                            if (cm.Contains(c))
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text="Повторное использование идентификатора слева";
                            }
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в имени идентификатора";
                        }
                        break;
                    case states.H:
                        if (Str[n] == ' ')
                            state = states.H;
                        else if (Str[n] == '=')
                            state = states.J;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось =";
                        }
                        break;
                    case states.J:
                        if (Str[n] == ' ')
                            state = states.J;
                        else if (Str[n] == 'A')
                            state = states.A6;
                        else if (Str[n] == 'S')
                            state = states.A8;
                        else if (Str[n] == 'P')
                            state = states.A9;
                        else if (Str[n] == 'C')
                            state = states.A14;
                        else if (Str[n] == 'I')
                            state = states.A2;
                        else if (Str[n] == 'B')
                            state = states.A5;
                        else if (Str[n] == '[')
                            state = states.A10;
                        else if (Str[n] == '(')
                            state = states.A11;
                        else if (Str[n] == 'R')
                            state = states.A37;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.A6:
                        if (Str[n] == 'R')
                            state = states.B6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.B6:
                        if (Str[n] == 'R')
                            state = states.C6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.C6:
                        if (Str[n] == 'A')
                            state = states.D6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.D6:
                        if (Str[n] == 'Y')
                            state = states.F6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось Y";
                        }
                        break;
                    case states.F6:
                        if (Str[n] == ' ')
                            state = states.F6;
                        else if (Str[n] == '[')
                            state = states.H6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось [";
                        }
                        break;
                    case states.H6:
                        if (Str[n] == ' ')
                            state = states.H6;
                        else if (Str[n] == '-')
                        {
                            state = states.I60;
                            f = f + Str[n];
                        }
                        else if (is_number(Str[n]))
                        {
                            state = states.I61;
                            f = f + Str[n];
                        }
                        else if (Str[n] == '0')
                        {
                            state = states.I62;
                            f = "0";
                            listBox2.Items.Add(f);
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.I60:
                        if (is_number(Str[n]))
                        {
                            state = states.I61;
                            f = f + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.I61:
                        if (Char.IsDigit(Str[n]))
                        {
                            f = f + Str[n];
                            if ((Convert.ToInt32(f) < 32767) && (Convert.ToInt32(f) > -32768))
                            {
                                state = states.I61;
                            }
                            else
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.J6;
                            listBox2.Items.Add(f);
                        }
                        else if (Str[n] == '.')
                        {
                            state = states.K6;
                            listBox2.Items.Add(f);
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.I62:
                        if (Str[n] == ' ')
                            state = states.J6;
                        else if (Str[n] == '.')
                            state = states.K6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.J6:
                        if (Str[n] == ' ')
                            state = states.J6;
                        else if (Str[n] == '.')
                            state = states.K6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.K6:
                        if (Str[n] == '.')
                            state = states.L6;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.L6:
                        if (Str[n] == ' ')
                            state = states.L6;
                        else if (Str[n] == '-')
                        {
                            state = states.N60;
                            f2 = f2 + Str[n];
                        }
                        else if (is_number(Str[n]))
                        {
                            state = states.N61;
                            f2 = f2 + Str[n];
                        }
                        else if (Str[n] == '0')
                        {
                            state = states.N62;
                            f2 = "0";
                            listBox2.Items.Add(f2);
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.N60:
                        if (is_number(Str[n]))
                        {
                            state = states.N61;
                            f2 = f2 + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.N61:
                        if (Char.IsDigit(Str[n]))
                        {
                            f2 = f2 + Str[n];
                            if ((Convert.ToInt32(f2) < 32767) && (Convert.ToInt32(f2) > -32768))
                            {
                                state = states.N61;
                            }
                            else
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.O6;
                            listBox2.Items.Add(f2);
                        }
                        else if (Str[n] == ']')
                        {
                            state = states.P6;
                            listBox2.Items.Add(f2);
                            i5 = i5 * (Convert.ToInt32(f2) - Convert.ToInt32(f) + 1);
                            f = "";
                            f2 = "";
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.N62:
                        if (Str[n] == ' ')
                            state = states.O6;
                        else if (Str[n] == ']')
                        {
                            state = states.P6;
                            listBox2.Items.Add(f2);
                            i5 = i5 * (Convert.ToInt32(f2) - Convert.ToInt32(f) + 1);
                            f = "";
                            f2 = "";
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.O6:
                        if (Str[n] == ' ')
                            state = states.O6;
                        else if (Str[n] == ']')
                        {
                            state = states.P6;
                            listBox2.Items.Add(f2);
                            i5 = i5 * (Convert.ToInt32(f2) - Convert.ToInt32(f) + 1);
                            f = "";
                            f2 = "";
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.P6:
                        if (Str[n] == ' ')
                            state = states.P6;
                        else if (Str[n] == ',')
                            state = states.F6;
                        else if (Str[n] == 'O')
                            state = states.E8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.E8:
                        if (Str[n] == 'F')
                            state = states.F8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось F";
                        }
                        break;
                    case states.F8:
                        if (Str[n] == ' ')
                            state = states.G8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидался пробел";
                        }
                        break;
                    case states.A8:
                        if (Str[n] == 'E')
                            state = states.B8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.B8:
                        if (Str[n] == 'T')
                            state = states.C8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.C8:
                        if (Str[n] == ' ')
                            state = states.D8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалася пробел";
                        }
                        break;
                    case states.D8:
                        if (Str[n] == ' ')
                            state = states.D8;
                        else if (Str[n] == 'O')
                            state = states.E8;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.A9:
                        if (Str[n] == 'O')
                            state = states.B9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.B9:
                        if (Str[n] == 'I')
                            state = states.C9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось I";
                        }
                        break;
                    case states.C9:
                        if (Str[n] == 'N')
                            state = states.D9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.D9:
                        if (Str[n] == 'T')
                            state = states.E9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.E9:
                        if (Str[n] == 'E')
                            state = states.F9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.F9:
                        if (Str[n] == 'R')
                            state = states.G9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.G9:
                        if (Str[n] == ' ')
                            state = states.H9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалася пробел";
                        }
                        break;
                    case states.H9:
                        if (Str[n] == ' ')
                            state = states.H9;
                        else if (Str[n] == 'T')
                            state = states.I9;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.I9:
                        if (Str[n] == 'O')
                        {
                            state = states.F8;
                            i2 = 4;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.G8:
                        if (Str[n] == ' ')
                            state = states.G8;
                        else if (Str[n] == 'C')
                            state = states.A14;
                        else if (Str[n] == 'I')
                            state = states.A2;
                        else if (Str[n] == 'R')
                            state = states.A3;
                        else if (Str[n] == 'B')
                            state = states.A5;
                        else if (Str[n] == '[')
                            state = states.A10;
                        else if (Str[n] == '(')
                            state = states.A11;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.A14:
                        if (Str[n] == 'A')
                            state = states.B1;
                        else if (Str[n] == 'H')
                            state = states.B4;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.B1:
                        if (Str[n] == 'R')
                            state = states.C1;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.C1:
                        if (Str[n] == 'D')
                            state = states.D1;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось D";
                        }
                        break;
                    case states.D1:
                        if (Str[n] == 'I')
                            state = states.E1;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось I";
                        }
                        break;
                    case states.E1:
                        if (Str[n] == 'N')
                            state = states.F1;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.F1:
                        if (Str[n] == 'A')
                            state = states.G1;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.G1:
                        if (Str[n] == 'L')
                        {
                            state = states.F0;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//CARDINAL
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i2 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.A2:
                        if (Str[n] == 'N')
                            state = states.B2;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.B2:
                        if (Str[n] == 'T')
                            state = states.C2;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.C2:
                        if (Str[n] == 'E')
                            state = states.D2;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.D2:
                        if (Str[n] == 'G')
                            state = states.E2;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось G";
                        }
                        break;
                    case states.E2:
                        if (Str[n] == 'E')
                            state = states.F2;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.F2:
                        if (Str[n] == 'R')
                        {
                            state = states.F0;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//INTEGER
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i2 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.A3:
                        if (Str[n] == 'E')
                            state = states.B3;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.B3:
                        if (Str[n] == 'A')
                            state = states.C3;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.C3:
                        if (Str[n] == 'L')
                        {
                            state = states.F0;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 4;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//REAL
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i2 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.B4:
                        if (Str[n] == 'A')
                            state = states.C4;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.C4:
                        if (Str[n] == 'R')
                        {
                            state = states.F0;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 1;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//CHAR
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i2 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.A5:
                        if (Str[n] == 'O')
                            state = states.B5;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.B5:
                        if (Str[n] == 'O')
                            state = states.C5;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.C5:
                        if (Str[n] == 'L')
                            state = states.D5;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.D5:
                        if (Str[n] == 'E')
                            state = states.E5;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.E5:
                        if (Str[n] == 'A')
                            state = states.F5;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.F5:
                        if (Str[n] == 'N')
                        {
                            state = states.F0;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 1;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//BOOLEAN
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i2 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.A10:
                        if (Str[n] == ' ')
                            state = states.A10;
                        else if (Str[n] == '1')
                            state = states.C10;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось 1";
                        }
                        break;
                    case states.C10:
                        if (Str[n] == ' ')
                            state = states.C10;
                        else if (Str[n] == '.')
                            state = states.D10;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.D10:
                        if (Str[n] == '.')
                            state = states.E10;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.E10:
                        if (Str[n] == ' ')
                            state = states.E10;
                        else if (is_number(Str[n]))
                        {
                            state = states.F10;
                            g = g + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.F10:
                        if (Char.IsDigit(Str[n]))
                        {
                            state = states.F10;
                            g = g + Str[n];
                            if (Convert.ToInt32(g) > 65535)
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.G10;
                            listBox2.Items.Add(g);
                            g = "";
                        }
                        else if (Str[n] == ']')
                        {
                            state = states.F0;
                            listBox2.Items.Add(g);
                            g = "";
                            i1 = 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//[ случай
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.G10:
                        if (Str[n] == ' ')
                            state = states.G10;
                        else if (Str[n] == ']')
                        {
                            state = states.F0;
                            i1 = 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";//[ случай
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.A11:
                        if (Str[n] == ' ')
                            state = states.A11;
                        else if (is_number(Str[n]))
                        {
                            state = states.D11;
                            g = g + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.D11:
                        if (Char.IsDigit(Str[n]))
                        {
                            state = states.D11;
                            g = g + Str[n];
                            if (Convert.ToInt32(g) > 65535)
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.E11;
                            listBox2.Items.Add(g);
                            g = "";
                        }
                        else if (Str[n] == ',')
                        {
                            state = states.A11;
                            listBox2.Items.Add(g);
                            g = "";
                            i3 = i3 + 1;
                        }
                        else if (Str[n] == ')')
                        {
                            state = states.F0;
                            i3 = i3 + 1;
                            listBox2.Items.Add(g);
                            g = "";
                            i1 = i3 * 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i3 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ) или ,";
                        }
                        break;
                    case states.E11:
                        if (Str[n] == ' ')
                            state = states.E11;
                        else if (Str[n] == ',')
                        {
                            state = states.A11;
                            i3 = i3 + 1;
                        }
                        else if (Str[n] == ')')
                        {
                            i3 = i3 + 1;
                            state = states.F0;
                            i1 = i3 * 2;
                            if (i5 != 1)
                                i1 = i1 * i5;
                            c = c + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = 1;
                            i3 = 0;
                            i5 = 1;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ) или ,";
                        }
                        break;
                    case states.F0:
                        if (Str[n] == ' ')
                            state = states.F0;
                        else if (Str[n] == ';')
                            state = states.F;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась ;";
                        }
                        break;
                    case states.A37:
                        if (Str[n] == 'E')
                            state = states.B37;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.B37:
                        if (Str[n] == 'A')
                            state = states.C3;
                        else if (Str[n] == 'C')
                            state = states.D7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.D7:
                        if (Str[n] == 'O')
                            state = states.E7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.E7:
                        if (Str[n] == 'R')
                            state = states.F7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.F7:
                        if (Str[n] == 'D')
                            state = states.G7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось D";
                        }
                        break;
                    case states.G7:
                        if (Str[n] == ' ')
                        {
                            state = states.H7;
                            cm.Add(c);
                            c2 = c;//RECORD
                            c = "";
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидался пробел";
                        }
                        break;
                    case states.H7:
                        if (Str[n] == ' ')
                            state = states.H7;
                        else if (Char.IsLetter(Str[n]) || Str[n] == '_')
                        {
                            state = states.J7;
                            c = c + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в имени идентификатора";
                        }
                        break;
                    case states.J7:
                        if (Char.IsLetter(Str[n]) || Char.IsDigit(Str[n]) || Str[n] == '_')
                        {
                            state = states.J7;
                            c = c + Str[n];
                            if ((c.Length > 8) || (c == "TYPE") || (c == "CARDINAL") || (c == "INTEGER") || (c == "REAL") || (c == "CHAR") || (c == "BOOLEAN") || (c == "ARRAY") || (c == "OF") || (c == "RECORD") || (c == "END") || (c == "SET") || (c == "OF") || (c == "POINTER") || (c == "TO"))
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Идентификатор слишком длинный или является зарезервированным словом";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.K7;
                            if (cm.Contains(c))
                            {
                                {
                                    state = states.E;
                                    listBox1.Items.Clear();
                                    listBox2.Items.Clear();
                                    textBox2.Text = "Повторное использование идентификатора слева";
                                }
                            }
                        }
                        else if (Str[n] == ',')
                        {
                            state = states.H7;
                            if (cm.Contains(c))
                            {
                                {
                                    state = states.E;
                                    listBox1.Items.Clear();
                                    listBox2.Items.Clear();
                                    textBox2.Text = "Повторное использование идентификатора слева";
                                }
                            }
                            i4 = i4 + 1;
                            L = L + c + ", ";
                            cm.Add(c);
                            c = "";
                        }
                        else if (Str[n] == ':')
                        {
                            state = states.M7;
                            i4 = i4 + 1;
                            if (cm.Contains(c))
                            {
                                {
                                    state = states.E;
                                    listBox1.Items.Clear();
                                    listBox2.Items.Clear();
                                    textBox2.Text = "Повторное использование идентификатора слева";
                                }
                            }
                            if (L != "")
                            {
                                cm.Add(c);
                                c = L + c;
                                L = "";
                            }
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось :";
                        }
                        break;
                    case states.K7:
                        if (Str[n] == ' ')
                            state = states.K7;
                        else if (Str[n] == ',')
                        {
                            state = states.H7;
                            i4 = i4 + 1;
                            L = L + c + ", ";
                            cm.Add(c);
                            c = "";
                        }
                        else if (Str[n] == ':')
                        {
                            state = states.M7;
                            i4 = i4 + 1;
                            if (L != "")
                            {
                                cm.Add(c);
                                c = L + c;
                                L = "";
                            }
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось : или ,";
                        }
                        break;
                    case states.M7:
                        if (Str[n] == ' ')
                            state = states.M7;
                        else if (Str[n] == 'C')
                            state = states.A141;
                        else if (Str[n] == 'I')
                            state = states.A21;
                        else if (Str[n] == 'R')
                            state = states.A31;
                        else if (Str[n] == 'B')
                            state = states.A51;
                        else if (Str[n] == '[')
                            state = states.A101;
                        else if (Str[n] == '(')
                            state = states.A111;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.A141:
                        if (Str[n] == 'A')
                            state = states.B19;
                        else if (Str[n] == 'H')
                            state = states.B41;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Неверное зарезервированное слово";
                        }
                        break;
                    case states.B19:
                        if (Str[n] == 'R')
                            state = states.C19;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.C19:
                        if (Str[n] == 'D')
                            state = states.D19;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось D";
                        }
                        break;
                    case states.D19:
                        if (Str[n] == 'I')
                            state = states.E19;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось I";
                        }
                        break;
                    case states.E19:
                        if (Str[n] == 'N')
                            state = states.F19;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.F19:
                        if (Str[n] == 'A')
                            state = states.G19;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.G19:
                        if (Str[n] == 'L')
                        {
                            state = states.N8;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 2;
                            c = c + " " + Convert.ToString(i1) + " байт";//CARDINAL
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i2 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.A21:
                        if (Str[n] == 'N')
                            state = states.B21;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.B21:
                        if (Str[n] == 'T')
                            state = states.C21;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось T";
                        }
                        break;
                    case states.C21:
                        if (Str[n] == 'E')
                            state = states.D21;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.D21:
                        if (Str[n] == 'G')
                            state = states.E21;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось G";
                        }
                        break;
                    case states.E21:
                        if (Str[n] == 'E')
                            state = states.F21;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.F21:
                        if (Str[n] == 'R')
                        {
                            state = states.N8;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 2;
                            c = c + " " + Convert.ToString(i1) + " байт";//INTEGER
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i2 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.A31:
                        if (Str[n] == 'E')
                            state = states.B31;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.B31:
                        if (Str[n] == 'A')
                            state = states.C31;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.C31:
                        if (Str[n] == 'L')
                        {
                            state = states.N8;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 4;
                            c = c + " " + Convert.ToString(i1) + " байт";//REAL
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i2 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.B41:
                        if (Str[n] == 'A')
                            state = states.C41;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.C41:
                        if (Str[n] == 'R')
                        {
                            state = states.N8;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 1;
                            c = c + " " + Convert.ToString(i1) + " байт";//CHAR
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i2 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось R";
                        }
                        break;
                    case states.A51:
                        if (Str[n] == 'O')
                            state = states.B51;
                        else
                        {
                            state = states.E;
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.B51:
                        if (Str[n] == 'O')
                            state = states.C51;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось O";
                        }
                        break;
                    case states.C51:
                        if (Str[n] == 'L')
                            state = states.D51;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось L";
                        }
                        break;
                    case states.D51:
                        if (Str[n] == 'E')
                            state = states.E51;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.E51:
                        if (Str[n] == 'A')
                            state = states.F51;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось A";
                        }
                        break;
                    case states.F51:
                        if (Str[n] == 'N')
                        {
                            state = states.N8;
                            cm.Add(c);
                            if (i2 != 0)
                                i1 = 4;
                            else
                                i1 = i1 * 1;
                            c = c + " " + Convert.ToString(i1) + " байт";//BOOLEAN
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i2 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.A101:
                        if (Str[n] == ' ')
                            state = states.A101;
                        else if (Str[n] == '1')
                            state = states.C101;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось 1";
                        }
                        break;
                    case states.C101:
                        if (Str[n] == ' ')
                            state = states.C101;
                        else if (Str[n] == '.')
                            state = states.D101;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.D101:
                        if (Str[n] == '.')
                            state = states.E101;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась точка";
                        }
                        break;
                    case states.E101:
                        if (Str[n] == ' ')
                            state = states.E101;
                        else if (is_number(Str[n]))
                        {
                            state = states.F101;
                            g = g + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.F101:
                        if (Char.IsDigit(Str[n]))
                        {
                            state = states.F101;
                            g = g + Str[n];
                            if (Convert.ToInt32(g) > 65535)
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.G101;
                            listBox2.Items.Add(g);
                            g = "";
                        }
                        else if (Str[n] == ']')
                        {
                            state = states.N8;
                            listBox2.Items.Add(g);
                            g = "";
                            i1 = 2;
                            c = c + " " + Convert.ToString(i1) + " байт";//[ случай
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.G101:
                        if (Str[n] == ' ')
                            state = states.G101;
                        else if (Str[n] == ']')
                        {
                            state = states.N8;
                            i1 = 2;
                            c = c + " " + Convert.ToString(i1) + " байт";//[ случай
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ]";
                        }
                        break;
                    case states.A111:
                        if (Str[n] == ' ')
                            state = states.A111;
                        else if (is_number(Str[n]))
                        {
                            state = states.D111;
                            g = g + Str[n];
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ошибка в значении переменной";
                        }
                        break;
                    case states.D111:
                        if (Char.IsDigit(Str[n]))
                        {
                            state = states.D111;
                            g = g + Str[n];
                            if (Convert.ToInt32(g) > 65535)
                            {
                                state = states.E;
                                listBox1.Items.Clear();
                                listBox2.Items.Clear();
                                textBox2.Text = "Переменная вне границ диапазона";
                            }
                        }
                        else if (Str[n] == ' ')
                        {
                            state = states.E111;
                            listBox2.Items.Add(g);
                            g = "";
                        }
                        else if (Str[n] == ',')
                        {
                            state = states.A111;
                            listBox2.Items.Add(g);
                            g = "";
                            i3 = i3 + 1;
                        }
                        else if (Str[n] == ')')
                        {
                            state = states.N8;
                            i3 = i3 + 1;
                            listBox2.Items.Add(g);
                            g = "";
                            i1 = i3 * 2;
                            c = c + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            c2 = "";
                            i1 = 1;
                            i3 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ) или ,";
                        }
                        break;
                    case states.E111:
                        if (Str[n] == ' ')
                            state = states.E111;
                        else if (Str[n] == ',')
                        {
                            i3 = i3 + 1;
                            state = states.A111;
                        }
                        else if (Str[n] == ')')
                        {
                            i3 = i3 + 1;
                            state = states.N8;
                            i1 = i3 * 2;
                            c = c + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c);
                            c = "";
                            i1 = i1 * i4;
                            c2 = c2 + " " + Convert.ToString(i1) + " байт";
                            listBox1.Items.Add(c2);
                            i1 = 1;
                            i3 = 0;
                            i4 = 0;
                        }
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось ) или ,";
                        }
                        break;
                    case states.N8:
                        if (Str[n] == ' ')
                            state = states.N7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидался пробел";
                        }
                        break;
                    case states.N7:
                        if (Str[n] == ' ')
                            state = states.N7;
                        else if (Str[n] == 'E')
                            state = states.O7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось E";
                        }
                        break;
                    case states.O7:
                        if (Str[n] == 'N')
                            state = states.P7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось N";
                        }
                        break;
                    case states.P7:
                        if (Str[n] == 'D')
                            state = states.Q7;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалось D";
                        }
                        break;
                    case states.Q7:
                        if (Str[n] == ' ')
                            state = states.Q7;
                        else if (Str[n] == ';')
                            state = states.F;
                        else
                        {
                            state = states.E;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            textBox2.Text = "Ожидалась ;";
                        }
                        break;
                    case states.E:
                        x = false;
                        textBox1.Select(n - 1, 1);
                        state = states.F;
                        break;
                }
                n++;
            }
            if (state == states.F)
            {
                return (x);
            }
            else
            {
                textBox1.Select(n - 1, 1);
                x = false;
                return (x);
            }
        }
    }
}
