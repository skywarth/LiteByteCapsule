using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LiteByte;


namespace XUnitTestNETCore1
{
    public class LiteByteCapsuleTest
    {
        Random rnd = new Random();
        [Fact]
        public void BaseConstructor_Base()
        {
            int amount = rnd.Next(0, 100);
            Stack<CapsuleConstant> capsulationConstants = CapsuleConstant.GenerateCapsulationConstants(amount);
            LiteByteCapsule lite = new LiteByteCapsule(capsulationConstants);
            Assert.NotNull(lite);
            Assert.Equal(amount, lite.GetCapsulationConstants().Count);
            Assert.Equal(capsulationConstants, lite.GetCapsulationConstants());
            Assert.NotSame(capsulationConstants, lite.GetCapsulationConstants());
        }
        [Fact]
        public void BaseConstructor_Null()
        {
            Assert.Throws<ArgumentNullException>(delegate { LiteByteCapsule lite = new LiteByteCapsule(null); });

        }

        [Fact]
        public void BaseConstructor_Empty()
        {

            Assert.Throws<ArgumentException>(delegate { LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(0)); });
        }
        [Fact]
        public void ByteArrayConstructor_Base()
        {
            byte[] firstPart = new byte[rnd.Next(1000)];
            byte[] lastPart = new byte[rnd.Next(1000)];
            for(int i = 0; i < firstPart.Length; i++)
            {
                firstPart[i] =(byte)rnd.Next(256);
            }
            for (int i = 0; i < lastPart.Length; i++)
            {
                lastPart[i] = (byte)rnd.Next(256);
            }
            LiteByteCapsule lite = new LiteByteCapsule(firstPart, lastPart);
            Assert.NotNull(lite.GetCapsulationConstants());
            Assert.Equal(firstPart.Length + lastPart.Length, lite.GetCapsulationConstants().Count);
           

        }



        }
}
