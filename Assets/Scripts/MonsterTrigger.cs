﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour {

    public int Index;

    private int m_count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            MonsterController mon = collision.GetComponent<MonsterController>();
            mon.AssignTrigger(this);
            if (m_count == 0)
            {
                GameManager.Instance.UpdateShootingStatus(Index, true);
            }
            m_count++;
        } else if (collision.tag == "Weapon")
        {
            WeaponController weapon = collision.GetComponent<WeaponController>();
            if (m_count > 0)
            {
                weapon.UpdateShootStatus(true);
            }
        }
    }

    public void OnMonsterDied()
    {
        m_count--;
        GameManager.Instance.RemoveMonster();
        if (m_count == 0)
        {
            GameManager.Instance.UpdateShootingStatus(Index, false);
        }
    }
}
