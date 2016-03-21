using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	public float MovementSpeed;
	public float MaxSpeed;
	public float JumpHeight;
	public float distToGround;
	public bool ControlLayout;
    public float BrakeForce;

    private bool CanJump;
	private Rigidbody2D PhysicBody;


	private void Start()
    {
		PhysicBody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
		float MovementX, MovementY;
		if (ControlLayout == false)
		{
			MovementX = Input.GetAxisRaw("Horizontal");
			MovementY = Input.GetAxisRaw("Vertical");
		}
		else
		{
			MovementX = Input.GetAxisRaw("Horizontal 2nd");
			MovementY = Input.GetAxisRaw("Vertical 2nd");
		}

		float VelocitySign = (MovementX >= 0.0f) ? 1.0f : -1.0f;
		PhysicBody.velocity += new Vector2(MovementX, 0.0f) * MovementSpeed;
		if (Mathf.Abs(PhysicBody.velocity.x) > MaxSpeed)
		{
			PhysicBody.velocity = new Vector2(MaxSpeed * VelocitySign, PhysicBody.velocity.y);
		}

		bool WantsJump = (MovementY == 1.0f);
		if (IsGrounded() && WantsJump)
		{
			PhysicBody.velocity = new Vector2(PhysicBody.velocity.x, JumpHeight);
		}

        Brake();
    }

	private bool IsGrounded()
	{
		RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.05f, 0x100);
		return (HitInfo.collider != null);
	}

    private void Brake()
    {
        if (IsGrounded())
        {
            // Apply brake force if grounded
            int Sign = Math.Sign(PhysicBody.velocity.x);
            float Module = Math.Abs(PhysicBody.velocity.x);

            if (Module > 0.0f)
            {
                // Apply brake
                PhysicBody.velocity = new Vector2((Module - BrakeForce) * Sign, PhysicBody.velocity.y);

                // Finally stop player movement, if module has changed
                if (Math.Sign(PhysicBody.velocity.x) != Sign)
                {
                    PhysicBody.velocity = new Vector2(0.0f, PhysicBody.velocity.y);
                }
            }
        }
    }
}
