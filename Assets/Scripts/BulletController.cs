using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ItemController {

    //public GameObject StaplerNail;
    //public GameObject Ball;

	void Update () {
        this.transform.Translate(Vector3.right * Time.deltaTime * Speed);

        if (transform.localPosition.x > Constants.OutOfScreenX)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            MonsterController mon = collision.GetComponent<MonsterController>();
            if (mon != null)
            {
                if (this.gameObject.name.Contains("StaplerNail") && collision.gameObject.name.Contains("Ball"))
                {
                    Debug.Log("Here!");
                    mon.TakeDamage(this.Damage * 3);
                } else
                {
                    mon.TakeDamage(this.Damage);
                }
            } else
            {
                Debug.LogError("Cannot find MonsterController:" + collision.name);
            }
            Destroy(this.gameObject);
        }
    }
}
