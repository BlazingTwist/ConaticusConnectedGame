using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects {

	public class AlertMessage : MonoBehaviour {

		public Image BackgroundImage;

		public TextMeshProUGUI TextMesh;

		public void SetText(string text, float timeout) {
			TextMesh.text = text;
			StartCoroutine(TimeoutCoroutine(timeout, 0.05f));
		}

		private IEnumerator TimeoutCoroutine(float visibleTime, float fadeStep) {
			yield return new WaitForSecondsRealtime(visibleTime);

			int numSteps = (int) Math.Ceiling(1f / fadeStep);
			for (int step = 0; step < numSteps; step++) {
				Color currentColor = BackgroundImage.color;
				currentColor.a -= fadeStep;
				BackgroundImage.color = currentColor;

				currentColor = TextMesh.color;
				currentColor.a -= fadeStep;
				TextMesh.color = currentColor;

				yield return new WaitForFixedUpdate();
			}

			Destroy(gameObject);
		}

	}

}
