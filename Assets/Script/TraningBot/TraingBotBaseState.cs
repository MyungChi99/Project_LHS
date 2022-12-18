using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraingBotBaseState : State
{
    [SerializeField] private int _hp;
    protected Animator animator;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();

    }
    
    public override void OnUpdate()
    {
        time += Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {
        fixedtime += Time.deltaTime;
    }

    public override void OnLateUpdate()
    {
        latetime += Time.deltaTime;
    }
    public override void OnExit()
    {

    }






    // public int hp;
    // // Cached animator component
    // protected Animator animator;
    // // bool to check whether or not the next attack in the sequence should be played or not

    // // The attack index in the sequence of attacks
    // protected int attackIndex;



    // // The cached hit collider component of this attack
    // protected Collider2D hitCollider;
    // // Cached already struck objects of said attack to avoid overlapping attacks on same target
    // private List<Collider2D> _collidersDamaged;
    // // When Taken dagmae the flash  effect to Spawn on the afflicted Enemy
    // private GameObject _flashEffectPrefab;


    // public override void OnEnter(StateMachine _stateMachine)
    // {
    //     base.OnEnter(_stateMachine); 
    //     animator = GetComponent<Animator>();
    //     _collidersDamaged = new List<Collider2D>();
    //     //hitCollider = GetComponent<GroundMeleeAttack>().Hitbox;
    //     //no hit effect yet
    //     //_hitEffectPrefab = GetComponent<GroundMeleeAttack>().Hiteffect;
    // }

    // public override void OnUpdate()
    // {
    //     base.OnUpdate();

    //     if (animator.GetBool("IsHitten"))
    //     {
    //         Attack();
    //     }
    // }

    // public override void OnExit()
    // {
    //     base.OnExit();
    // }
    // protected void HitAction()
    // {
        
    // }


    // protected void Attack()
    // {
    //     Collider2D[] collidersToDamage = new Collider2D[10];
    //     ContactFilter2D filter = new ContactFilter2D();
    //     filter.useTriggers = true;
    //     int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
    //     for (int i = 0; i < colliderCount; i++)
    //     {

    //         if (!_collidersDamaged.Contains(collidersToDamage[i]))
    //         {
    //             TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

    //             // Only check colliders with a valid Team Componnent attached
    //             if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
    //             {
    //                 //no hit effect yet
    //                 //GameObject.Instantiate(_hitEffectPrefab, collidersToDamage[i].transform);
    //                 Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
    //                 _collidersDamaged.Add(collidersToDamage[i]);
    //             }
    //         }
    //     }
    // }

}
