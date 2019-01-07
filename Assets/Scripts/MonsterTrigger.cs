using System.Collections;
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
        }
    }

   public void OnMonsterDied()
    {
        m_count--;
        if (m_count == 0)
        {
            GameManager.Instance.UpdateShootingStatus(Index, false);
        }
    }
}
