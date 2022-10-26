using System;
using DataTypes;

namespace GameObjects.Machines {

	public class ConstantMachine : AbstractMachine {

		public ProductColor ConstantColor;

		private Product _product;

		public ConstantMachine() : base(0, 1) { }

		private void Start() {
			int dim = OperatorMachineSettings.Instance.FallbackProductDim;
			ProductColor[,] colors = new ProductColor[dim, dim];
			for (int x = 0; x < dim; x++) {
				for (int y = 0; y < dim; y++) {
					colors[x, y] = ConstantColor;
				}
			}
			_product = new Product(dim, dim, colors);
			
			DisplayProduct(_product);
			OnProductChangeDelegate();
		}

		public override void OnInputChanged() { }

		public override Product GetOutput(MachineConnectionPoint outputSlot) {
			VerifyOutputSlot(outputSlot);
			return _product;
		}

	}

}
