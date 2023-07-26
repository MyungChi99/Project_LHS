using UnityEngine;



[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [Header("Particle")]
    public ParticleSystem dust;
    
    [Header("Camera Stuff")]
    [SerializeField] private GameObject _cameraFollowGO;
    private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;
    private float _maxSpeedChange, _acceleration;
    public bool _onGround;
    public bool isFacingRight = false;
    private Animator _animator;
    private CameraFollowObject _cameraFollowObject;
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
        _animator = GetComponent<Animator>();

        _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
    }
    private void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Min(_maxSpeed, _maxSpeed*(1-_ground.Friction));
    }
    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;
        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
        _body.velocity = _velocity;
        _animator.SetFloat("RunSpeed",Mathf.Abs(_direction.x));
        if (_direction.x != 0)
        {
		    CheckDirectionToFace(_direction.x > 0);
        }
    }
    private void Turn()
    {
        dust.Play();
        if(isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x,0f,transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;

            //turn the camera follow object
            _cameraFollowObject.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x,180f,transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;

            //turn the camera follow object
            _cameraFollowObject.CallTurn();
        }
    }
    private void CheckDirectionToFace(bool isMovingRight)
    {
        if(isMovingRight != isFacingRight)
            Turn();
    }
}

