using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit:" + other.transform.name);

    if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

    if(other.tag == "Laser")
        {
            Destroy(GameObject.FindWithTag("Laser"));
            Destroy(this.gameObject);
        }
    }
}
