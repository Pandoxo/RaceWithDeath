using UnityEngine;
using TMPro;

public class LudekCode : MonoBehaviour
{
    public float range;
    public float lifetime;
    public bool timePasses;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject player;
    [SerializeField] TextMeshPro lifetimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetimer = GetComponentInChildren<TextMeshPro>();
    }


    void FixedUpdate()
    {
        lifetimer.text = lifetime.ToString();
        if (Vector3.Distance(player.GetComponent<Rigidbody2D>().position, rb.position) <= range)
        {
            Debug.Log("Gracz w zasiegu");
            timePasses = false;
        }

        if (lifetime == 0 && timePasses)
        {
            Debug.Log("KONIEC ZYCIA");
            timePasses = false;
        }
        else
        {
            if (timePasses)
                {
                    lifetime -= 1;
                }
            }
    }
}
