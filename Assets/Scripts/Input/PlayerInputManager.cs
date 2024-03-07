using BlastGame.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlastGame.InputManagement
{
    public class PlayerInputManager : InputBase
    {
        private PlayerInputActions _input;

        private Camera _cam;

        private Vector2 MousePosition => _input.Player.Position.ReadValue<Vector2>();

        private bool _isEnabled;

        private void Awake()
        {
            _cam = Camera.main;
            _input = new PlayerInputActions();

            _input.Player.Enable();

            EnableInput();

            _input.Player.Click.performed += OnClickDownDetected;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<OutOfMovesEvent>(OnGameEnd);
            EventManager.Instance.AddListener<AllGoalsAreCompletedEvent>(OnGameEnd);
            EventManager.Instance.AddListener<GameLoopHasStartedEvent>(OnGameLoopStart);
            EventManager.Instance.AddListener<GameLoopHasEndedEvent>(OnGameLoopEnd);
        }


        private void OnClickDownDetected(InputAction.CallbackContext ctx)
        {
            if (!_isEnabled)
            {
                return;
            }
            EventManager.Instance.TriggerEvent<ClickEvent>(new ClickEvent { Position = GetMouseWorldPosition() });
        }

        private Vector3 GetMouseWorldPosition()
        {
            return _cam.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, 10f));
        }

        private void OnGameLoopStart(object eventData)
        {
            DisableInput();
        }

        private void OnGameLoopEnd(object eventData)
        {
            EnableInput();
        }

        private void OnGameEnd(object eventData)
        {
            DisableCompletely();
        }

        public void DisableCompletely()
        {
            _input.Player.Disable();
        }

        public void EnableInput()
        {
            _isEnabled = true;
        }

        public void DisableInput()
        {
            _isEnabled = false;
        }

        private void UnsubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<OutOfMovesEvent>(OnGameEnd);
            EventManager.Instance.RemoveListener<AllGoalsAreCompletedEvent>(OnGameEnd);
            EventManager.Instance.RemoveListener<GameLoopHasStartedEvent>(OnGameLoopStart);
            EventManager.Instance.RemoveListener<GameLoopHasEndedEvent>(OnGameLoopEnd);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
