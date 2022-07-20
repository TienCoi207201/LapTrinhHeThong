using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTang_App.Src.Model
{
    class Month
    {
        String _MM;
        int _value;

        public Month()
        {
        }

        public Month(string mM, int value)
        {
            _MM = mM;
            _value = value;
        }

        public string MM { get => _MM; set => _MM = value; }
        public int Value { get => _value; set => _value = value; }
    }
}
