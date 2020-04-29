using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	private static readonly int Running = Animator.StringToHash("Running");
	private static readonly int Climbing = Animator.StringToHash("Climbing");
	
	private Animator _myAnimator;
	private Rigidbody2D _myRigidBody;
	private Collider2D _myCollider;

	[SerializeField] private float climbSpeed = 5f;
	[SerializeField] private float jumpSpeed = 5f;
	[SerializeField] private float runSpeed = 5f;

	// Start is called before the first frame update
	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
		_myAnimator = GetComponent<Animator>();
		_myCollider = GetComponent<Collider2D>();
		Debug.Log(LayerMask.GetMask("Default", "Ground"));
	}

	// Update is called once per frame
	private void Update()
	{
		Run();
		Jump();
		ClimbLadder();
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

	private void ClimbLadder()
	{
		if (!_myCollider.IsTouchingLayers(LayerMask.GetMask("Default", "Climbing")))
		{
			return;
		}

		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value is -1 to +1
		var climbVelocity = new Vector2(_myRigidBody.velocity.x, controlThrow * climbSpeed);
		_myRigidBody.velocity = climbVelocity;
		bool playerHasVerticalSpeed = PlayerHasVerticalSpeed();
		_myAnimator.SetBool(Climbing, playerHasVerticalSpeed);
	}


	private void Jump()
	{
		if (!_myCollider.IsTouchingLayers(LayerMask.GetMask("Default", "Ground")))
		{
			return;
		}

		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);

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
		return Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;
	}

	private bool PlayerHasVerticalSpeed()
	{
		return Mathf.Abs(_myRigidBody.velocity.y) > Mathf.Epsilon;
	}
}