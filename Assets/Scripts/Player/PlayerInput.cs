using UnityEngine;

public class PlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector3 GetDirection()
    {
        if (Input.touchSupported == false)
            return Vector3.forward * Input.GetAxis(Vertical) + Vector3.right * Input.GetAxis(Horizontal);

        if (Input.touchCount == 0)
            return Vector3.zero;

        float verticalCenter = Screen.height / 2;
        float horizontalCenter = Screen.width / 2;
        Vector2 touchPosition = Input.GetTouch(0).position;
        Vector2 direction = touchPosition - new Vector2(horizontalCenter, verticalCenter);

        return new Vector3(direction.x, 0, direction.y);
    }
}
