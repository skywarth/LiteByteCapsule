using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteBiteCapsule
{
    class LiteByteCapsule
    {//
        private Stack<CapsuleConstant> capsulationConstants;
        public LiteByteCapsule(Stack<CapsuleConstant> constants)
        {
            capsulationConstants = constants;
        }

        private byte ComputeAdditionChecksum(byte[] data)
        {
            long longSum = data.Sum(x => (long)x);//long overflow engellemek için (uzun bir streamde)
            return unchecked((byte)longSum);
        }
        public byte[] CheckRevSyntax(byte[] data)
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
        }
        public byte[] ConvertToSyntax(byte[] infactData)
        {
            int capsuleSize = infactData.Length + capsulationConstants.Count;
            byte[] capsule = new byte[capsuleSize];
            //[10] length=10, max[9]
            foreach(CapsuleConstant c in capsulationConstants)
            {
                CapsuleConstant constant= capsulationConstants.Pop();
                if (constant.Head)
                {
                    capsule[constant.Position] = constant.Val;
                }
                else
                {
                    capsule[capsule.Length - constant.Position-1] = constant.Val;
                }
                
            }

            for(int i = 0; i < capsule.Length; i++)
            {
                
            }


            int size = infactData.Length;
            byte[] encodedData = new byte[size + 5];

            encodedData[0] = ComTitles.Head;
            encodedData[1] = (byte)size;
            encodedData[2] = ComTitles.Head;
            Array.Copy(infactData, 0, encodedData, 3, size);
            encodedData[encodedData.Length - 1] = ComTitles.End;
            encodedData[encodedData.Length - 2] = ComputeAdditionChecksum(infactData);

            return encodedData;
        }

        public string ConvertToString(byte[] data)
        {
            string content="";
            if(data!=null)
            {
                foreach (byte element in data)
                {
                    content += element;
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

    }
}
