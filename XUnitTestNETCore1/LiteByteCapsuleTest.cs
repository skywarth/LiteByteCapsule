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
            Assert.Throws<ArgumentNullException>(delegate { new LiteByteCapsule(null); });

        }

        [Fact]
        public void BaseConstructor_Empty()
        {

            Assert.Throws<ArgumentException>(delegate {new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(0)); });
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
        [Fact]
        public void ConvertToString_Base()
        {
            byte[] pack={12,0,14,255,131};
            string st=LiteByteCapsule.ConvertToString(pack);
            Assert.NotNull(st);
            Assert.IsType<String>(st);
            Assert.NotEmpty(st);
            Assert.StartsWith("1", st.Substring(0));
            Assert.EndsWith("1", st.Substring(st.Length-1));

        }

        [Fact]
        public void ConvertToString_Null()
        {
            Assert.Throws<ArgumentNullException>(delegate { LiteByteCapsule.ConvertToString(null); });

        }


        [Fact]
        public void ConvertToSyntax_Base()
        {
            int amount = 100;
            byte[] pack = { 12, 0, 14, 255, 131 };
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] capsule=lite.ConvertToSyntax(pack);
            Assert.NotNull(capsule);
            Assert.Equal(pack.Length + amount, capsule.Length);
            Assert.NotEmpty(capsule);
            Assert.NotSame(pack, capsule);
            Assert.NotEqual(pack, capsule);
            Assert.Contains<byte>(pack[0], capsule);
            
        }




        }
}
