using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable HP;

    public bool ResetHP;

    public FloatReference StartingHP;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private void Start()
    {
        if(ResetHP)
            HP.SetValue(StartingHP);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
            if(damage != null)
            {
                HP.ApplyChange(-damage.DamageAmount);
                DamageEvent.Invoke();
            }

            if(HP.Value <=0.0f)
            {
                DeathEvent.Invoke();
            }
        }
    }
}
