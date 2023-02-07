using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Pickup))]
public class SpawnPickups : MonoBehaviour
{
    // Start is called before the first frame update
    public int ranType;
    public Pickup powerupPrefab;
    public Pickup lifePrefab;
    public Pickup scorePrefab;
    public float xCoord;
    public float yCoord;
    public Vector2 spawnPoint;
    SpriteRenderer sr;
    void Start()
    {
        for (int i = 0; i < 5; i++) {
            sr = GetComponent<SpriteRenderer>();
            ranType = Random.Range(0, 3);
            xCoord = Random.Range(-5.0f, 5.0f);
            yCoord = Random.Range(-2.0f, 2.0f);
            spawnPoint = new Vector2(xCoord, yCoord);
            if (ranType == 0)
            {
                Pickup powerup = Instantiate(powerupPrefab, spawnPoint, Quaternion.identity);
                sr.sprite = Resources.Load<Sprite>("DragonCoin");
            }
            else if (ranType == 1)
            {
                Pickup life = Instantiate(lifePrefab, spawnPoint, Quaternion.identity);
                sr.sprite = Resources.Load<Sprite>("Life");
            }
            else if (ranType == 2)
            {
                Pickup score = Instantiate(scorePrefab, spawnPoint, Quaternion.identity);
                sr.sprite = Resources.Load<Sprite>("Score");
                
            }
            void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
                {
                    Destroy(gameObject);
                    i--;
                }
    }
}   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
