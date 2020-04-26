using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	private const string Horizontal = "Horizontal";
	private Rigidbody2D _myRigidBody;

	[SerializeField] private float runSpeed = 1f;

	// Start is called before the first frame update
	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	private void Update()
	{
		Run();
		FlipSprite();
	}

	private void Run()
	{
		float controlThrow = CrossPlatformInputManager.GetAxis(Horizontal); //value is -1 to +1
		var playerVelocity = new Vector2(controlThrow * runSpeed, _myRigidBody.velocity.y);
		_myRigidBody.velocity = playerVelocity;
	}

	private void FlipSprite()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;

		if (playerHasHorizontalSpeed)
		{
			transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);
		}
	}
}