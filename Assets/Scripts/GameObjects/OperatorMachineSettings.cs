using System;
using System.Collections.Generic;
using DataTypes.ProductBiOperators;
using DataTypes.ProductOperators;
using UnityEngine;

namespace GameObjects {

	public class OperatorMachineSettings : MonoBehaviour {

		public static OperatorMachineSettings Instance = null;

		public Sprite IconAnd;
		public Sprite IconOr;
		public Sprite IconXor;
		public Sprite IconNot;

		public int FallbackProductDim;

		public Dictionary<OperatorType, Sprite> OperatorIcons;

		public IProductOperator GetProductOperator(OperatorType opType) {
			return opType switch {
					OperatorType.NotOp => new OperatorNot(),
					_ => null
			};
		}

		public IProductBiOperator GetProductBiOperator(OperatorType opType) {
			return opType switch {
					OperatorType.AndOp => new OperatorAnd(),
					OperatorType.OrOp => new OperatorOr(),
					OperatorType.XorOp => new OperatorXor(),
					_ => null
			};
		}

		private void Awake() {
			Instance = this;
			OperatorIcons = new Dictionary<OperatorType, Sprite> {
					[OperatorType.AndOp] = IconAnd,
					[OperatorType.OrOp] = IconOr,
					[OperatorType.XorOp] = IconXor,
					[OperatorType.NotOp] = IconNot
			};
		}

		public enum OperatorType {

			AndOp,
			OrOp,
			XorOp,
			NotOp

		}

	}

}
