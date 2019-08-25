// Smooth towards the target

using UnityEngine;

public class SmoothFollow : GameBehaviour
{
    private Vector3 _velocity = Vector3.zero;
    public float Dist;
    public float SmoothTime = 0.3F;
    public Transform Target;

    private void Update()
    {
        Dist = Vector3.Distance(Target.position, transform.position);
        if (Dist > 1.2f)
            Player.CharacterController.LockMovment();
        else
            Player.CharacterController.UnlockMovment();
        // Define a target position above and behind the target transform
        var targetPosition = Target.TransformPoint(new Vector3(0, 0, -1));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime);
    }
}