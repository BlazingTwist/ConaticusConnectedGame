using DataTypes;
using NUnit.Framework;

namespace Tests.EditMode.DataTypes {

	public class TestProduct {

		[Test]
		public void TestProductEquals() {
			Product productA = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
					{ ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White },
			});

			Product productB = new(2, 4, new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
					{ ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White },
			});

			Assert.AreEqual(productA, productB);
		}

		[Test]
		public void TestProductEquals_black() {
			Product productA = new(2, 4);

			Product productB = new(new[,] {
					{ ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black },
					{ ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black },
			});

			Assert.AreEqual(productA, productB);
		}

		[Test]
		public void TestProductNotEquals_unequalXDimension() {
			Product productA = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
					{ ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White },
			});

			Product productB = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue, ProductColor.Yellow },
					{ ProductColor.Magenta, ProductColor.Cyan, ProductColor.White, ProductColor.Black, ProductColor.Black },
			});

			Assert.AreNotEqual(productA, productB);
		}

		[Test]
		public void TestProductNotEquals_unequalYDimension() {
			Product productA = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green },
					{ ProductColor.Blue, ProductColor.Yellow, ProductColor.Magenta },
					{ ProductColor.Cyan, ProductColor.White, ProductColor.Black }
			});

			Product productB = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green },
					{ ProductColor.Blue, ProductColor.Yellow, ProductColor.Magenta },
			});

			Assert.AreNotEqual(productA, productB);
		}

		[Test]
		public void TestProductNotEquals_unequalColors() {
			Product productA = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
					{ ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White },
			});

			Product productB = new(new[,] {
					{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
					{ ProductColor.Yellow, ProductColor.Magenta, ProductColor.Blue, ProductColor.White },
			});

			Assert.AreNotEqual(productA, productB);
		}

	}

}
