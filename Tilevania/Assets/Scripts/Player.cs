using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	//State
	
	//Cached component references
	private Rigidbody2D _myRigidBody;
	private Animator _myAnimator;

	//Config
	[SerializeField] private float runSpeed = 5f;
	[SerializeField] private float jumpSpeed = 5f;
	private static readonly int Running = Animator.StringToHash("Running");


	//Messages
	
	//Methods

	// Start is called before the first frame update
	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
		_myAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	private void Update()
	{
		Run();
		Jump();
		FlipSprite();
	}

	private void Run()
	{
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is -1 to +1
		var playerVelocity = new Vector2(controlThrow * runSpeed, _myRigidBody.velocity.y);
		_myRigidBody.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = PlayerHasHorizontalSpeed();
		_myAnimator.SetBool(Running, playerHasHorizontalSpeed);
	}

	private void Jump()
	{
		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);

			_myRigidBody.velocity += jumpVelocityToAdd;
		}
	}

	private void FlipSprite()
	{
		bool playerHasHorizontalSpeed = PlayerHasHorizontalSpeed();

		if (playerHasHorizontalSpeed)
		{
			transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);
		}
	}

	private bool PlayerHasHorizontalSpeed()
	{
		return  Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;
	}
}