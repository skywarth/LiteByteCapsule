using System;
using Xunit;
using Moq;
using LiteByte;

namespace XUnitTestNETCore1

{
    public class CapsuleConstantTest
    {
        [Fact]
        public void BaseConstructorRandom()
        {
            Random rnd = new Random();
            byte val = (byte)rnd.Next(0, 255);
            int pos = rnd.Next();
            bool head;
            if(rnd.NextDouble() >= 0.5)
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
            Assert.Equal(constantObj1,constantObj2);
            Assert.InRange(constantObj1.Val, 0, 255);
            Assert.InRange(constantObj1.Position, 0, int.MaxValue);
            


           
        }
        [Fact]
        public void BaseConstructorNegative()
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
            Assert.Throws<ArgumentOutOfRangeException>(delegate { CapsuleConstant constant = new CapsuleConstant(val, pos, head); });

        }

    }
}
