using System;
using System.Text;
using UnityEngine;

namespace DataTypes {

	public class Product {

		public static ProductColor[,] ConvertFromEditorLayout(ProductColor[,] editorColors) {
			int editorXDim = editorColors.GetLength(0);
			int editorYDim = editorColors.GetLength(1);

			ProductColor[,] result = new ProductColor[editorYDim, editorXDim];
			for (int x = 0; x < editorXDim; x++) {
				for (int y = 0; y < editorYDim; y++) {
					int resultY = (editorYDim - 1) - x;
					result[y, resultY] = editorColors[x, y];
				}
			}
			return result;
		}

		private static Texture2D GenerateTexture(int xDim, int yDim, ProductColor[,] colors) {
			Texture2D result = new(xDim, yDim, TextureFormat.RGB24, false);

			Color[] colorsArray = new Color[colors.Length];
			int arrIdx = 0;
			for (int y = 0; y < yDim; y++) {
				for (int x = 0; x < xDim; x++) {
					ProductColor color = colors[x, y];
					colorsArray[arrIdx++] = color == ProductColor.Black
							? new Color(0.066f, 0.066f, 0.066f)
							: new Color(
									(((int) color & 0xff0000) >> 16) / 255f,
									(((int) color & 0x00ff00) >> 8) / 255f,
									(((int) color & 0x0000ff) >> 0) / 255f
							);
				}
			}
			result.SetPixels(colorsArray);
			result.filterMode = FilterMode.Point;
			result.Apply();

			return result;
		}

		public Product(int xDim, int yDim) {
			XDim = xDim;
			YDim = yDim;
			Colors = new ProductColor[xDim, yDim];
			Texture = GenerateTexture(xDim, yDim, Colors);
		}

		public Product(ProductColor[,] colors) {
			XDim = colors.GetLength(0);
			YDim = colors.GetLength(1);
			Colors = colors;
			Texture = GenerateTexture(XDim, YDim, colors);
		}

		public Product(int xDim, int yDim, ProductColor[,] colors) {
			XDim = xDim;
			YDim = yDim;
			Colors = colors;
			Texture = GenerateTexture(XDim, YDim, colors);
		}

		public ProductColor[,] GetResizedColors(int xDim, int yDim) {
			if (xDim == XDim && yDim == YDim) {
				return Colors;
			}

			ProductColor[,] result = new ProductColor[xDim, yDim];
			for (int x = 0; x < XDim; x++) {
				for (int y = 0; y < YDim; y++) {
					result[x, y] = Colors[x, y];
				}
			}
			return result;
		}

		public override bool Equals(object obj) {
			return Equals(obj as Product);
		}

		public override int GetHashCode() {
			return HashCode.Combine(XDim, YDim, Colors);
		}

		public override string ToString() {
			ProductColor[,] colors = Colors;
			StringBuilder stringBuilder = new();
			stringBuilder.Append($"xDim: {XDim} | yDim: {YDim}\r\n");
			for (int y = 0; y < YDim; y++) {
				for (int x = 0; x < XDim; x++) {
					string colorName = Enum.GetName(typeof(ProductColor), colors[x, y]);
					if (colorName?.Length > 4) {
						colorName = colorName[..4];
					}
					stringBuilder.Append(colorName).Append("\t");
				}
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		private bool Equals(Product other) {
			if (other == null) {
				return false;
			}

			if (XDim != other.XDim || YDim != other.YDim) {
				return false;
			}

			ProductColor[,] colors = Colors;
			ProductColor[,] otherColors = other.Colors;
			for (int x = 0; x < XDim; x++) {
				for (int y = 0; y < YDim; y++) {
					if (colors[x, y] != otherColors[x, y]) {
						return false;
					}
				}
			}

			return true;
		}

		public int XDim { get; }

		public int YDim { get; }

		public ProductColor[,] Colors { get; }

		public Texture2D Texture { get; }

	}

}
