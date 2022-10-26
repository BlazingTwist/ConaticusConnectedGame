namespace DataTypes.ProductOperators {

	public interface IProductOperator {

		static Product ApplyOperator(Product a, IProductOperator @operator) {
			int xDim = a.XDim;
			int yDim = a.YDim;
			ProductColor[,] colorsA = a.Colors;
			ProductColor[,] result = new ProductColor[xDim, yDim];

			for (int x = 0; x < xDim; x++) {
				for (int y = 0; y < yDim; y++) {
					result[x, y] = @operator.ApplyOperator(colorsA[x, y]);
				}
			}

			return new Product(xDim, yDim, result);
		}

		ProductColor ApplyOperator(ProductColor colorA);

	}

}
