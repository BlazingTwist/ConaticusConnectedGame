using DataTypes;
using DataTypes.ProductBiOperators;
using GameObjects.Tooltips;
using UnityEngine.UI;

namespace GameObjects.Machines {

	public class BiOperatorMachine : AbstractMachine {

		public OperatorMachineSettings.OperatorType OperatorType;

		public Image OperatorIcon;

		private IProductBiOperator _operator;

		private Product _currentProduct;

		public BiOperatorMachine() : base(2, 1) { }

		private void Start() {
			_operator = OperatorMachineSettings.Instance.GetProductBiOperator(OperatorType);
			SetMachineIcon(OperatorIcon, OperatorType);
			
			BiOperatorTooltipTrigger tooltip = OperatorIcon.gameObject.AddComponent<BiOperatorTooltipTrigger>();
			tooltip.OperatorType = OperatorType;
			
			OnInputChanged();
		}

		public override void OnInputChanged() {
			Product[] inputs = new Product[2];
			for (int i = 0; i < inputs.Length; i++) {
				if (InputPoints[i].InboundConnection?.OutputPoint) {
					inputs[i] = InputPoints[i].InboundConnection.OutputPoint.GetOutput();
				}
				int fallbackDim = OperatorMachineSettings.Instance.FallbackProductDim;
				inputs[i] ??= new Product(fallbackDim, fallbackDim);
			}

			_currentProduct = IProductBiOperator.ApplyOperator(inputs[0], inputs[1], _operator);
			DisplayProduct(_currentProduct);
			OnProductChangeDelegate();
		}

		public override Product GetOutput(MachineConnectionPoint outputSlot) {
			VerifyOutputSlot(outputSlot);
			return _currentProduct;
		}

	}

}
