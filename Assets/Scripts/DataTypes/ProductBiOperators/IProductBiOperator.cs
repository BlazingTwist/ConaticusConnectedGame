using System;

namespace DataTypes.ProductBiOperators {

	public interface IProductBiOperator {

		static Product ApplyOperator(Product a, Product b, IProductBiOperator @operator) {
			int xDim = Math.Max(a.XDim, b.XDim);
			int yDim = Math.Max(a.YDim, b.YDim);

			ProductColor[,] colorsA = a.GetResizedColors(xDim, yDim);
			ProductColor[,] colorsB = b.GetResizedColors(xDim, yDim);
			ProductColor[,] result = new ProductColor[xDim, yDim];

			for (int x = 0; x < xDim; x++) {
				for (int y = 0; y < yDim; y++) {
					result[x, y] = @operator.ApplyOperator(colorsA[x, y], colorsB[x, y]);
				}
			}

			return new Product(xDim, yDim, result);
		}

		ProductColor ApplyOperator(ProductColor colorA, ProductColor colorB);

	}

}
