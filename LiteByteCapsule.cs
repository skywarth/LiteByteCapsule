using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteBiteCapsule
{
    class LiteByteCapsule
    {//
        private CapsuleConstant[] capsulationConstants;
        public LiteByteCapsule(CapsuleConstant[] constants)
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
            /**/
            //int adet = stream.Read(data, 0, data.Length);

            int size = data.Length;//increment for correct size, since .length gives 3 for a 4 element array.


            int NumberOfConst = capsulationConstants.Length;


            byte[] InfactData = new byte[size - NumberOfConst];

            try
            {

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
        public CapsuleConstant[] GetCapsulationConstants()
        {
            return capsulationConstants;
        }

    }
}
