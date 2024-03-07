using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

namespace BlastGame.UI.GameEndManagement
{
    public class GameOverCanvas : GameEndCanvas
    {
        public void OnRetryButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public override void Execute(Action OnComplete)
        {
            _canvasGroup.DOFade(1, 0.5f).OnComplete(() =>
            {
                OnComplete();
            });
        }
    }
}
