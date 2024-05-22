using UnityEngine;
using Agava.WebUtility;

public class PlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector3 GetDirection()
    {
        if (Device.IsMobile == false)
            return (Vector3.forward * Input.GetAxis(Vertical) + Vector3.right * Input.GetAxis(Horizontal)).normalized;

        if (Input.touchCount == 0)
            return Vector3.zero;

        float verticalCenter = Screen.height / 2f;
        float horizontalCenter = Screen.width / 2f;
        Vector2 touchPosition = Input.GetTouch(0).position;
        Vector2 direction = new Vector2( (touchPosition.x - horizontalCenter) / touchPosition.x , (touchPosition.y - verticalCenter) / touchPosition.y);

        return new Vector3(direction.x, 0, direction.y).normalized;
    }
}
