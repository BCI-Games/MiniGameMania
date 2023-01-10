using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Submissions.StarDefense
{
    public class ProjectileBehavior : MonoBehaviour
    {

        public Rigidbody2D projectileRB;
        public ParticleSystem particles;
        public SpriteRenderer sprites;
        public int damage;
        public Selection.SelectionType selectionType;

        private float lifeTime = 10;
        public float existingTime;
        public float decayTimer = 1;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] sounds;

        public bool exploded = false;
        // Start is called before the first frame update
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            resetProperties();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 v = projectileRB.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (transform.position.y < -10)
            {
                gameObject.SetActive(false);
            }
            if (!exploded)
            {
                if (existingTime > lifeTime)
                {
                    boom();
                }
                else
                {
                    existingTime += Time.deltaTime;
                }
            }
            else if (exploded)
            {
                if (decayTimer > 0)
                {
                    decayTimer -= Time.deltaTime;
                }
                else
                {
                    //Debug.Log("Instant hide");
                    gameObject.SetActive(false);
                }

            }
        }
        public void resetProperties()
        {
            exploded = false;
            existingTime = 0;
            decayTimer = 1;
            projectileRB = GetComponent<Rigidbody2D>();
            projectileRB.simulated = true;
            particles = GetComponent<ParticleSystem>();
            sprites = GetComponent<SpriteRenderer>();
            sprites.color = new Color(1, 1, 1, 1);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (exploded == false)
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    if (selectionType != Selection.SelectionType.BOMB)
                    {
                        boom();
                    }

                }
                else if (collision.gameObject.CompareTag("Wall"))
                {
                    collision.gameObject.GetComponent<WallBehavior>().TakeDamage(damage);
                    boom();
                }
            }

        }

        private void boom()
        {
            audioSource.clip = sounds[0]; //boom sound
            audioSource.Play();
            exploded = true;
            particles.Play();
            sprites.color = new Color(1, 1, 1, 0);
            projectileRB.simulated = false;

        }

    }
}