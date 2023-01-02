using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
	[SerializeField]
	private float acceleration;
	[SerializeField]
	private float deAcceleration;

	private Vector3 direction;
	private float velocity;

    private Rigidbody2D rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		Move(new Vector3(x, y));
	}

	private void FixedUpdate()
	{
		rigid.velocity = direction * velocity;
	}

	private void Move(Vector3 input)
	{
		direction = input.normalized;
		velocity = VelocityCalculator(input);
	}

	private float VelocityCalculator(Vector3 direction)
	{
		if (direction.sqrMagnitude > 0)
		{
			velocity += Time.deltaTime * acceleration;
		}
		else
		{
			velocity -= Time.deltaTime * deAcceleration;
		}

		return Mathf.Clamp(velocity, 0, maxSpeed);
	}
}
