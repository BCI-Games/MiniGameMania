using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submissions.BrainBuddy
{
    public class PlayerController : MonoBehaviour
    {
        public Camera MainCamera;
        public Vector3 CameraOffset;
        public LayerMask terrainLayer;
        public float JumpVelocity = 6;
        public float LeftRightVelocity = 3;

        private Rigidbody2D _playerBody;
        private BoxCollider2D _collider;
        private Animator[] _animators;
        private SpriteRenderer[] _spriteRenderers;
        private float _yVelocity;
        private float _xVelocity;
        private bool _respawn;


        // Start is called before the first frame update
        void Start()
        {
            _playerBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _animators = GetComponentsInChildren<Animator>();
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _yVelocity = JumpVelocity;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                _xVelocity = LeftRightVelocity;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                _xVelocity = -LeftRightVelocity;

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
                _xVelocity = 0;

            _playerBody.velocity = new Vector3(_xVelocity, _playerBody.velocity.y, 0);

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
            {
                _playerBody.velocity = new Vector3(_playerBody.velocity.x, JumpVelocity, 0);
            }

            UpdateAnimation();

            MainCamera.orthographicSize = 7;
            MainCamera.transform.position = new Vector3(this.transform.position.x + CameraOffset.x, this.transform.position.y + CameraOffset.y, CameraOffset.z);

            if (this.transform.position.y < -6)
                _respawn = true;

            if (_respawn)
            {
                if (!FinishedController.Instance.FinishReached)
                    ScoreCounter.Instance.Reset();
                this.transform.position = new Vector3(-6, 3, -1);
                _playerBody.velocity = new Vector3(_playerBody.velocity.x, JumpVelocity / 2, 0);
                _respawn = false;
            }

        }

        private void UpdateAnimation()
        {
            if (_xVelocity > 0)
            {
                for (int i = 0; i < _spriteRenderers.Length; i++)
                {
                    _animators[i].SetBool("IsRunning", true);
                    _spriteRenderers[i].flipX = false;
                }
            }
            else if (_xVelocity < 0)
            {
                for (int i = 0; i < _spriteRenderers.Length; i++)
                {
                    _animators[i].SetBool("IsRunning", true);
                    _spriteRenderers[i].flipX = true;
                }
            }
            else
            {
                for (int i = 0; i < _spriteRenderers.Length; i++)
                    _animators[i].SetBool("IsRunning", false);
            }
        }


        private bool IsGrounded()
        {
            return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.1f, terrainLayer);
        }
    }
}