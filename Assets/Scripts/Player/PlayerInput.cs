using UnityEngine;

public class PlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector3 Direction => Vector3.forward * Input.GetAxis(Vertical) + Vector3.right * Input.GetAxis(Horizontal);
}
