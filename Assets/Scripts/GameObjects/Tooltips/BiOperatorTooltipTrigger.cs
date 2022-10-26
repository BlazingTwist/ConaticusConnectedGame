using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameObjects.Tooltips {

	public class BiOperatorTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

		public OperatorMachineSettings.OperatorType OperatorType;

		private IEnumerator _showTooltipCoroutine;

		public void OnPointerEnter(PointerEventData eventData) {
			_showTooltipCoroutine = ShowTooltipLater(0.5f);
			StartCoroutine(_showTooltipCoroutine);
		}

		public void OnPointerExit(PointerEventData eventData) {
			StopCoroutine(_showTooltipCoroutine);
			BiOperatorTooltip.Instance.gameObject.SetActive(false);
		}

		private IEnumerator ShowTooltipLater(float time) {
			yield return new WaitForSecondsRealtime(time);

			BiOperatorTooltip biOperatorTooltip = BiOperatorTooltip.Instance;
			biOperatorTooltip.PrepareDisplay(OperatorType);
			biOperatorTooltip.gameObject.SetActive(true);
		}

	}

}
