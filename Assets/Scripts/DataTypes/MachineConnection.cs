using GameObjects;
using UnityEngine;

namespace DataTypes {

	public class MachineConnection {

		public readonly GameObject ConnectionGraphics;
		public readonly MachineConnectionPoint OutputPoint;
		public readonly MachineConnectionPoint InputPoint;

		public MachineConnection(GameObject connectionGraphics, MachineConnectionPoint outputPoint, MachineConnectionPoint inputPoint) {
			ConnectionGraphics = connectionGraphics;
			OutputPoint = outputPoint;
			InputPoint = inputPoint;

			AddProductListener();
		}

		public void ReleaseConnection(bool allowInputChangeEvent = true, bool updateOutputPoint = true) {
			if (ConnectionGraphics) {
				Object.Destroy(ConnectionGraphics);
			}

			RemoveProductListener();
			if (updateOutputPoint && OutputPoint) {
				OutputPoint.ReleaseConnection(this, allowInputChangeEvent);
			}
			if (InputPoint) {
				InputPoint.ReleaseConnection(this, allowInputChangeEvent);
			}
		}

		private void AddProductListener() {
			if (InputPoint && OutputPoint
					&& InputPoint.OwningMachine && OutputPoint.OwningMachine) {
				OutputPoint.OwningMachine.OnProductChangeDelegate += InputPoint.OwningMachine.OnInputChanged;
			}
		}

		private void RemoveProductListener() {
			if (InputPoint && OutputPoint
					&& InputPoint.OwningMachine && OutputPoint.OwningMachine) {
				OutputPoint.OwningMachine.OnProductChangeDelegate -= InputPoint.OwningMachine.OnInputChanged;
			}
		}

	}

}
