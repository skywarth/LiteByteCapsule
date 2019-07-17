using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteByte
{

    /// <summary>
    /// Create an instance of this class in order to use capsulation methods. Main class for the package/library.
    /// </summary>
    public class LiteByteCapsule
    {

        private Stack<CapsuleConstant> capsulationConstants;
        /// <summary>
        /// Base overload of costructor for LiteByteCapsule. 
        /// </summary>
        /// <param name="constants">This parameter is used for capsulationConstants across the instance of LiteByteCapsule.</param>
        public LiteByteCapsule(Stack<CapsuleConstant> constants)
        {
            capsulationConstants = constants;
        }



        /// <summary>
        /// This overload of constructor takes two different byte arrays to convert into CapsuleConstant type of stack.
        /// If you need to access capsulation constants stack, just call GetCapsulationConstants() to get the stack.
        /// </summary>
        /// <param name="constantFirstPart">Counting from the head, zero based. Positions are exact.</param>
        /// <param name="constantLastPart">Counting from the end, zero based. First element of the array will be the last element of capsule.</param>
        public LiteByteCapsule(byte[] constantFirstPart, byte[] constantLastPart)
        {
            byte[] joint = new byte[constantFirstPart.Length + constantLastPart.Length];
            if (constantFirstPart != null)
            {
                for(int i = 0; i < constantFirstPart.Length; i++)
                {//TODO individual null check
                    capsulationConstants.Push(new CapsuleConstant(constantFirstPart[i], i, true));
                }
            }
            
            
            if (constantLastPart != null)
            {
                for(int k = 0; k < constantLastPart.Length; k++)
                { 
                    capsulationConstants.Push(new CapsuleConstant(constantLastPart[k], (joint.Length-1)-k,false));
                }
            }
        }


        private static byte ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);//long overflow engellemek için (uzun bir streamde)
            return unchecked((byte)longSum);
        }

        //TODO Make an overload method or new method for random constant creation based on number provided. (for ex: 100 random constants)

        /// <summary>
        /// This method is used to check the syntax of a provided (or incoming) byte array package based on the LiteByteCapsule instance constant stack sequence.
        /// </summary>
        /// <param name="capsule">Byte array package to check the syntax according to LiteByteCapsule instance constant sequence</param>
        /// <returns></returns>
        public byte[] CheckSyntax(byte[] capsule)
        {
            byte[] infactData = new byte[capsule.Length - capsulationConstants.Count];
            byte[] capsuleClone = new byte[capsule.Length];
            Array.Copy(capsule, capsuleClone, capsule.Length);
            Stack<CapsuleConstant> capsuleConstantsClone = StackClone<CapsuleConstant>(capsulationConstants);
            bool status = false;

            do
            {
                CapsuleConstant constant = capsuleConstantsClone.Pop();
                if (constant.Head)
                {
                    if (capsuleClone[constant.Position] == constant.Val)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                else
                {
                    if (capsuleClone[(capsuleClone.Length - 1) - constant.Position] == constant.Val)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            while (capsuleConstantsClone.Count() != 0 && status);
            if (status)
            {
                CapsuleConstant maxHead = (from x in capsulationConstants where x.Head == true select x).Max();
                Array.Copy(capsuleClone, maxHead.Position + 1, infactData, 0, infactData.Length);
            }
            else
            {
                infactData = null;
            }




            return infactData;

        }

        /// <summary>
        /// Converts the provided byte array (inner) package to desired capsule format.
        /// </summary>
        /// <param name="infactData">Inner package byte array to capsulate with capsulation constants.</param>
        /// <returns></returns>
        public byte[] ConvertToSyntax(byte[] infactData)
        {
            Stack<CapsuleConstant> capsuleConstantsClone = StackClone<CapsuleConstant>(capsulationConstants);
            int capsuleSize = infactData.Length + capsuleConstantsClone.Count;
            byte[] capsule = new byte[capsuleSize];


            
            while (capsuleConstantsClone.Count != 0)
            {
                CapsuleConstant constant = capsuleConstantsClone.Pop();
                if (constant.Head)
                {
                    capsule[constant.Position] = constant.Val;
                }
                else
                {
                    capsule[(capsule.Length - 1) - constant.Position] = constant.Val;
                }
            }

            //DONE add overload for stack to stack, create array to stack UNDONE
            CapsuleConstant maxHead = (from x in capsulationConstants where x.Head == true select x).Max();
            Array.Copy(infactData, 0, capsule, maxHead.Position + 1, infactData.Length);

            return capsule;
        }

        /// <summary>
        /// Converts the given byte array to string with dashes between each element.
        /// </summary>
        /// <param name="data">Byte array package to convert to string</param>
        /// <returns></returns>
        public static string ConvertToString(byte[] data)
        {
            StringBuilder bld = new StringBuilder();
            if (data != null)
            {
                foreach (byte element in data)
                {
                    bld.Append(element);
                    bld.Append("-");
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
            if(bld !=null)
            {
                bld.Remove(bld.Length, 1);
            }
            
            return bld.ToString();
        }
        /// <summary>
        /// A helper method to get the capsulation constants stack.
        /// </summary>
        /// <returns>Returns capsulation constants stack.</returns>
        public Stack<CapsuleConstant> GetCapsulationConstants()
        {
            return capsulationConstants;
        }
        private static Stack<CapsuleConstant> StackClone<CapsuleConstant>(Stack<CapsuleConstant> original)
        {
            var arr = new CapsuleConstant[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<CapsuleConstant>(arr);
        }

    }
}