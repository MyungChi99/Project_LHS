using UnityEngine;

public class Attack : MonoBehaviour
{  
    //AttackComponent
    //[SerializeField,Range(0f,5f)] private float _attackRange = 0.5f;

    private bool _attackTriggered = false;

    //기본 구성요소
    private Animation _ani;
    private Rigidbody2D _body;
    private Controller _controller;
    public Transform AttackPositon;

    private void Awake()
    {
        _ani = GetComponent<Animation>();
        _body = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();        
    }

    private void Update() 
    {
        _attackTriggered = _controller.input.RetrieveAttackInput();
    }
    
    private void FixedUpdate() 
    {
        if(_attackTriggered)
        {
            AttackAction();
            _attackTriggered = false;
        }
    }


    private void AttackAction()
    {
        Debug.Log("Attack");


    }
}
