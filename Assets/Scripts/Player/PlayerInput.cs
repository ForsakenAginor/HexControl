using Agava.WebUtility;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInput : IPlayerInput
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector3 GetDirection()
        {
            if (Device.IsMobile == false)
                return ((Vector3.forward * Input.GetAxis(Vertical)) + (Vector3.right * Input.GetAxis(Horizontal))).normalized;

            if (Input.touchCount == 0)
                return Vector3.zero;

            float verticalCenter = Screen.height / 2f;
            float horizontalCenter = Screen.width / 2f;
            Vector2 touchPosition = Input.GetTouch(0).position;
            Vector2 direction = new((touchPosition.x - horizontalCenter) / touchPosition.x, (touchPosition.y - verticalCenter) / touchPosition.y);

            return new Vector3(direction.x, 0, direction.y).normalized;
        }
    }

    public interface IPlayerInput
    {
        public Vector3 GetDirection();

        public virtual void DisposeDisposable()
        {

        }
    }

    public class NewPlayerInput : IPlayerInput
    {
        private MyInputActions _inputActions;

        public NewPlayerInput()
        {
            _inputActions = new MyInputActions();
            _inputActions.Player.Enable();
        }

        public Vector3 GetDirection()
        {
            Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
            return ((Vector3.forward * input.y) + (Vector3.right * input.x)).normalized;
        }

        public void DisposeDisposable()
        {
            _inputActions.Player.Disable();
            _inputActions.Dispose();
        }
    }
}