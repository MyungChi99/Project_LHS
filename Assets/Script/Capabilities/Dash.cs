using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Dash : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _dashingPower = 1.7f;
    [SerializeField, Range(0f, 0.7f)] private float _dashingTime = 0.2f;
    //[SerializeField, Range(0f, 2f)] private float _dashingCoolDown = 0.2f;

    [SerializeField] private TrailRenderer _trailRenderer;
    private Controller _controller;
    private Rigidbody2D _body;
    private Ground _ground;
    private Move _move;
    private bool _canDash = true,_isDashing; 
    private Vector2 _dashingDir,_direction;
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
        _move = GetComponent<Move>();
    }
    
    void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput();
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            _isDashing = true;
            _canDash =false;
            _trailRenderer.emitting = true;
            _dashingDir = new Vector2(_direction.x, Input.GetAxisRaw("Vertical"));
            if(_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x,0);
            }
            StartCoroutine(StopDashing());
        }
        if(_isDashing)
        {
            _body.velocity = _dashingDir.normalized * _dashingPower;
        }

        if(_move._onGround)
        {
            _canDash = true;
        }        
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing =false;
    }
}

