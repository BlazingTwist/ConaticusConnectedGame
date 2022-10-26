using System.Collections.Generic;
using DataTypes;

namespace LevelData {

	public static class Products {

		private static readonly Product SampleColumnTile2X2 = new(GenerateColumnWise(
				new[] { ProductColor.Black, ProductColor.White }, 2
		));

		private static readonly Product SampleRowTile2X2 = new(GenerateRowWise(
				new[] { ProductColor.Black, ProductColor.White }, 2
		));

		private static readonly Product SampleColumnTile4X4 = new(GenerateColumnWise(
				new[] { ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue }, 4
		));

		private static readonly Product SampleRowTile4X4 = new(GenerateRowWise(
				new[] { ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue }, 4
		));

		private static readonly Product SampleColumnTile8X8 = new(GenerateColumnWise(
				new[] {
						ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue,
						ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White
				}, 8
		));

		private static readonly Product SampleRowTile8X8 = new(GenerateRowWise(
				new[] {
						ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue,
						ProductColor.Yellow, ProductColor.Magenta, ProductColor.Cyan, ProductColor.White
				}, 8
		));

		private static readonly Product Tutorial1Product1 = new(Product.ConvertFromEditorLayout(new[,] {
				{ ProductColor.Black, ProductColor.White },
				{ ProductColor.Black, ProductColor.Black },
		}));

		private static readonly Product Tutorial1Product2 = new(Product.ConvertFromEditorLayout(new[,] {
				{ ProductColor.White, ProductColor.White },
				{ ProductColor.Black, ProductColor.White },
		}));

		private static readonly Product Tutorial1Product3 = new(Product.ConvertFromEditorLayout(new[,] {
				{ ProductColor.White, ProductColor.Black },
				{ ProductColor.Black, ProductColor.White },
		}));

		private static readonly Product Tutorial2Product1 = new(Product.ConvertFromEditorLayout(new[,] {
				{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
				{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
				{ ProductColor.Black, ProductColor.Red, ProductColor.Green, ProductColor.Blue },
				{ ProductColor.Black, ProductColor.Black, ProductColor.Black, ProductColor.Black }
		}));

		private static readonly Product Level1Product1 = new(Product.ConvertFromEditorLayout(new[,] {
				{
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				}, {
						ProductColor.Black, ProductColor.Red, ProductColor.Black, ProductColor.Black,
						ProductColor.Red, ProductColor.Red, ProductColor.Black, ProductColor.Red
				},
		}));

		public static Product GetProduct(LevelProduct productType) {
			return productType switch {
					LevelProduct.SampleColumnTile2X2 => SampleColumnTile2X2,
					LevelProduct.SampleRowTile2X2 => SampleRowTile2X2,
					LevelProduct.SampleColumnTile4X4 => SampleColumnTile4X4,
					LevelProduct.SampleRowTile4X4 => SampleRowTile4X4,
					LevelProduct.SampleColumnTile8X8 => SampleColumnTile8X8,
					LevelProduct.SampleRowTile8X8 => SampleRowTile8X8,

					LevelProduct.Tutorial1_Product1 => Tutorial1Product1,
					LevelProduct.Tutorial1_Product2 => Tutorial1Product2,
					LevelProduct.Tutorial1_Product3 => Tutorial1Product3,

					LevelProduct.Tutorial2_Product1 => Tutorial2Product1,

					LevelProduct.Level1_Product1 => Level1Product1,
					_ => null
			};
		}

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

		public enum LevelProduct {

			SampleColumnTile2X2,
			SampleRowTile2X2,
			SampleColumnTile4X4,
			SampleRowTile4X4,
			SampleColumnTile8X8,
			SampleRowTile8X8,

			Tutorial1_Product1,
			Tutorial1_Product2,
			Tutorial1_Product3,

			Tutorial2_Product1,

			Level1_Product1,

		}

	}

}
