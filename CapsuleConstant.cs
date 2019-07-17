using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteByte{

     /// <summary>
     /// Class to generate instances for capsulation constants.
     /// </summary>
    public class CapsuleConstant : IComparable<CapsuleConstant>
    {
        private byte val;
        private int position;//0 is first 
        private bool head;
        /// <summary>
        /// Value property of an CapsuleConstant instance. Byte value for constant.
        /// </summary>
        public byte Val { get => val; set => val = value; }

        /// <summary>
        /// Position of the constant related to the start position parameter(head). Ex: if position is:0 and head=true, this constant will be the first element in the capsule.
        /// </summary>
        public int Position { get => position; set => position = value; }

        /// <summary>
        /// Property to indicate counting for position from start(head) or counting from the end(tail) of the capsule. Ex:Head:false, position:0 will be the last element of the capsule.
        /// </summary>
        public bool Head { get => head; set => head = value; }

        /// <summary>
        /// Sole constructor of CapsuleConstant class to initiate instances.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <param name="head"></param>
        public CapsuleConstant(byte value, int position, bool head)
        {
            this.val = value;
            this.position = position;
            this.head = head;
        }

        /// <summary>
        /// Implementation of IEnumarable 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CapsuleConstant other)
        {
            return this.position.CompareTo(other.position);
            
        }
    }
}