using DataTypes;
using DataTypes.ProductBiOperators;
using NUnit.Framework;

namespace Tests.EditMode.DataTypes.ProductBiOperators {

	public class TestOperatorAnd {

		[Test]
		public void TestApplyToProduct() {
			Product productA = TestData.GetProductA();
			Product productB = TestData.GetProductB();

			Product expected = new(new[,] {
					{
							ProductColor.White, ProductColor.White, ProductColor.White, ProductColor.White,
							ProductColor.White, ProductColor.White, ProductColor.White, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.Yellow, ProductColor.White, ProductColor.Yellow,
							ProductColor.White, ProductColor.Yellow, ProductColor.White, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.White, ProductColor.Magenta, ProductColor.Magenta,
							ProductColor.White, ProductColor.White, ProductColor.Magenta, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.Yellow, ProductColor.Magenta, ProductColor.Red,
							ProductColor.White, ProductColor.Yellow, ProductColor.Magenta, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.White, ProductColor.White, ProductColor.White,
							ProductColor.Cyan, ProductColor.Cyan, ProductColor.Cyan, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.Yellow, ProductColor.White, ProductColor.Yellow,
							ProductColor.Cyan, ProductColor.Green, ProductColor.Cyan, ProductColor.Black
					}, {
							ProductColor.White, ProductColor.White, ProductColor.Magenta, ProductColor.Magenta,
							ProductColor.Cyan, ProductColor.Cyan, ProductColor.Blue, ProductColor.Black
					}, {
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black,
							ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black
					}
			});

			Assert.AreEqual(expected, IProductBiOperator.ApplyOperator(productA, productB, new OperatorAnd()));
		}

	}

}
