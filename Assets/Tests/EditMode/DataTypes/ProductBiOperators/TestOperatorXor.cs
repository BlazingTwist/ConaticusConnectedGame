using DataTypes;
using DataTypes.ProductBiOperators;
using NUnit.Framework;

namespace Tests.EditMode.DataTypes.ProductBiOperators {

	public class TestOperatorXor {

		[Test]
		public void TestApplyToProduct() {
			Product productA = TestData.GetProductA();
			Product productB = TestData.GetProductB();

			Product expected = new(new[,] {
					{
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.White
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Yellow
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Magenta
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Red
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Cyan
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Green
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Blue
					}, {
							ProductColor.White, ProductColor.Yellow, ProductColor.Magenta, ProductColor.Red,
							ProductColor.Cyan, ProductColor.Green, ProductColor.Blue, ProductColor.Black
					}
			});

			Assert.AreEqual(expected, IProductBiOperator.ApplyOperator(productA, productB, new OperatorXor()));
		}

	}

}
