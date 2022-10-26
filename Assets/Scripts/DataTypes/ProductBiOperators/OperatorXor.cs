namespace DataTypes.ProductBiOperators {

	public class OperatorXor : IProductBiOperator {

		public ProductColor ApplyOperator(ProductColor colorA, ProductColor colorB) {
			bool colorBitA = colorA != ProductColor.Black;
			bool colorBitB = colorB != ProductColor.Black;

			return (colorBitA ^ colorBitB) ? colorA | colorB : ProductColor.Black;
		}

	}

}
