using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMeleeAttack : MonoBehaviour
{

    private StateMachine _meleeStateMachine;
    private Controller _controller;

    private float _duration = 0.5f;

    [SerializeField] public Collider2D Hitbox;
    //no hit effect yet
    [SerializeField] public GameObject Hiteffect;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<Controller>();
        _meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        _duration -= Time.deltaTime;
        if (_controller.input.RetrieveAttackInput() && _meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState) && _duration <= 0)
        {
            _meleeStateMachine.SetNextState(new GroundEntryState());
            _duration = 0.5f;
        }
    }
}