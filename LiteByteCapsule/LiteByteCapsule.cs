using Force.Crc32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace LiteByte
{

    /// <summary>
    /// Create an instance of this class in order to use capsulation methods. Main class for the package/library.
    /// </summary>
    public class LiteByteCapsule
    {
        private static readonly RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private Stack<CapsuleConstant> capsulationConstants;
        /// <summary>
        /// Base overload of costructor for LiteByteCapsule. 
        /// </summary>
        /// <param name="constants">This parameter is used for capsulationConstants across the instance of LiteByteCapsule.</param>
        public LiteByteCapsule(Stack<CapsuleConstant> constants)
        {
            /*
             * capsulationConstants = constants;
             * Old, guess it's referencing, not copying
             **/
             //DONE null check
             if(constants==null)
            {
                throw new ArgumentNullException("constants", "Stack parameter cannot be null");
            }//TODO BIG messages for exceptions (all)
             else if (constants.Count.Equals(0))
            {
                throw new ArgumentException("Stack parameter cannot be empty","constants");
            }
            capsulationConstants = StackClone<CapsuleConstant>(constants);
        }
        /// <summary>
        /// This overload of constructor takes two different byte arrays to convert into CapsuleConstant type of stack.
        /// If you need to access capsulation constants stack, just call GetCapsulationConstants() to get the stack.
        /// </summary>
        /// <param name="constantFirstPart">Counting from the head, zero based. Positions are exact.</param>
        /// <param name="constantLastPart">Counting from the end, zero based. First element of the array will be the last element of capsule.</param>
        public LiteByteCapsule(byte[] constantFirstPart, byte[] constantLastPart)
        {
            capsulationConstants = new Stack<CapsuleConstant>();


            byte[] joint = new byte[constantFirstPart.Length + constantLastPart.Length];
            if (constantFirstPart != null)
            {
                for (int i = 0; i < constantFirstPart.Length; i++)
                {
                    capsulationConstants.Push(new CapsuleConstant(constantFirstPart[i], i, true));
                }
            }

            //TODO create a method for smart capsule constant creation
            if (constantLastPart != null)
            {
                for (int k = 0; k < constantLastPart.Length; k++)
                {
                    capsulationConstants.Push(new CapsuleConstant(constantLastPart[k], (joint.Length - 1) - k, false));
                }
            }


        }


        public LiteByteCapsule()
        {
            capsulationConstants = null;
        }

        private void SmartCapsule(byte[] infactData)
        {
            capsulationConstants = new Stack<CapsuleConstant>();
            capsulationConstants.Push(new CapsuleConstant(0, 0, true));
            capsulationConstants.Push(new CapsuleConstant((byte)infactData.Length, 1, true));
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        /*private static byte ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);//long overflow engellemek için (uzun bir streamde)
            return unchecked((byte)longSum);
        }*/

        //DONE Make an overload method or new method for random constant creation based on number provided. (for ex: 100 random constants)

        /// <summary>
        /// This method is used to check the syntax of a provided (or incoming) byte array package based on the LiteByteCapsule instance constant stack sequence.
        /// </summary>
        /// <param name="capsule">Byte array package to check the syntax according to LiteByteCapsule instance constant sequence</param>
        /// <returns></returns>
        public byte[] CheckSyntax(byte[] capsule)
        {
            if (capsule == null)
            {
                throw new ArgumentNullException("capsule", "Byte array parameter capsule cannot be null.");
            }
            else if (capsule.Length==0)
            {
                throw new ArgumentException("Capsule parameter cannot be empty.", "capsule");
            }//TODO Method for saving or recovering constants from JSON file
            else if (capsule.Length <= capsulationConstants.Count) {
                return null;
            }
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
            bool smartCapsule = false;
            if (infactData == null)
            {
                throw new ArgumentNullException("infactData", "Byte array parameter infactData cannot be null");
            }
            if (capsulationConstants == null)
            {
                SmartCapsule(infactData);
                smartCapsule = true;
            }
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
            if(smartCapsule)
            {
                capsule = AddCRC32CToEnd(capsule);
            }
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
                bld.Remove(bld.Length-1, 1);
            }
            
            return bld.ToString();
        }

        /// <summary>
        /// Returns the Stack of "CapsulationConstant" instances in string form as JSON format.
        /// </summary>
        /// <returns>String in JSON format</returns>
        public string CapsulationConstantsJSONString()
        {
            return JsonConvert.SerializeObject(capsulationConstants, Formatting.Indented);
        }//TEST capsulation stack to json string

        /// <summary>
        /// A helper method to get the capsulation constants stack.
        /// </summary>
        /// <returns>Returns capsulation constants stack.</returns>
        public Stack<CapsuleConstant> GetCapsulationConstants()
        {
            return capsulationConstants;
        }
        private static Stack<T> StackClone<T>(Stack<T> original)
        {
           
                var arr = new T[original.Count];
                original.CopyTo(arr, 0);
                Array.Reverse(arr);
                return new Stack<T>(arr);
        }

        /// <summary>
        /// Used to generate the CRC32-C hash of a given byte array. Returns CRC32-C hash in hex string format.
        /// </summary>
        /// <param name="package">Byte array to calculate CRC32-C.</param>
        /// <returns>CRC32-C hash in hex string format.</returns>
        public static string GenerateCRC32C(byte[] package)
        {
            String crc = String.Format("0x{0:X}", Crc32CAlgorithm.Compute(package));
            return crc;
        }


        /// <summary>
        /// Calculates and adds the CRC32-C of an byte array to the end of the array. Doesn't overwrite, returns new byte array with original size + 4
        /// </summary>
        /// <param name="package">Byte array to calculate CRC32-C.</param>
        /// <returns>Returns byte array with size of original array + 4. Last 4 elements are CRC32-C.</returns>
        public static byte[] AddCRC32CToEnd(byte[] package)
        {
            byte[] newPackage = new byte[package.Length + 4];
            Array.Copy(package, 0, newPackage, 0, package.Length);
            Crc32CAlgorithm.ComputeAndWriteToEnd(newPackage);
            return newPackage;
        }

        public static byte[] CheckCRC32CIntegrity(byte[] package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package", "Null byte array is not valid parameter for this method.");
            }
            else if (package.Length < 4)
            {
                throw new ArgumentException("Parameter length/size cannot be less than 4","package");
            }
            
            bool status = false;
            byte[] subPackage = new byte[package.Length - 4];
            Array.Copy(package, 0, subPackage, 0, subPackage.Length);
            status = Enumerable.SequenceEqual(AddCRC32CToEnd(subPackage), package);
            if (!status)
            {
                subPackage = null;
            }
            return subPackage;
        }




        public static byte[] GetRandomPackage(uint count)
        {
            byte[] package = new byte[count];
            rngCsp.GetBytes(package);
            return package;
        }


    }
}