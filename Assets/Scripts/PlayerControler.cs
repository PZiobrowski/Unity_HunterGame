
using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{

	public CharacterController characterControler;

	public float actualSpeed = 4.0f;
	public float walkingSpeed = 4.0f;
	public float runningSpeed = 10.0f;
	public float crouchingSpeed = 2.0f;

	public float normalHigh = 1.0f;
	public float jumpHigh = 5.0f;
	public float actualHigh = 1.0f;
	public float crouchHigh = -2.5f;

	public bool isCrouching = false;

	public float mouseSensitivity = 2.0f;
	public float mouseUpDown = 0.0f;
	public float mouseRange = 50.0f;

	// Use this for initialization
	void Start()
	{
		characterControler = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		move();
		rotate();
	}

	private void move()
	{

		float moveBackForward = Input.GetAxis("Vertical") * actualSpeed;
		float moveRightLeft = Input.GetAxis("Horizontal") * actualSpeed;

		

		if (characterControler.isGrounded && Input.GetKeyDown("left ctrl"))
		{
			if (isCrouching)
			{
				isCrouching = false;
				actualSpeed = walkingSpeed;
				//actualHigh = normalHigh;
			}
			else
			{
				isCrouching = true;
				actualHigh = crouchHigh;
			}

		}

		if (isCrouching)
		{
			
			actualSpeed = crouchingSpeed;
		}
		else
		{
			

			if (characterControler.isGrounded && Input.GetButton("Jump"))
			{
				actualHigh = jumpHigh;
			}
			else if (!characterControler.isGrounded)
			{
				actualHigh += Physics.gravity.y * Time.deltaTime;
			}

			if (characterControler.isGrounded && Input.GetKeyDown("left shift"))
			{
				actualSpeed = runningSpeed;
			}
			else if (characterControler.isGrounded && Input.GetKeyUp("left shift"))
			{
				actualSpeed = walkingSpeed;
			}

		}


		


		Vector3 move = new Vector3(moveRightLeft, actualHigh, moveBackForward);
		move = transform.rotation * move;
		characterControler.Move(move * Time.deltaTime);
	}

	private void rotate()
	{

		float mouseRightLeft = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, mouseRightLeft, 0);

		mouseUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		mouseUpDown = Mathf.Clamp(mouseUpDown, -mouseRange, mouseRange);

		Camera.main.transform.localRotation = Quaternion.Euler(mouseUpDown, 0, 0);
	}

}



