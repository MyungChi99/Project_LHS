using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMeleeAttack : MonoBehaviour
{

    private StateMachine _meleeStateMachine;
    private Controller _controller;

    [SerializeField] public Collider2D Hitbox;
    //no hit effect yet
    //[SerializeField] public GameObject Hiteffect;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<Controller>();
        _meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.input.RetrieveAttackInput() && _meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            _meleeStateMachine.SetNextState(new GroundEntryState());
        }
    }
}