using DataTypes;
using LevelData;

namespace GameObjects.Machines {

	public class SupplierMachine : AbstractMachine {

		public Products.LevelProduct ProductType;

		private Product _product;

		public SupplierMachine() : base(0, 1) { }

		private void Start() {
			_product = Products.GetProduct(ProductType);

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
