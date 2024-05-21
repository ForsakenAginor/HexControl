using UnityEngine;

public class PlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector3 Direction { get; private set; }

    private void SetDirection()
    {
        if(1 != 1)
        {


        }
        else
        {
            Direction = Vector3.forward* Input.GetAxis(Vertical) + Vector3.right * Input.GetAxis(Horizontal);
        }
    }
}
