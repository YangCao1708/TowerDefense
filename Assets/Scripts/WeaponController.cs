using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : ItemController {
    public BulletController Bullet;

    private bool m_isShooting = false;
    private bool m_shouldShoot = false;
    private Animator m_ani;
    private List<MonsterController> m_mons;

    private void Awake()
    {
        m_ani = GetComponent<Animator>();
        m_mons = new List<MonsterController>();
    }

    // Update is called once per frame
    void Update () {
		if (!m_isShooting && m_shouldShoot)
        {
            StartCoroutine(StartShoot());
        }
	}

    void Shooting()
    {
        GameObject bullet = Instantiate<GameObject>(Bullet.gameObject, transform);
        bullet.transform.localPosition = Vector3.zero;
    }

    IEnumerator StartShoot()
    {
        m_isShooting = true;
        m_ani.SetBool("isShooting", true);
        while (m_shouldShoot)
        {
            Shooting();
            yield return new WaitForSeconds(AttackInterval);
        }
        m_ani.SetBool("isShooting", false);
        m_isShooting = false;
    }

    public void UpdateShootStatus(bool shouldShoot)
    {
        m_shouldShoot = shouldShoot;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            foreach (MonsterController mon in m_mons) {
                mon.UpdateForward(true);
            }
            //GameManager.Instance.RemoveWeapon(this);
            Destroy(this.gameObject);
            return;
        }

        HealthBar.UpdateHealthBar(CurrentHealth / MaxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            MonsterController mon = collision.GetComponent<MonsterController>();
            mon.UpdateForward(false);
            m_mons.Add(mon);
            int index = m_mons.IndexOf(mon);
            mon.AssignWeapon(index, this);
            mon.name += index;
            mon.transform.localPosition += Vector3.right * index * 0.1f;
            StartCoroutine(TakeMonsterDamage(mon));
        }
    }

    IEnumerator TakeMonsterDamage(MonsterController mon)
    {
        while (mon)
        {
            TakeDamage(mon.Damage);
            yield return new WaitForSeconds(mon.AttackInterval);
        }
    }

    public void OnMonsterDead()
    {
        foreach (MonsterController mon in m_mons)
        {
            if (mon != null)
            {
                mon.UpdateForward(true);
            }
        }
        m_mons.Clear();
    }
}
