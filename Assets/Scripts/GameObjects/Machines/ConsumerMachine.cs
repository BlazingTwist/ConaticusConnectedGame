using System;
using DataTypes;
using LevelData;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Machines {

	public class ConsumerMachine : AbstractMachine {

		public Products.LevelProduct ExpectedProductType;
		public GameObject LedGreen;
		public GameObject LedRed;
		public Image GridExpectedImage;
		public Image ProductExpectedImage;

		private Product _expectedProduct;
		private Product _currentProduct;
		private bool _previousSatisfied;

		public ConsumerMachine() : base(1, 0) { }

		private void Start() {
			_expectedProduct = Products.GetProduct(ExpectedProductType);
			LevelCompletionManager.Instance.RegisterConsumer(this);

			DisplayExpected();
			OnInputChanged();
		}

		private void DisplayExpected() {
			int dim = Math.Max(_expectedProduct.XDim, _expectedProduct.YDim);
			GridExpectedImage.pixelsPerUnitMultiplier = dim;
			GridExpectedImage.SetAllDirty();

			ProductExpectedImage.sprite = Sprite.Create(
					_expectedProduct.Texture,
					new Rect(0, 0, _expectedProduct.Texture.width, _expectedProduct.Texture.height),
					Vector2.zero
			);
			ProductExpectedImage.SetAllDirty();
		}

		private void UpdateSatisfiedState() {
			bool equals = Equals(_expectedProduct, _currentProduct);
			LedGreen.SetActive(equals);
			LedRed.SetActive(!equals);

			if (equals != _previousSatisfied) {
				LevelCompletionManager.Instance.UpdateConsumer(this, equals);
			}

			_previousSatisfied = equals;
		}

		public override void OnInputChanged() {
			// TODO verify that input is correct
			Product input = null;
			if (InputPoints[0].InboundConnection?.OutputPoint) {
				input = InputPoints[0].InboundConnection.OutputPoint.GetOutput();
			}
			int fallbackDim = OperatorMachineSettings.Instance.FallbackProductDim;
			input ??= new Product(fallbackDim, fallbackDim);

			_currentProduct = input;
			DisplayProduct(_currentProduct);
			UpdateSatisfiedState();
		}

		public override Product GetOutput(MachineConnectionPoint outputSlot) {
			return null;
		}

	}

}
