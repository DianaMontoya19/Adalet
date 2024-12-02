using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Adalet/Player/Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [Header("Gravity")]
    [Tooltip(
        "Strength of the player's gravity as a multiplier of Unity's gravity. Aswell as the value the player's rigidbody.gravityScale."
    )]
    public float GravityScale;

    [Header("Movement")]
    [Tooltip("Target speed we want the player to reach.")]
    public float MaxSpeed;

    [Tooltip("The maximum angle in a slope the player can walk on.")]
    public float MaxSlopeAngle;

    [Space]
    [Header("Walking")]
    [Tooltip("The speed at which the player accelerates to max speed.")]
    [SerializeField]
    private float WalkAcceleration;

    [ReadOnly]
    [Tooltip("The actual acceleration force applied to the player.")]
    public float WalkAccelAmount;

    [Space(5)]
    [SerializeField]
    [Tooltip("The speed at which the player decelerates from their current speed.")]
    private float WalkDecceleration;

    [ReadOnly]
    [Tooltip("The actual decceleration force applied to the player.")]
    public float WalkDecelAmount;

    [Space]
    [Header("Running")]
    [Tooltip("The acceleration mutiplier applied to the player WalkAccelAmount.")]
    public float RunAccelMult;

    [Tooltip("The deceleration mutiplier applied to the player WalkDeccelAmount.")]
    public float RunDecelMult;

    [Space]
    [Header("Ground")]
    [Tooltip("The amount of drag applied to the player's rigidbody.")]
    public float GroundDrag;

    [Tooltip("The ground physics layer.")]
    public LayerMask GroundLayer;

    private void OnValidate()
    {
        WalkAccelAmount = 20 * WalkAcceleration / MaxSpeed;
        WalkDecelAmount = 20 * WalkDecceleration / MaxSpeed;

        WalkAcceleration = Mathf.Clamp(WalkAcceleration, 0.01f, MaxSpeed);
        WalkDecceleration = Mathf.Clamp(WalkDecceleration, 0.01f, MaxSpeed);
    }
}
