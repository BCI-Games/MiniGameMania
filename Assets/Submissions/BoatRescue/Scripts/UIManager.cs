using TMPro;
using UnityEngine;

namespace Submissions.BoatRescue
{
    public class UIManager : MonoBehaviour
    {
        [Header("Panels")] [SerializeField] private GameObject _levelPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _levelCompletePanel;

        [Header("Text Elements")] [SerializeField]
        private TextMeshProUGUI _levelNumber;

        [SerializeField] private TextMeshProUGUI _movementText;
        [SerializeField] private TextMeshProUGUI _gameWinMovementText;
        [SerializeField] private TextMeshProUGUI _rewardText;

        private void Awake()
        {
            GameManager.Instance.RegisterUIManager(this);
            ResetUI();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance == null)
            {
                return;
            }

            GameManager.Instance.UnregisterUIManager(this);
        }

        public void InitializeLevelDetails(Level level)
        {
            _levelNumber.SetText($"Level {level.LevelNumber}");
        }

        public void UpdateMovementScore(int count)
        {
            _movementText.SetText(count.ToString());
        }

        public void UpdateRewardScore(int count)
        {
            _rewardText.SetText(count.ToString());
        }

        public void ToggleGameOver(bool visible)
        {
            _gameOverPanel.SetActive(visible);

            _levelCompletePanel.SetActive(!visible);
            _levelPanel.SetActive(!visible);
        }

        public void ToggleGameWin(bool visible)
        {
            _levelCompletePanel.SetActive(visible);

            _gameOverPanel.SetActive(!visible);
            _levelPanel.SetActive(!visible);

            _gameWinMovementText.SetText($"Moves: {_movementText.text}");
        }

        public void NextLevel()
        {
            GameManager.Instance.NextLevel();
            ResetUI();
        }

        public void RestartLevel()
        {
            GameManager.Instance.RestartLevel();
            ResetUI();
        }

        private void ResetUI()
        {
            _movementText.SetText("0");
            _rewardText.SetText("0");

            _levelPanel.SetActive(true);
            _gameOverPanel.SetActive(false);
            _levelCompletePanel.SetActive(false);
        }
    }
}
