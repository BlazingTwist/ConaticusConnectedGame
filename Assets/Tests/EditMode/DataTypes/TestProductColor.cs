using DataTypes;
using NUnit.Framework;

namespace Tests.EditMode.DataTypes {

	public class TestProductColor {

		[Test]
		public void TestFlagsCombine() {
			const ProductColor black = ProductColor.Black;
			const ProductColor red = ProductColor.Red;
			const ProductColor green = ProductColor.Green;
			const ProductColor yellow = ProductColor.Yellow;

			Assert.AreEqual(red, (black | red));
			Assert.AreEqual(green, (black | green));
			Assert.AreEqual(yellow, (black | yellow));
			Assert.AreEqual(yellow, (red | green));
			Assert.AreEqual(yellow, (red | yellow));
			Assert.AreEqual(yellow, (green | yellow));
		}

		[Test]
		public void TestArrayDefaultValueIsBlack() {
			ProductColor[,] array = new ProductColor[2, 2];

			for (int x = 0; x < 2; x++) {
				for (int y = 0; y < 2; y++) {
					Assert.AreEqual(ProductColor.Black, array[x, y]);
				}
			}
		}

	}

}
