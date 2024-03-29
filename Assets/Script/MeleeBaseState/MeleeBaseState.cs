using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;
    protected float attackDamage;



    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> _collidersDamaged;
    // The Hit Effect to Spawn on the afflicted Enemy
    protected GameObject _hitEffectPrefab;
    private Controller _controller;


    // Input buffer Timer
    private float _attackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine); 
        animator = GetComponent<Animator>();
        _collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<GroundMeleeAttack>().Hitbox;
        _controller = GetComponent<Controller>();
        //no hit effect yet
        _hitEffectPrefab = GetComponent<GroundMeleeAttack>().Hiteffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _attackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }


        if (_controller.input.RetrieveAttackInput())
        {
            _attackPressedTimer = 2.0f;
        }

        if (animator.GetFloat("AttackWindow.Open") > 0f && _attackPressedTimer > 0)
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!_collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    //GameObject.Instantiate(_hitEffectPrefab, collidersToDamage[i].transform);
                    Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
                    _collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
    public void DestroyHitEffect(GameObject hitEffect)
    {
        Destroy(hitEffect);
    }

}