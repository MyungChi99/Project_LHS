using UnityEngine;

[RequireComponent(typeof(Controller))]
//made by chat gpt, The Ai made a dashinvincibility option so Adjustment is required in the future
public class Dash_gpt : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _dashSpeed = 15f;
    [SerializeField, Range(0f, 10f)] private float _dashDuration = 0.5f;
    [SerializeField, Range(0f, 5f)] private float _dashInvincibilityDuration = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _dashRechargeTime = 0.5f;

    private Controller _controller;
    private Rigidbody2D _body;
    private Move _move;
    private Vector2 _velocity;
    private Animator _animator;
    private bool _desiredDash, _dashing, _recharging;
    private float _dashCounter, _invincibilityCounter, _rechargeCounter, _direction;
    private Vector2 _dashDirection, _currrentDireciton, _originalGravity;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
        _move = GetComponent<Move>();
        _animator = GetComponent<Animator>();
        _originalGravity = Physics2D.gravity;
    }

    private void Update()
    {
        _desiredDash = _controller.input.RetrieveDashInput();
        _direction = _controller.input.RetrieveMoveInput();
        _currrentDireciton = new Vector2(_direction,0f);
    }

    private void FixedUpdate()
    {
        _velocity = _body.velocity;

        if (_dashing)
        {
            _dashCounter -= Time.deltaTime;

            _velocity = _dashDirection * _dashSpeed;
            Physics2D.gravity = Vector2.zero;

            if (_dashCounter <= 0)
            {
                _dashing = false;
                _invincibilityCounter = 0;
                Physics2D.gravity = _originalGravity;
            }
        }
        else if (_recharging)
        {
            _rechargeCounter -= Time.deltaTime;

            if (_rechargeCounter <= 0)
            {
                _recharging = false;
            }
        }
        else if (_desiredDash)
        {
            _desiredDash = false;
            _dashing = true;
            _dashCounter = _dashDuration;
            _invincibilityCounter = _dashInvincibilityDuration;
            _recharging = true;
            _rechargeCounter = _dashRechargeTime;
            if (_currrentDireciton.x != 0)
            {
                _dashDirection = _currrentDireciton.normalized;

            }
            if(_currrentDireciton.x == 0)
            {
                if(_move.isFacingRight)
                    _dashDirection = new Vector2(1,0);
                else
                    _dashDirection  = new Vector2(-1,0);
            }
        }

        _body.velocity = _velocity;
        _animator.SetBool("IsDashing", _dashing);
    }
}