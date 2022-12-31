using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Submissions.StudioSomething
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Keyboard;
        public static PlayerController BCI;
        public Image FoodSprite;
        public Animator Animator;
        public ProcessBar ProcessBar;
        public GameObject ItemHolder;
        public InventoryManager InventoryManager;
        public bool HasItem;
        public Item HeldItem;
        public GameObject HeldItemObject;
        private string[] KeyCodes;
        private NavMeshAgent NavAgent;
        private Vector3 worldDeltaPosition;
        private Vector3 groundDeltaPosition;
        private Vector2 velocity = Vector2.zero;

        // Start is called before the first frame update
        void Start()
        {
            NavAgent = GetComponent<NavMeshAgent>();
            KeyCodes = StationManager.Instance.KeyCodes;
            NavAgent.updatePosition = false;
        }

        void Update()
        {
            if (Keyboard == this)
                KeyboardMovement();

            worldDeltaPosition = NavAgent.nextPosition - transform.position;
            groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
            groundDeltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);
            velocity = (Time.deltaTime > 1e-5) ? groundDeltaPosition : Vector2.zero;
            Animator.SetFloat("Speed", velocity.magnitude);
            transform.position = Vector3.Lerp(transform.position, NavAgent.nextPosition, 0.1f);


        }

        public void MoveCharacter(Vector3 target)
        {
            NavAgent.SetDestination(target);
        }

        private void KeyboardMovement()
        {
            for (int i = 0; i < KeyCodes.Length; i++)
            {
                if (Input.GetKeyDown(KeyCodes[i]))
                {
                    MoveCharacter(StationManager.Instance.GetStationLocation(i));
                }
            }
        }




    }
}
