using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteByte
{


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


        private byte ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);//long overflow engellemek için (uzun bir streamde)
            return unchecked((byte)longSum);
        }
        /*public byte[] CheckRevSyntax(byte[] data)
        {
            bool status = false;
            int capsuleSize = data.Length;


            byte[] InfactData = new byte[capsuleSize-capsulationConstants.Count];

            try
            {
                for(int i = 0; i < capsuleSize; i++)
                {
                    if(data[i].)


                }




                if (data[0] == ComTitles.Head)// first head check
                {
                    if (data[1] == (byte)(size - NumberOfConst))//infact data size check
                    {
                        if (data[2] == ComTitles.Head)// second head check
                        {
                            if (data[size - 1] == ComTitles.End)//end check
                            {

                                Array.Copy(data, 3, InfactData, 0, (size - NumberOfConst));
                                byte checkSum = ComputeAdditionChecksum(InfactData);
                                if (data[size - 2] == checkSum)
                                {

                                    Console.WriteLine("Infact package content:");
                                }

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            return InfactData;
        /**/
        //}
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
                //CapsuleConstant maxNonHead = (from x in capsulationConstants where x.Head != true select x).Max();
                Array.Copy(capsuleClone, maxHead.Position + 1, infactData, 0, infactData.Length);
            }
            else
            {
                infactData = null;
            }




            return infactData;

        }


        public byte[] ConvertToSyntax(byte[] infactData)
        {
            Stack<CapsuleConstant> capsuleConstantsClone = StackClone<CapsuleConstant>(capsulationConstants);
            int capsuleSize = infactData.Length + capsuleConstantsClone.Count;
            byte[] capsule = new byte[capsuleSize];


            //[10] length=10, max[9]
            /*foreach(CapsuleConstant c in capsulationConstants)
            {
                CapsuleConstant constant= capsulationConstants.Pop();//constant.val=5, position=2, head=true
                if (constant.Head)
                {
                    capsule[constant.Position] = constant.Val;
                }
                else
                {
                    capsule[(capsule.Length - 1) - constant.Position] = constant.Val;
                }

            }*/
            while (capsuleConstantsClone.Count != 0)
            {
                CapsuleConstant constant = capsuleConstantsClone.Pop();//constant.val=5, position=2, head=true
                if (constant.Head)
                {
                    capsule[constant.Position] = constant.Val;
                }
                else
                {
                    capsule[(capsule.Length - 1) - constant.Position] = constant.Val;
                }
            }

            /*int infactStartPosition = Array.IndexOf(capsule, null);*/
            //TODO add overload for stack to stack, create array to stack
            //CapsuleConstant maxHead = (from x in capsulationConstants where x.Head = true select x).Max();
            //CapsuleConstant maxHead = capsulationConstants.MaxBy();

            /*WORKING COMPARISON APPROACH*
             * 
             *CapsuleConstant maxHead = capsulationConstants.OrderByDescending(x => x.Position).First();
             * */

            /* WORKING COMPARISON 2
             * 
            CapsuleConstant maxHead = (from x in capsulationConstants where x.Head = true select x).Max();
            */
            CapsuleConstant maxHead = (from x in capsulationConstants where x.Head == true select x).Max();
            //Console.WriteLine("maxHead is=" + maxHead.Head + " " + maxHead.Position);
            Array.Copy(infactData, 0, capsule, maxHead.Position + 1, infactData.Length);


            /*
             * OLD
             * int size = infactData.Length;
            byte[] encodedData = new byte[size + 5];

            encodedData[0] = ComTitles.Head;
            encodedData[1] = (byte)size;
            encodedData[2] = ComTitles.Head;
            Array.Copy(infactData, 0, encodedData, 3, size);
            encodedData[encodedData.Length - 1] = ComTitles.End;
            encodedData[encodedData.Length - 2] = ComputeAdditionChecksum(infactData);
            */
            return capsule;
        }

        public string ConvertToString(byte[] data)
        {
            string content = "";
            if (data != null)
            {
                foreach (byte element in data)
                {
                    content += element + "-";
                }
            }
            else
            {
                content = null;
            }

            return content;
        }
        public Stack<CapsuleConstant> GetCapsulationConstants()
        {
            return capsulationConstants;
        }
        public Stack<CapsuleConstant> StackClone<CapsuleConstant>(Stack<CapsuleConstant> original)
        {
            var arr = new CapsuleConstant[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<CapsuleConstant>(arr);
        }

    }
}