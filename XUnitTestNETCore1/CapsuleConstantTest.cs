using System;
using Xunit;
using Moq;
using LiteByte;
using System.Collections.Generic;

namespace XUnitTestNETCore1

{
    public class CapsuleConstantTest
    {
        [Fact]
        public void BaseConstructor_Base()
        {
            Random rnd = new Random();
            byte val = (byte)rnd.Next(0, 255);
            int pos = rnd.Next();
            bool head;
            if (rnd.NextDouble() >= 0.5)
            {
                head = true;
            }
            else
            {
                head = false;
            }
            CapsuleConstant constantObj1 = new CapsuleConstant(val, pos, head);
            CapsuleConstant constantObj2 = new CapsuleConstant(val, pos, head);
            Assert.NotNull(constantObj1);
            Assert.IsType<CapsuleConstant>(constantObj1);
            Assert.Equal(constantObj1, constantObj2);
            Assert.InRange(constantObj1.Val, 0, 255);
            Assert.InRange(constantObj1.Position, 0, int.MaxValue);




        }
        [Fact]
        public void BaseConstructor_Negative()
        {
            byte val = 255;
            int pos = -123123123;
            Random rnd = new Random();
            bool head;
            if (rnd.NextDouble() >= 0.5)
            {
                head = true;
            }
            else
            {
                head = false;
            }
            Assert.Throws<ArgumentOutOfRangeException>(delegate {new CapsuleConstant(val, pos, head); });

        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void GenerateRandom_Boundary(int val)
        {
            Stack<CapsuleConstant> capsulationConstants = new Stack<CapsuleConstant>();
            Assert.Throws<ArgumentOutOfRangeException>(delegate { CapsuleConstant.GenerateCapsulationConstants(val); });
            
        }

        [Fact]
        public void GenerateRandom_Count()
        {//FIXME Memory problem
            Stack<CapsuleConstant> capsulationConstants = new Stack<CapsuleConstant>();
            Random rnd = new Random();
            int amount=rnd.Next(0, 1000);
            Assert.Equal(amount, (CapsuleConstant.GenerateCapsulationConstants(amount)).Count);

        }
        //TODO test generateRandom for int.MaxValue exception throw(also implement)
        [Fact]
        public void GenerateRandom_Base()
        {
            Stack<CapsuleConstant> capsulationConstants;
            Random rnd = new Random();
            int amount = rnd.Next(0, 100);
            capsulationConstants=CapsuleConstant.GenerateCapsulationConstants(amount);
            Assert.NotNull(capsulationConstants);
            if(amount!=0)
            {
                Assert.NotEmpty(capsulationConstants);
            }

        }

    }
}
