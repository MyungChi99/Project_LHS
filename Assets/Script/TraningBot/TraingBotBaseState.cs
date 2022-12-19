using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraingBotBaseState : MonoBehaviour
{
    [SerializeField] private float _health , _maxHealth = 3f;

    private void Start() 
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount; //3->2->1 ->0 = enemy has died
    }


}