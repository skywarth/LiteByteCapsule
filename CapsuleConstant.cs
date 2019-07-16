using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteByte{


    public class CapsuleConstant : IComparable<CapsuleConstant>
    {
        private byte val;
        private int position;//0 is first 
        private bool head;

        public byte Val { get => val; set => val = value; }
        public int Position { get => position; set => position = value; }
        public bool Head { get => head; set => head = value; }

        public CapsuleConstant(byte value, int position, bool head)
        {
            this.val = value;
            this.position = position;
            this.head = head;
        }

        public int CompareTo(CapsuleConstant other)
        {
            return this.position.CompareTo(other.position);
            /*if (this.head && other.head)
            {
                return this.position.CompareTo(other.position);
            }
            else
            {
                return -1;
            }*/
        }
    }
}