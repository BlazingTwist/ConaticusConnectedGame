namespace DataTypes.ProductOperators {

	public class OperatorNot : IProductOperator {

		public ProductColor ApplyOperator(ProductColor colorA) {
			return (ProductColor) (~(int) colorA & 0xffffff);
		}

	}

}
