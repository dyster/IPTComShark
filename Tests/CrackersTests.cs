
using BitDataParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitDataParser.Tests
{
    
    public class CrackersTests
    {
        [Theory]
        [InlineData("1111111111111111111111111111111111111111")]
        [InlineData("1100001110011100000000001101101110111101")]
        [InlineData("0000000000000000000000000000000000000000")]
        [InlineData("0100001110011100000000001101101110111100")]
        [InlineData("01000001000011100111000000000011011011101111001110011100000000001101101110111101")]
        public void CrackUInt16Test(string input)
        {
            
            
            

                //var bytes = StringToBytes(input);
                //
                //
                //
                //    for (int x = 1; x <= Math.Min(input.Length - i, 16); x++)
                //    {
                //        var old_v = Functions.FieldGetter(bytes, i, x);
                //        var new_v = Crackers.CrackUInt16(bytes, i); 
                //        Assert.Equal(old_v, new_v);
                //    }
                
            

            Assert.Fail();
        }

        [Theory]
        [InlineData("1111111111111111111111111111111111111111")]
        [InlineData("1100001110011100000000001101101110111101")]
        [InlineData("0000000000000000000000000000000000000000")]
        [InlineData("0100001110011100000000001101101110111100")]
        [InlineData("01000001000011100111000000000011011011101111001110011100000000001101101110111101")]
        public void CrackUInt32Test(string input)
        {
            
            
            

                //var bytes = StringToBytes(input);
                //
                //for (int i = 1; i <= input.Length; i++)
                //{
                //    for (int x = 1; x <= Math.Min(input.Length - i, 32); x++)
                //    {
                //        var old_v = Functions.FieldGetter(bytes, i, x);
                //        var new_v = Functions.FieldGetter(bytes, i, x);
                //        Assert.Equal(old_v, new_v);
                //    }
                //}
            
            Assert.Fail();
        }
                
        public void CrackUInt64Test()
        {
            Assert.Fail();
        }

        
    }
}