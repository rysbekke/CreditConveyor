using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace СreditСonveyor.Data.Technodom
{
    public class mod43
    {
        public string getCode39Mod43(string s)
        {
            int sum = 0;
            string temps = s.ToUpper();
            for (int i = 0; i < s.Length; i++)
            {
                sum += AsciiToCharTable(temps[i]);
            }

            int mod = sum % 43;
            return temps + CharTableToString(mod);
        }

        public int AsciiToCharTable(char c)
        {
            if (c == 48)//0
            {
                return 0;
            }
            else if (c == 49)//1
            {
                return 1;
            }
            else if (c == 50)//2
            {
                return 2;
            }
            else if (c == 51)//3
            {
                return 3;
            }
            else if (c == 52)//4
            {
                return 4;
            }
            else if (c == 53)//5
            {
                return 5;
            }
            else if (c == 54)//6
            {
                return 6;
            }
            else if (c == 55)//7
            {
                return 7;
            }
            else if (c == 56)//8
            {
                return 8;
            }
            else if (c == 57)//9
            {
                return 9;
            }
            else if (c == 65)//A
            {
                return 10;
            }
            else if (c == 66)//B
            {
                return 11;
            }
            else if (c == 67)//C
            {
                return 12;
            }
            else if (c == 68)//D
            {
                return 13;
            }
            else if (c == 69)//E
            {
                return 14;
            }
            else if (c == 70)//F
            {
                return 15;
            }
            else if (c == 71)//G
            {
                return 16;
            }
            else if (c == 72)//H
            {
                return 17;
            }
            else if (c == 73)//I
            {
                return 18;
            }
            else if (c == 74)//J
            {
                return 19;
            }
            else if (c == 75)//K
            {
                return 20;
            }
            else if (c == 76)//L
            {
                return 21;
            }
            else if (c == 77)//M
            {
                return 22;
            }
            else if (c == 78)//N
            {
                return 23;
            }
            else if (c == 79)//O
            {
                return 24;
            }
            else if (c == 80)//P
            {
                return 25;
            }
            else if (c == 81)//Q
            {
                return 26;
            }
            else if (c == 82)//R
            {
                return 27;
            }
            else if (c == 83)//S
            {
                return 28;
            }
            else if (c == 84)//T
            {
                return 29;
            }
            else if (c == 85)//U
            {
                return 30;
            }
            else if (c == 86)//V
            {
                return 31;
            }
            else if (c == 87)//W
            {
                return 32;
            }
            else if (c == 88)//X
            {
                return 33;
            }
            else if (c == 89)//Y
            {
                return 34;
            }
            else if (c == 90)//Z
            {
                return 35;
            }
            else if (c == 45)//-
            {
                return 36;
            }
            else if (c == 46)//.
            {
                return 37;
            }
            else if (c == 32)//sp
            {
                return 38;
            }
            else if (c == 36)//$
            {
                return 39;
            }
            else if (c == 47)///
            {
                return 40;
            }
            else if (c == 43)//+
            {
                return 41;
            }
            else if (c == 37)//%
            {
                return 42;
            }
            else
            {
                return 0;
            }
        }

        public string CharTableToString(int c)
        {
            if (c == 0)//0
            {
                return "0";
            }
            else if (c == 1)//1
            {
                return "1";
            }
            else if (c == 2)//2
            {
                return "2";
            }
            else if (c == 3)//3
            {
                return "3";
            }
            else if (c == 4)//4
            {
                return "4";
            }
            else if (c == 5)//5
            {
                return "5";
            }
            else if (c == 6)//6
            {
                return "6";
            }
            else if (c == 7)//7
            {
                return "7";
            }
            else if (c == 8)//8
            {
                return "8";
            }
            else if (c == 9)//9
            {
                return "9";
            }
            else if (c == 10)//A
            {
                return "A";
            }
            else if (c == 11)//B
            {
                return "B";
            }
            else if (c == 12)//C
            {
                return "C";
            }
            else if (c == 13)//D
            {
                return "D";
            }
            else if (c == 14)//E
            {
                return "E";
            }
            else if (c == 15)//F
            {
                return "F";
            }
            else if (c == 16)//G
            {
                return "G";
            }
            else if (c == 17)//H
            {
                return "H";
            }
            else if (c == 18)//I
            {
                return "I";
            }
            else if (c == 19)//J
            {
                return "J";
            }
            else if (c == 20)//K
            {
                return "K";
            }
            else if (c == 21)//L
            {
                return "L";
            }
            else if (c == 22)//M
            {
                return "M";
            }
            else if (c == 23)//N
            {
                return "N";
            }
            else if (c == 24)//O
            {
                return "O";
            }
            else if (c == 25)//P
            {
                return "P";
            }
            else if (c == 26)//Q
            {
                return "Q";
            }
            else if (c == 27)//R
            {
                return "R";
            }
            else if (c == 28)//S
            {
                return "S";
            }
            else if (c == 29)//T
            {
                return "T";
            }
            else if (c == 30)//U
            {
                return "U";
            }
            else if (c == 31)//V
            {
                return "V";
            }
            else if (c == 32)//W
            {
                return "W";
            }
            else if (c == 33)//X
            {
                return "X";
            }
            else if (c == 34)//Y
            {
                return "Y";
            }
            else if (c == 35)//Z
            {
                return "Z";
            }
            else if (c == 36)//-
            {
                return "-";
            }
            else if (c == 37)//.
            {
                return ".";
            }
            else if (c == 38)//sp
            {
                return " ";
            }
            else if (c == 39)//$
            {
                return "$";
            }
            else if (c == 40)///
            {
                return "/";
            }
            else if (c == 41)//+
            {
                return "+";
            }
            else if (c == 42)//%
            {
                return "%";
            }
            else
            {
                return "";
            }
        }
    }
}