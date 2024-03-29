using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    [Header("Particle")]
    public ParticleSystem dust;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 0.3f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;
    private Controller _controller;
    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;
    private Animator _animator;
    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed, _coyoteCounter, _jumpBufferCounter;
    private bool _desiredJump, _onGround, _isJumping;
    private float _fallSpeedYDampingChangeThreshold;
    // Start is called before the first frame update

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
        _defaultGravityScale = 1f;
        _animator = GetComponent<Animator>();
        Debug.Log(_fallSpeedYDampingChangeThreshold);
        
        _fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
    }
    // Update is called once per frame
    void Update()
    {
        _desiredJump |= _controller.input.RetrieveJumpInput();

        //if we are falling past a certain speed threshold
        if(_body.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        //if we are standing still or moving up
        if(_body.velocity.y >=0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }
    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _animator.SetBool("OnGround",_onGround);
        _velocity = _body.velocity;
        if (_onGround && _body.velocity.y == 0)
        {
            _jumpPhase = 0;
            _coyoteCounter = _coyoteTime;
            _isJumping = false;
            _animator.SetBool("IsJumping",false);
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }
        if (_desiredJump)
        {
            _desiredJump = false;
            _jumpBufferCounter = _jumpBufferTime;
        }
        else if(!_desiredJump && _jumpBufferCounter > 0)
        {
            _jumpBufferCounter -=Time.deltaTime;
        }
        if(_jumpBufferCounter > 0)
        {
            JumpAction();
        }

        if (_controller.input.RetrieveJumpHoldInput() && _body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (!_controller.input.RetrieveJumpHoldInput() || _body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if(_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }
        _body.velocity = _velocity;
        _animator.SetFloat("Y_Velocity",_body.velocity.y);
    }
    private void JumpAction()
    {
        //dust.Play();
        if (_coyoteCounter >0f || (_jumpPhase < _maxAirJumps && _isJumping))
        {
            if(_isJumping)
            {
                _jumpPhase += 1; 
            }
            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            _isJumping = true;
            _animator.SetBool("IsJumping",true);

            
            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_body.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }
}

