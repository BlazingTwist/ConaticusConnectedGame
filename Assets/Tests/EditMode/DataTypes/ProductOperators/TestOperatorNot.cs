using DataTypes;
using DataTypes.ProductOperators;
using NUnit.Framework;

namespace Tests.EditMode.DataTypes.ProductOperators {

	public class TestOperatorNot {

		[Test]
		public void TestApplyOperator() {
			OperatorNot @operator = new();

			Assert.AreEqual(ProductColor.Black, @operator.ApplyOperator(ProductColor.White));
			Assert.AreEqual(ProductColor.Red, @operator.ApplyOperator(ProductColor.Cyan));
			Assert.AreEqual(ProductColor.Green, @operator.ApplyOperator(ProductColor.Magenta));
			Assert.AreEqual(ProductColor.Blue, @operator.ApplyOperator(ProductColor.Yellow));
			Assert.AreEqual(ProductColor.Yellow, @operator.ApplyOperator(ProductColor.Blue));
			Assert.AreEqual(ProductColor.Magenta, @operator.ApplyOperator(ProductColor.Green));
			Assert.AreEqual(ProductColor.Cyan, @operator.ApplyOperator(ProductColor.Red));
			Assert.AreEqual(ProductColor.White, @operator.ApplyOperator(ProductColor.Black));
		}

		[Test]
         		public void TestApplyToProduct() {
         			Product start = new(new[,] {
         					{
         							ProductColor.Black,
         							ProductColor.Red,
         							ProductColor.Green,
         							ProductColor.Blue,
         							ProductColor.Yellow,
         							ProductColor.Magenta,
         							ProductColor.Cyan,
         							ProductColor.White
         					}
         			});
         
         			Product expected = new(new[,] {
         					{
         							ProductColor.White,
         							ProductColor.Cyan,
         							ProductColor.Magenta,
         							ProductColor.Yellow,
         							ProductColor.Blue,
         							ProductColor.Green,
         							ProductColor.Red,
         							ProductColor.Black
         					}
         			});
         			
         			Assert.AreEqual(expected, IProductOperator.ApplyOperator(start, new OperatorNot()));
         		}

	}

}
