using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects {

	public abstract class AbstractMachine : MonoBehaviour {

		protected static void SetMachineIcon(Image image, OperatorMachineSettings.OperatorType operatorType) {
			Sprite iconSprite = OperatorMachineSettings.Instance.OperatorIcons[operatorType];
			image.sprite = iconSprite;
			image.rectTransform.sizeDelta = new Vector2(iconSprite.textureRect.width, iconSprite.textureRect.height);
			image.SetAllDirty();
		}

		public readonly int NumInputs;
		public readonly int NumOutputs;

		public Image GridImage;
		public Image ProductImage;

		protected readonly MachineConnectionPoint[] InputPoints;
		protected readonly MachineConnectionPoint[] OutputPoints;

		protected AbstractMachine(int numInputs, int numOutputs) {
			NumInputs = numInputs;
			NumOutputs = numOutputs;

			InputPoints = new MachineConnectionPoint[numInputs];
			OutputPoints = new MachineConnectionPoint[numOutputs];
		}

		private void Awake() {
			int inputID = 0;
			int outputID = 0;
			foreach (MachineConnectionPoint connectionPoint in GetComponentsInChildren<MachineConnectionPoint>(true)) {
				connectionPoint.OwningMachine = this;
				if (connectionPoint.ConnectionType == MachineConnectionPoint.Type.Input) {
					connectionPoint.MachineSlotID = inputID;
					InputPoints[inputID] = connectionPoint;
					inputID++;
				} else {
					connectionPoint.MachineSlotID = outputID;
					OutputPoints[outputID] = connectionPoint;
					outputID++;
				}
			}

			if (inputID != NumInputs) {
				throw new ArgumentException($"Machine expected to find {NumInputs} inputs, but found {inputID}");
			}
			if (outputID != NumOutputs) {
				throw new ArgumentException($"Machine expected to find {NumOutputs} outputs, but found {outputID}");
			}
		}

		public IReadOnlyList<MachineConnectionPoint> GetAllInputPoints() {
			return InputPoints;
		}

		public IReadOnlyList<MachineConnectionPoint> GetAllOutputPoints() {
			return InputPoints;
		}

		public OnProductChanged OnProductChangeDelegate = () => { };

		protected void DisplayProduct(Product product) {
			int dim = Math.Max(product.XDim, product.YDim);
			GridImage.pixelsPerUnitMultiplier = dim;
			GridImage.SetAllDirty();

			ProductImage.sprite = Sprite.Create(product.Texture, new Rect(0, 0, product.Texture.width, product.Texture.height), Vector2.zero);
			ProductImage.SetAllDirty();
		}

		protected void VerifyOutputSlot(MachineConnectionPoint outputSlot) {
			if (!OutputPoints.Contains(outputSlot)) {
				throw new ArgumentException("GetOutput received outputSlot that did not belong to this Machine!");
			}
		}

		public abstract void OnInputChanged();

		public abstract Product GetOutput(MachineConnectionPoint outputSlot);

		public delegate void OnProductChanged();

	}

}
