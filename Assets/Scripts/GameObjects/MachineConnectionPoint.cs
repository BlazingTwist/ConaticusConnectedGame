using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;

namespace GameObjects {

	public class MachineConnectionPoint : MonoBehaviour {

		public Type ConnectionType;

		[HideInInspector] public AbstractMachine OwningMachine;

		[HideInInspector] public int MachineSlotID;

		/// <summary>
		/// Only populated if this is a Input-Point (sorry, nasty code | TODO)
		/// </summary>
		private MachineConnection _inboundConnection = null;

		/// <summary>
		/// Only populated if this is a Output-Point (sorry, nasty code | TODO)
		/// </summary>
		private readonly List<MachineConnection> _outboundConnections = new();

		public bool HasMachineAsInput(AbstractMachine target) {
			if (OwningMachine == target) {
				return true;
			}
			if (OwningMachine == null) {
				Debug.LogError("WHAT THE FUCK!");
			}
			
			IReadOnlyList<MachineConnectionPoint> inputPoints = OwningMachine.GetAllInputPoints();
			if (inputPoints.Count <= 0) {
				return false;
			}
			return inputPoints.Any(point =>
					point.InboundConnection?.OutputPoint
					&& point.InboundConnection.OutputPoint.HasMachineAsInput(target)
			);
		}

		public void Connect(MachineConnectionPoint other, GameObject connectionGraphics) {
			Debug.Assert(ConnectionType != other.ConnectionType);

			if (ConnectionType != Type.Input) {
				other.Connect(this, connectionGraphics);
				return;
			}

			_inboundConnection?.ReleaseConnection(false);

			if (other && other.OwningMachine) {
				MachineConnection connection = new(connectionGraphics, other, this);
				_inboundConnection = connection;
				other._outboundConnections.Add(connection);
			}

			OwningMachine.OnInputChanged();
		}

		public Product GetOutput() {
			if (ConnectionType != Type.Output) {
				return null;
			}
			return OwningMachine ? OwningMachine.GetOutput(this) : null;
		}

		public void ReleaseConnection(MachineConnection connection, bool allowInputChangeEvent) {
			if (ConnectionType == Type.Input) {
				bool hadInboundConnection = _inboundConnection != null;
				_inboundConnection = null;
				if (allowInputChangeEvent && hadInboundConnection && OwningMachine) {
					OwningMachine.OnInputChanged();
				}
			} else {
				_outboundConnections.Remove(connection);
			}
		}

		public void ReleaseAllConnections() {
			if (ConnectionType == Type.Input) {
				_inboundConnection?.ReleaseConnection();
			} else {
				HashSet<AbstractMachine> affectedMachines = new();
				foreach (MachineConnection outConnection in _outboundConnections.Where(outConnection => outConnection != null)) {
					if (outConnection.InputPoint && outConnection.InputPoint.OwningMachine) {
						affectedMachines.Add(outConnection.InputPoint.OwningMachine);
					}
					outConnection.ReleaseConnection(false, false);
				}
				_outboundConnections.Clear();

				foreach (AbstractMachine affectedMachine in affectedMachines) {
					affectedMachine.OnInputChanged();
				}
			}
		}

		public MachineConnection InboundConnection => _inboundConnection;

		public enum Type {

			Input,
			Output

		}

	}

}
