using GameObjects;

namespace DataTypes {

	public class MachineOutput {

		public readonly AbstractMachine Machine;
		public readonly int OutputID;

		public MachineOutput(AbstractMachine machine, int outputID) {
			Machine = machine;
			OutputID = outputID;
		}

	}

}
