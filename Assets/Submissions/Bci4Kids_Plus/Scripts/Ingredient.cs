using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    // public GameObject gameManager;

    private bool sendDown = false;


    // Start is called before the first frame update
    void Start()
    {
        // gameManager = GameObject.Find("GameManager");
    }
        // Update is called once per frame
    void Update()
    {
        if (sendDown == true)
        {
            FallDown();
        }
        if (transform.position.x > 6&& transform.position.y > 6) {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (transform.position.y > 7 && transform.position.x > -6.5 && transform.position.x < 5)
        {
            gameObject.GetComponent<AudioSource>().Play();
            sendDown = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

            // Delete path child
            Destroy(gameObject.transform.GetChild(0).gameObject);

            // gameObject.AddComponent<Rigidbody2D>();
        }
    }
    private void FallDown()
    {     
        if (gameObject.transform.position.y < -5)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            sendDown = false;

            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            // Remove the ridigbody component
            // Destroy(gameObject.GetComponent<Rigidbody2D>());

            // Set mass to 0
            // gameObject.GetComponent<Rigidbody2D>().mass = 0;
            // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

    }


}
