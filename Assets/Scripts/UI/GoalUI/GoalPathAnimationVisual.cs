using BlastGame.BoardItems.Data;
using BlastGame.ObjectPoolingSystem;
using BlastGame.ServiceManagement;
using BlastGame.UI.Animations;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlastGame.UI.GoalManagement
{
    public class GoalPathAnimationVisual : GoalAnimationVisualBase
    {
        [SerializeField] private Image _goalImage;
        [SerializeField] private float _animationDuration;

        const float MOVE_BACK_AMOUNT = 1.5f;
        const float MOVE_BACK_DURATION = 0.15f;
        const float RIGHT_CONTROL_POINT_X_OFFSET = 3f;
        const float RIGHT_CONTROL_POINT_Y_OFFSET = 1.5f;
        const float TOP_CONTROL_POINT_X_OFFSET = 1.5f;
        const float TOP_CONTROL_POINT_Y_OFFSET = 1.5f;

        public override void Initialize(ItemData data, IUIAnimationEndPoint endPoint)
        {
            base.Initialize(data, endPoint);

            _goalImage.sprite = data.GetInitialSprite();
            transform.localScale = Vector3.one;
        }

        public override void Execute(Action OnComplete)
        {
            TravelPath(OnComplete);
        }

        private void TravelPath(Action OnComplete)
        {
            Sequence s = DOTween.Sequence();

            Vector3 moveBackAmount = (_animationEndPoint.GetPosition() - transform.position).normalized * MOVE_BACK_AMOUNT;

            s.Append(transform.DOMove(-moveBackAmount, MOVE_BACK_DURATION).SetRelative()
                .OnComplete(() =>
                {
                    OnComplete?.Invoke();
                }));
            s.Append(transform.DOPath(GetBezierPoints(), _animationDuration, PathType.CubicBezier));
            s.Join(transform.DOScale(0.25f, _animationDuration));
            s.OnComplete(() =>
            {
                _animationEndPoint.OnReached();

                ServiceLocator.Instance.GetService<IObjectPoolManager>().ReleaseObject(this);
            });
        }

        private Vector3[] GetBezierPoints()
        {
            Vector3[] pathPoints = new Vector3[3];

            pathPoints[0] = _animationEndPoint.GetPosition();
            pathPoints[1] = transform.position + Vector3.right * RIGHT_CONTROL_POINT_X_OFFSET + Vector3.up * RIGHT_CONTROL_POINT_Y_OFFSET;
            pathPoints[2] = _animationEndPoint.GetPosition() + Vector3.right * TOP_CONTROL_POINT_X_OFFSET - Vector3.up * TOP_CONTROL_POINT_Y_OFFSET;

            return pathPoints;
        }
    }
}
