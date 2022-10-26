using DataTypes;
using DataTypes.ProductBiOperators;
using LevelData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects.Tooltips {

	public class BiOperatorTooltip : MonoBehaviour {

		public static BiOperatorTooltip Instance;

		private static string GetTitleText(OperatorMachineSettings.OperatorType operatorType) {
			return operatorType switch {
					OperatorMachineSettings.OperatorType.AndOp => "Combinator\nType: \"And\"",
					OperatorMachineSettings.OperatorType.OrOp => "Combinator\nType: \"Or\"",
					OperatorMachineSettings.OperatorType.XorOp => "Combinator\nType: \"Xor\"",
					_ => null
			};
		}

		private static string GetDescriptionText(OperatorMachineSettings.OperatorType operatorType) {
			return operatorType switch {
					OperatorMachineSettings.OperatorType.AndOp => "Combines the input colors if both are present (not black)",
					OperatorMachineSettings.OperatorType.OrOp => "Adds both input colors to the output, combining them",
					OperatorMachineSettings.OperatorType.XorOp => "Combines the input colors if only one of them is present",
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
		public Image GridParam2;
		public Image GridOutput;

		public Image ProductParam1;
		public Image ProductParam2;
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
			Product rowSample = Products.GetProduct(Products.LevelProduct.SampleRowTile8X8);
			DisplayProduct(dim, colSample, GridParam1, ProductParam1);
			DisplayProduct(dim, rowSample, GridParam2, ProductParam2);

			IProductBiOperator biOperator = OperatorMachineSettings.Instance.GetProductBiOperator(operatorType);
			Product resultProduct = IProductBiOperator.ApplyOperator(colSample, rowSample, biOperator);
			DisplayProduct(dim, resultProduct, GridOutput, ProductOutput);
		}

	}

}
