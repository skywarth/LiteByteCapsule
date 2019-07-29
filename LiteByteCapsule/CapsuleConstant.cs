using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace LiteByte{

     /// <summary>
     /// Class to generate instances for capsulation constants.
     /// </summary>
    public class CapsuleConstant : IComparable<CapsuleConstant>
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
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
        /// Base constructor of CapsuleConstant class to initiate instances.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <param name="head"></param>
        public CapsuleConstant(byte value, int position, bool head)
        {
            this.val = value;
            if (position < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.position = position;
            this.head = head;
        }

        private CapsuleConstant(byte value,int headCounter, int tailCounter,bool head)
        {
            this.val = value;
            if (head)
            {
                this.position = headCounter;
            }
            else
            {
                this.position = tailCounter;
            }
            this.head = head;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Values which vary from 1 to int.max()</param>
        /// <returns></returns>
        public static Stack<CapsuleConstant> GenerateCapsulationConstants(int amount)
        {

            try
            {
                uint unsignedAmount = Convert.ToUInt32(amount);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException();
            }
            Stack<CapsuleConstant> capsuleConstants = new Stack<CapsuleConstant>();

            byte[] randomHolder = new byte[1];
            
            

            Random rnd = new Random();
            int headCounter = -1;
            int tailCounter = -1;
            bool head;
            
            for (int i = 0; i < amount; i++)
            {//TODO stack to string conversion for console writeline, string
                rngCsp.GetBytes(randomHolder);
                if (rnd.NextDouble() >= 0.5)
                {
                    head = true;
                    headCounter++;
                    
                }//TODO unit test for Crypto randomizer imp
                else
                {
                    head = false;
                    tailCounter++;
                    //TODO method for searching capsulation stack for certain stuff
                }
                capsuleConstants.Push(new CapsuleConstant(randomHolder[0], headCounter, tailCounter, head));

            }

            return capsuleConstants;
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