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


    }
}
