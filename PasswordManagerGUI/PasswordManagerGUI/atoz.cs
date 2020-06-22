using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerGUI
{
    class Encrypt
    {
        public string ATOZ(string code)
        {
            string newCode = "";

            for(int i = 0; i < code.Length; i++)
            {
                if(code[i] >= 'a' && code[i] <= 'z')
                {
                    newCode += (char)('z' - (code[i] - 'a'));
                }
                else if (code[i] >= 'A' && code[i] <= 'Z')
                {
                    newCode += (char)('Z' - (code[i] - 'A'));
                }
                else if(code[i] >= '0' && code[i] <= '9')
                {
                    newCode += (char)('9' - (code[i] - '0'));
                }
                if(code[i] == ' ')
                {
                    newCode += code[i];
                }
            }

            return newCode;
        }

    }
}
