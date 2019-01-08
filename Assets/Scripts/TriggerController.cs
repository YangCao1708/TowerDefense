using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerController : MonoBehaviour
{

    public static TriggerController Trigger;

    public Image Win;
    public Image Lose;
    public Button Restart;

    private void Awake()
    {
        if (Trigger != null)
        {
            Destroy(Trigger);
        }
        Trigger = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster")
        {
            Lose.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
    }

    public void Won()
    {
        Win.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
    }
}
