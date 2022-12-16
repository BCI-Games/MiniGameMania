using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

    public float speed = 8;

    private Vector3 amountToMove;

    private PlayerPhysics playerPhysics;

    // private int direction = 1;

    private bool triggerEntered = false;
    private Collider2D itemCollidedWith;

    private Animator anim;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerPhysics = GetComponent<PlayerPhysics>();

        gameManager = this.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
        amountToMove = new Vector3(speed, 0, 0);
        transform.Translate(amountToMove * Time.deltaTime);

        // if (transform.position.x > maxPositionX) {
        //     // direction = -1;
        //     this.transform.Rotate(0, 180, 0);
        // } else if (transform.position.x < minPositionX) {
        //     // direction = 1;
        //     this.transform.Rotate(0, -180, 0);
        // }

        if (triggerEntered) {
            if (itemCollidedWith.gameObject.name == "Right Border") {
                this.transform.Rotate(0, 180, 0);
                triggerEntered = false;
            } else if (itemCollidedWith.gameObject.name == "Left Border") {
                this.transform.Rotate(0, -180, 0);
                triggerEntered = false;
            }
        }

         if (Input.GetKeyDown (KeyCode.Space)) {
            
            // anim.Play("chef_holding");

            Debug.Log(gameManager.GetComponent<GameManager>().currentRecipe[0]);

            if (triggerEntered == true) {

                // Debug.Log(gameManager.GetComponent<GameManager>().currentRecipe[0]);
                
                // if (itemCollidedWith.gameObject.name.)

                Debug.Log("name" + itemCollidedWith.gameObject.name);

                if (gameManager.GetComponent<GameManager>().currentRecipe.Any(itemCollidedWith.gameObject.name.Contains)) {
                    
                    // Success

                    // gameManager.currentRecipe.Remove(itemCollidedWith.gameObject.name);
                    Destroy(itemCollidedWith.gameObject);
                    
                } else {

                    // Fail
                    Destroy(itemCollidedWith.gameObject);

                    // anim.Play("chef_panic");
                }

                // Debug.Log(itemCollidedWith.gameObject.name);

            }
        }



        

        // amountToMove = new Vector2(currentSpeed, 0);
        // playerPhysics.MoveAmount(direction*amountToMove * Time.deltaTime);

        // if (Input.GetKeyDown (KeyCode.Space) && triggerEntered == true) {
        // if (triggerEntered == true) {
        //     Debug.Log();
        // }

    }

    private void OnTriggerEnter2D(Collider2D item) {
        triggerEntered = true;
        itemCollidedWith = item;
    }

    private void OnTriggerExit2D() {
        triggerEntered = false;
    }

    /** 

        - Cheese + Flour + Tomato = Pizza
    
    **/

}
