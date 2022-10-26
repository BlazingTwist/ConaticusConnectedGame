using DataTypes;
using DataTypes.ProductOperators;
using GameObjects.Tooltips;
using UnityEngine.UI;

namespace GameObjects.Machines {

	public class OperatorMachine : AbstractMachine {

		public OperatorMachineSettings.OperatorType OperatorType;

		public Image OperatorIcon;

		private IProductOperator _operator;

		private Product _currentProduct;

		public OperatorMachine() : base(1, 1) { }

		private void Start() {
			_operator = OperatorMachineSettings.Instance.GetProductOperator(OperatorType);
			SetMachineIcon(OperatorIcon, OperatorType);

			OperatorTooltipTrigger tooltip = OperatorIcon.gameObject.AddComponent<OperatorTooltipTrigger>();
			tooltip.OperatorType = OperatorType;

			OnInputChanged();
		}

		public override void OnInputChanged() {
			Product input = null;
			if (InputPoints[0].InboundConnection?.OutputPoint) {
				input = InputPoints[0].InboundConnection.OutputPoint.GetOutput();
			}
			int fallbackDim = OperatorMachineSettings.Instance.FallbackProductDim;
			input ??= new Product(fallbackDim, fallbackDim);

			_currentProduct = IProductOperator.ApplyOperator(input, _operator);
			DisplayProduct(_currentProduct);
			OnProductChangeDelegate();
		}

		public override Product GetOutput(MachineConnectionPoint outputSlot) {
			VerifyOutputSlot(outputSlot);
			return _currentProduct;
		}

	}

}
