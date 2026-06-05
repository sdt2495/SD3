using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 5f;

    [Header("ダッシュ")]
    public float dashSpeed = 10f;
    public float dashDuration = 2f;
    public float dashCooldown = 3f;

    private Vector2 moveInput;

    public Package currentPackage;

    private bool isDashing;
    private bool isCooldown;

    private float dashTimer;
    private float cooldownTimer;

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        HandleDash();

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        Vector3 move = new Vector3(
            moveInput.x,
            0f,
            moveInput.y);

        transform.Translate(
            move * currentSpeed * Time.deltaTime,
            Space.World);
    }

    private void HandleDash()
    {
        // ダッシュ開始
        if (Keyboard.current.spaceKey.wasPressedThisFrame
            && !isDashing
            && !isCooldown)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        // ダッシュ中
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;

                isCooldown = true;
                cooldownTimer = dashCooldown;
            }
        }

        // クールタイム
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                isCooldown = false;
            }
        }
    }
}