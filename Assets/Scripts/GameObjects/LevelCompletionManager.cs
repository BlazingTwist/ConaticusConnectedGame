using GameObjects.Machines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameObjects {

	public class LevelCompletionManager : MonoBehaviour {

		public static LevelCompletionManager Instance;

		public GameObject LevelCompleteOverlay;
		public GameObject NextLevelButton;
		public GameObject AllLevelsCompleteText;
		public string NextLevel;

		private int _registeredConsumers;
		private int _satisfiedConsumers;

		private void Awake() {
			Instance = this;
		}

		public void RegisterConsumer(ConsumerMachine machine) {
			_registeredConsumers++;
		}

		public void UpdateConsumer(ConsumerMachine machine, bool satisfied) {
			if (satisfied) {
				_satisfiedConsumers++;
				CheckLevelComplete();
			} else {
				_satisfiedConsumers--;
			}
		}

		public void OnClickNextLevel() {
			SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
		}

		private void CheckLevelComplete() {
			if (_registeredConsumers > 0 && _registeredConsumers == _satisfiedConsumers) {
				ShowLevelComplete();
			}
		}

		private void ShowLevelComplete() {
			LevelCompleteOverlay.SetActive(true);
			bool hasNextLevel = !string.IsNullOrEmpty(NextLevel);
			NextLevelButton.SetActive(hasNextLevel);
			AllLevelsCompleteText.SetActive(!hasNextLevel);
		}

	}

}
