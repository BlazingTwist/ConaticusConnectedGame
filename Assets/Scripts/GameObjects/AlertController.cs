using System;
using UnityEngine;

namespace GameObjects {

	public class AlertController : MonoBehaviour {

		public static AlertController Instance;

		public GameObject AlertMessagePrefab;

		public GameObject AlertMessageContainer;

		private void Awake() {
			Instance = this;
		}

		public void AddMessage(string message) {
			GameObject alertMessage = Instantiate(AlertMessagePrefab, AlertMessageContainer.transform);
			AlertMessage messageComponent = alertMessage.GetComponent<AlertMessage>();
			messageComponent.SetText(message, 2f);
		}

		public void AlertNoRecursion() {
			AddMessage("And God said:\n\"Thou shalt not crash my game by creating a recursive machine.\"");
		}

		public void AlertSameConnectionType() {
			AddMessage("Can only connect Outputs (green) to Inputs (red)");
		}

	}

}
