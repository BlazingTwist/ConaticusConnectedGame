using System.Collections.Generic;
using DataTypes;

namespace Tests.EditMode.DataTypes.ProductBiOperators {

	public class TestData {

		private static ProductColor[,] GenerateRowWise(IReadOnlyList<ProductColor> rowColors, int numColumns) {
			ProductColor[,] result = new ProductColor[numColumns, rowColors.Count];

			for (int column = 0; column < numColumns; column++) {
				for (int row = 0; row < rowColors.Count; row++) {
					result[column, row] = rowColors[row];
				}
			}

			return result;
		}

		private static ProductColor[,] GenerateColumnWise(IReadOnlyList<ProductColor> columnColors, int numRows) {
			ProductColor[,] result = new ProductColor[columnColors.Count, numRows];

			for (int column = 0; column < columnColors.Count; column++) {
				ProductColor columnColor = columnColors[column];
				for (int row = 0; row < numRows; row++) {
					result[column, row] = columnColor;
				}
			}

			return result;
		}

		/// <summary>
		/// Generates a 8x8 product where each row is one of the 8 colors.
		/// In this order: [White, Yellow, Magenta, Red, Cyan, Green, Blue, Black]
		/// </summary>
		/// <returns>a Product for testing BiOperators</returns>
		public static Product GetProductA() {
			return new Product(
					GenerateRowWise(new[] {
							ProductColor.White, ProductColor.Yellow, ProductColor.Magenta, ProductColor.Red,
							ProductColor.Cyan, ProductColor.Green, ProductColor.Blue, ProductColor.Black
					}, 8)
			);
		}

		/// <summary>
		/// Generates a 8x8 product where each column is one of the 8 colors.
		/// In this order: [White, Yellow, Magenta, Red, Cyan, Green, Blue, Black]
		/// </summary>
		/// <returns>a Product for testing BiOperators</returns>
		public static Product GetProductB() {
			return new Product(
					GenerateColumnWise(new[] {
							ProductColor.White, ProductColor.Yellow, ProductColor.Magenta, ProductColor.Red,
							ProductColor.Cyan, ProductColor.Green, ProductColor.Blue, ProductColor.Black
					}, 8)
			);
		}

	}

}
