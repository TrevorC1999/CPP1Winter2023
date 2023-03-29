using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("inRange", inRange);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;

        }

    }
}
