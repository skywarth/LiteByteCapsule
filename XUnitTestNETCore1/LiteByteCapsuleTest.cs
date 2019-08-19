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

            Assert.Throws<ArgumentException>(delegate { new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(0)); });
        }
        [Fact]
        public void ByteArrayConstructor_Base()
        {
            byte[] firstPart = new byte[rnd.Next(1000)];
            byte[] lastPart = new byte[rnd.Next(1000)];
            for (int i = 0; i < firstPart.Length; i++)
            {
                firstPart[i] = (byte)rnd.Next(256);
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
            byte[] pack = { 12, 0, 14, 255, 131 };
            string st = LiteByteCapsule.ConvertToString(pack);
            Assert.NotNull(st);
            Assert.IsType<String>(st);
            Assert.NotEmpty(st);
            Assert.StartsWith("1", st.Substring(0));
            Assert.EndsWith("1", st.Substring(st.Length - 1));

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
            byte[] capsule = lite.ConvertToSyntax(pack);
            Assert.NotNull(capsule);
            Assert.Equal(pack.Length + amount, capsule.Length);
            Assert.NotEmpty(capsule);
            Assert.NotSame(pack, capsule);
            Assert.NotEqual(pack, capsule);
            Assert.Contains<byte>(pack[0], capsule);

        }

        [Fact]
        public void ConvertToSyntax_Null()
        {
            int amount = 100;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            Assert.Throws<ArgumentNullException>(delegate { lite.ConvertToSyntax(null); });

        }
        [Fact]
        public void ConvertToSyntax_Empty()
        {
            int amount = 100;
            byte[] pack = { };
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] capsule = lite.ConvertToSyntax(pack);
            Assert.NotNull(capsule);
            Assert.NotEmpty(capsule);
            Assert.Equal(amount, capsule.Length);
        }

        [Fact]
        public void CheckSyntax_Base()
        {
            int amount = 100;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] package = { 99, 0, 255, 12, 33, 54, 123 };
            byte[] capsule = lite.ConvertToSyntax(package);
            byte[] inner = lite.CheckSyntax(capsule);
            Assert.NotNull(inner);
            Assert.NotEmpty(inner);
            Assert.Equal(inner.Length, package.Length);
            Assert.Equal(package, inner);

        }
        [Fact]
        public void CheckSyntax_Null()
        {
            int amount = 100;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            Assert.Throws<ArgumentNullException>(delegate { lite.CheckSyntax(null); });
        }

        [Fact]
        public void CheckSyntax_Empty()
        {
            int amount = 100;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] capsule = { };
            Assert.Throws<ArgumentException>(delegate { lite.CheckSyntax(capsule); });
        }

        [Fact]
        public void CheckSyntax_Imposter()
        {
            int amount = 100;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] capsule = { 23, 55, 123, 33, 98, 235 };
            Assert.Null(lite.CheckSyntax(capsule));
        }

        [Fact]
        public void CheckSyntax_Imposter_SameLength()
        {
            int amount = 5;
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(amount));
            byte[] capsule = { 23, 55, 123, 33, 98, 235, 33, 99, 52 };
            Assert.Null(lite.CheckSyntax(capsule));
        }


        [Fact]
        public void GenerateCRC32C_Base()
        {
            byte[] exArray1 = { 255 };
            string crc1 = LiteByteCapsule.GenerateCRC32C(exArray1);
            Assert.NotNull(crc1);
            Assert.NotEmpty(crc1);
            Assert.Equal("0xFF000000", crc1);
        }//TODO generateCRC32C_same()

        [Fact]
        public void GenerateCRC32C_Same()
        {
            byte[] exArray1 = { 255 };
            byte[] exArray2 = { 0xFF };
            string crc1 = LiteByteCapsule.GenerateCRC32C(exArray1);
            string crc2 = LiteByteCapsule.GenerateCRC32C(exArray2);
            Assert.NotNull(crc1);
            Assert.NotNull(crc2);
            Assert.Equal(crc1, crc2);
        }

        [Fact]
        public void GenerateCRC32C_Null()
        {
            Assert.Throws<ArgumentNullException>(delegate { LiteByteCapsule.GenerateCRC32C(null); });
        }

        [Fact]
        public void GenerateCRC32C_Empty()
        {
            byte[] package = { };
            string hex = LiteByteCapsule.GenerateCRC32C(package);
            Assert.NotNull(hex);
            Assert.NotEmpty(hex);
            Assert.StartsWith("0x", hex);
            //Assert.Equal("0x00000000", hex);
        }
        [Fact]
        public void AddCRC32CToEnd_Base()
        {
            byte[] package = LiteByteCapsule.AddCRC32CToEnd(LiteByteCapsule.GetRandomPackage(100));
            Assert.NotNull(package);
            Assert.NotEmpty(package);
            Assert.True(package.Length == 104);
        }


        [Fact]
        public void AddCRC32CToEnd_Same()
        {
            byte[] package = LiteByteCapsule.GetRandomPackage(100);
            byte[] newPackage1 = LiteByteCapsule.AddCRC32CToEnd(package);
            byte[] newPackage2 = LiteByteCapsule.AddCRC32CToEnd(package);
            Assert.Equal(newPackage1, newPackage2);

        }
        [Fact]
        public void CheckCRC32CIntegrity_Base()
        {
            byte[] package = LiteByteCapsule.GetRandomPackage(100);
            byte[] capsule = LiteByteCapsule.AddCRC32CToEnd(package);
            byte[] subPackage = LiteByteCapsule.CheckCRC32CIntegrity(capsule);
            Assert.NotNull(subPackage);
            Assert.NotEmpty(subPackage);
            Assert.True(subPackage.Length == 100);
            Assert.Equal(package, subPackage);
        }
        [Fact]
        public void CheckCRC32CIntegrity_Imposter()
        {
            byte[] capsule = LiteByteCapsule.GetRandomPackage(7);
            byte[] subPackage = LiteByteCapsule.CheckCRC32CIntegrity(capsule);
            Assert.Null(subPackage);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CheckCRC32CIntegrity_LowLength(uint size)
        {
            byte[] capsule = LiteByteCapsule.GetRandomPackage(size);
            Assert.Throws<ArgumentException>(delegate { LiteByteCapsule.CheckCRC32CIntegrity(capsule); });
        }
        [Fact]
        public void CheckCRC32CIntegrity_Null()
        {
            Assert.Throws<ArgumentNullException>(delegate { LiteByteCapsule.CheckCRC32CIntegrity(null); });
        }


        [Fact]
        public void SmartCapsuleConstructor_Base()
        {
            int length = 300;
            byte[] package = LiteByteCapsule.GetRandomPackage((uint)length);
            LiteByteCapsule lite = new LiteByteCapsule();
            byte[] capsule = lite.ConvertToSyntax(package);
            Assert.NotNull(capsule);
            Assert.NotEmpty(capsule);
            Assert.Equal(length + 6, capsule.Length);

        }

        [Fact]
        public void CapsulationConstantsJSONString_Base()
        {
            LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(50));
            string json=lite.CapsulationConstantsJSONString();
            Assert.NotNull(json);
            Assert.NotEmpty(json);
            //FIXME this sucks, do a proper unit testing for this method.
        }
    }
}
