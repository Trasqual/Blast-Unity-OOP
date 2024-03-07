using DG.Tweening;
using UnityEngine;

namespace BlastGame.UI.Animations
{
    public class PunchScaleUIAnimation : UIAnimationBase
    {
        [SerializeField] private float _scale = 0.5f;
        [SerializeField] private float _duration = 0.25f;
        [SerializeField] private int _vibrato = 8;

        private Tween _punchScaleTween;

        protected override void PlayAnimation()
        {
            _punchScaleTween?.Kill();

            transform.localScale = Vector3.one;

            _punchScaleTween = transform.DOPunchScale(Vector3.one * _scale, _duration, _vibrato).OnComplete(() =>
            {
                transform.localScale = Vector3.one;
            });
        }
    }
}
