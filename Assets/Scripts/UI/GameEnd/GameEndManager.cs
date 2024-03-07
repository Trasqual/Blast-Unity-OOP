using BlastGame.EventSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace BlastGame.UI.GameEndManagement
{
    public class GameEndManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverCanvasPrefab;
        [SerializeField] private GameObject _gameWonCanvasPrefab;

        private bool _isGameOver = false;

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<OutOfMovesEvent>(OnOutOfMoves);
            EventManager.Instance.AddListener<AllGoalsAreCompletedEvent>(OnAllGoalsCompleted);
        }

        private async void OnOutOfMoves(object eventData)
        {
            if (_isGameOver)
            {
                return;
            }

            _isGameOver = true;
            await Task.Delay(500);
            Instantiate(_gameOverCanvasPrefab);
        }

        private async void OnAllGoalsCompleted(object eventData)
        {
            if (_isGameOver)
            {
                return;
            }

            _isGameOver = true;
            await Task.Delay(500);
            Instantiate(_gameWonCanvasPrefab);
        }

        private void UnsubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<OutOfMovesEvent>(OnOutOfMoves);
            EventManager.Instance.RemoveListener<AllGoalsAreCompletedEvent>(OnAllGoalsCompleted);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
