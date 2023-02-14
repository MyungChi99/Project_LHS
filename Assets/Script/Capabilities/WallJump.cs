using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _wallJumpHeight = 3f;
    [SerializeField, Range(0f, 10f)] private float _wallJumpSpeed = 3f;
    //[SerializeField, Range(0f, 10f)] private float _wallStickTime = 0.5f;
    private Controller _controller;
    private Rigidbody2D _body;
    private Move _move;
    private float _wallStickCounter;
    private bool _isCollidingWithWall, _desiredJump, _wallJumping;

    void Awake()
    {
        _controller = GetComponent<Controller>();
        _body = GetComponent<Rigidbody2D>();
        _move = GetComponent<Move>();
    }

    private void FixedUpdate()
    {
        if (_isCollidingWithWall)
        {
            _wallStickCounter += Time.deltaTime;
            _desiredJump |= _controller.input.RetrieveJumpInput();
            if (_desiredJump)
            {
                _wallJumping = true;
                Vector2 wallJumpDirection = (_move.isFacingRight) ? Vector2.left : Vector2.right;
                _body.velocity = new Vector2(_wallJumpSpeed * wallJumpDirection.x, _wallJumpHeight);
                _desiredJump = false;
                _isCollidingWithWall = false;
                _wallStickCounter = 0;
            }
        }
        else
        {
            _wallStickCounter = 0;
        }
        Debug.Log(_move._onGround);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") && !_move._onGround && !_wallJumping && _body.velocity.y < 0)
        {
            _isCollidingWithWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _isCollidingWithWall = false;
        }
    }
}