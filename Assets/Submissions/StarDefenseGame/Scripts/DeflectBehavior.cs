using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.StarDefense
{
    public class DeflectBehavior : MonoBehaviour
    {
        private int maxSize = 10;
        private int maxSpin = 360;
        private float currentSpin = 0;
        private float currentSize = 1;
        private float animationTime = 1;
        public string side;
        public bool hit = false;
        private BoxCollider2D boxCollider;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] sounds;



        private string[] types =
        {
        "Rocket",
        "Bomb",
    };
        // Start is called before the first frame update
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            resetProperties();
        }

        public void resetProperties()
        {
            currentSpin = 0;
            transform.rotation = Quaternion.Euler(0, 0, currentSpin);
            currentSize = 1;
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = true;
            hit = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (hit == false)
            {
                if (currentSize < maxSize)
                {
                    currentSize += Time.deltaTime * (maxSize / animationTime);
                    transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                }
                else
                {
                    currentSize = maxSize;
                    transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                }
                if (currentSpin < maxSpin)
                {
                    currentSpin += Time.deltaTime * (maxSpin / animationTime);
                    transform.rotation = Quaternion.Euler(0, 0, currentSpin);
                }
                else
                {
                    currentSpin = maxSpin;
                    transform.rotation = Quaternion.Euler(0, 0, currentSpin);
                }
            }
            else if (hit && gameObject.activeSelf)
            {
                //Debug.Log("active");
                if (currentSize > 0)
                {
                    currentSize -= Time.deltaTime * (maxSize / animationTime);
                    transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                }
                else
                {
                    currentSize = 0;
                    transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                    gameObject.SetActive(false);
                }
                if (currentSpin > 0)
                {
                    currentSpin -= Time.deltaTime * (maxSpin / animationTime);
                    transform.rotation = Quaternion.Euler(0, 0, currentSpin);
                }
                else
                {
                    currentSpin = 0;
                    transform.rotation = Quaternion.Euler(0, 0, currentSpin);
                }
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (string s in types)
            {
                if (!collision.gameObject.layer.ToString().Contains(side))
                {

                    if (hit == false)
                    {
                        hit = true;
                        boxCollider.enabled = false;
                        audioSource.clip = sounds[0];
                        audioSource.Play();

                        Vector2 newVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

                        collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(
                            new Vector2(-20, 0), ForceMode2D.Impulse);

                        collision.gameObject.layer = GameManager.retrieveLayer("Opponent" + collision.gameObject.name);
                    }
                }
            }
        }
    }
}