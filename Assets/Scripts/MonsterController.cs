using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : ItemController {

    private MonsterTrigger m_trigger;
    private WeaponController m_attackingWeapon;

    private bool m_isForward = true;

    new void Start()
    {
        base.Start();
    }

    void Update () {
        if (m_isForward)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
	}

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            m_trigger.OnMonsterDied();
            Debug.Log("HERE");
            m_attackingWeapon.OnMonsterDead();
            Destroy(this.gameObject);
            return;
        }

        HealthBar.UpdateHealthBar(CurrentHealth / MaxHealth);
    }

    public void AssignTrigger(MonsterTrigger trigger)
    {
        m_trigger = trigger;
    }

    public void UpdateForward(bool forward)
    {
        m_isForward = forward;
    }

    public void AssignWeapon(int idx, WeaponController weapon)
    {
        //m_monListIndex = idx;
        m_attackingWeapon = weapon;
    }
}
