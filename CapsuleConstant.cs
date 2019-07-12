using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteBiteCapsule
{
    class CapsuleConstant
    {
        private byte val;
        private int position;//0 is first 
        private bool head;

        public byte Val { get => val; set => val = value; }
        public int Position { get => position; set => position = value; }
        public bool Head { get => head; set => head = value; }
    }
}
