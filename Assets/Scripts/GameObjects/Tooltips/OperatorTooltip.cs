using DataTypes;
using DataTypes.ProductOperators;
using LevelData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tooltips {

	public class OperatorTooltip : MonoBehaviour {

		public static OperatorTooltip Instance;

		private static string GetTitleText(OperatorMachineSettings.OperatorType operatorType) {
			return operatorType switch {
					OperatorMachineSettings.OperatorType.NotOp => "Complement",
					_ => null
			};
		}

		private static string GetDescriptionText(OperatorMachineSettings.OperatorType operatorType) {
			return operatorType switch {
					OperatorMachineSettings.OperatorType.NotOp => "Outputs the Complement of the input color",
					_ => null
			};
		}

		private static void SetMachineIcon(Image image, OperatorMachineSettings.OperatorType operatorType) {
			Sprite iconSprite = OperatorMachineSettings.Instance.OperatorIcons[operatorType];
			image.sprite = iconSprite;
			image.rectTransform.sizeDelta = new Vector2(iconSprite.textureRect.width, iconSprite.textureRect.height);
			image.SetAllDirty();
		}

		private static void DisplayProduct(int dim, Product product, Image gridImage, Image productImage) {
			gridImage.pixelsPerUnitMultiplier = dim;
			gridImage.SetAllDirty();

			productImage.sprite = Sprite.Create(product.Texture, new Rect(0, 0, product.Texture.width, product.Texture.height), Vector2.zero);
			productImage.SetAllDirty();
		}

		public TextMeshProUGUI TitleText;
		public TextMeshProUGUI DescriptionText;

		public Image TitleIcon;
		public Image TransferIcon;

		public Image GridParam1;
		public Image GridOutput;

		public Image ProductParam1;
		public Image ProductOutput;

		private RectTransform _rectTransform;

		private void Awake() {
			Instance = this;
			_rectTransform = GetComponent<RectTransform>();
			gameObject.SetActive(false);
		}

		private void Update() {
			Vector2 position = Input.mousePosition;
			float pivotX = position.x / Screen.width;
			float pivotY = position.y / Screen.height;
			_rectTransform.pivot = new Vector2(pivotX, pivotY);
			transform.position = position;
		}

		public void PrepareDisplay(OperatorMachineSettings.OperatorType operatorType) {
			TitleText.text = GetTitleText(operatorType);
			DescriptionText.text = GetDescriptionText(operatorType);
			SetMachineIcon(TitleIcon, operatorType);
			SetMachineIcon(TransferIcon, operatorType);


			const int dim = 8;
			Product colSample = Products.GetProduct(Products.LevelProduct.SampleColumnTile8X8);
			DisplayProduct(dim, colSample, GridParam1, ProductParam1);

			IProductOperator @operator = OperatorMachineSettings.Instance.GetProductOperator(operatorType);
			Product resultProduct = IProductOperator.ApplyOperator(colSample, @operator);
			DisplayProduct(dim, resultProduct, GridOutput, ProductOutput);
		}

	}

}
