using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStatus status { get; private set; }

    // Character information
    static private int character_counter = 0;
    //public string playerName { get; private set; }

    // Inventary
    // TODO DEBUG: uncomment
    /*
    protected Inventory inventory;
    protected Inventory last_game_inventory;
    */

    // Displacement
    [Header("Displacement")]
    public float translationSpeed = 2f;

    public float proximity_threshold = 0.01f;
    protected GameObject target_dest;
    protected Vector3 pos_offset;

    // Properties
    [Header("Health")]
    public int maxHealth = 100;
    public int health = 100;

    [Header("Spell")]
    public GameObject power_ball_model;
    public float power_ball_duration = 4f;
    protected GameObject power_ball;
    protected float spell_anim_timer;

    // Event handler
    static public event Action<GameObject> OnReachDestination;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        map = Map.instance;
        if(map==null)
        {
            Debug.Log("Warning: map undefined instance");
        }
        */

        status = CharacterStatus.IDLE;

        gameObject.tag = "Character";
        character_counter++;
        gameObject.name = "Avatar " + character_counter.ToString();
        Debug.Log("New character: my name is " + gameObject.name);

        //UpdateAppearance();
    }

    void Start()
    {
        // TODO DEBUG: uncomment
        //inventory = new Inventory();
        //last_game_inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(status == CharacterStatus.MOVING)
        {
            Walk();
        }
    }

    private void Walk()
    {
        if(ReachedTarget())
        {
            status = CharacterStatus.IDLE;
            OnReachDestination?.Invoke(target_dest);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                 target_dest.transform.position + pos_offset,
                                                 translationSpeed * Time.deltaTime);
            transform.LookAt(target_dest.transform);
        }

    }

    public bool ReachedTarget()
    {
        if (target_dest == null)
        {
            return false;
        }

        float distance = ComputeDistance(target_dest.transform.position + pos_offset);
        return distance <= proximity_threshold;
    }

    public float ComputeDistance(Vector3 targetPosition)
    {
        float target_x = targetPosition.x;
        float target_y = targetPosition.y;
        float this_x = transform.position.x;
        float this_y = transform.position.y;
        return (float)Math.Sqrt(Math.Pow(target_x - this_x, 2) + Math.Pow(target_y - this_y, 2));
    }

    /*
    public void SetColor(Color color)
    {
        GameObject chrInChariot = transform.Find("character").gameObject;
        var renderer = chrInChariot.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color);
    }
    */

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void SetOffset(Vector3 offset)
    {
        pos_offset = offset;
    }

    public void SetDestination(GameObject target_dest)
    {
        SetDestination(target_dest, new Vector3());
    }

    public void SetDestination(GameObject target_dest, Vector3 offset)
    {
        if (status == CharacterStatus.IDLE)
        {
            Debug.Log("Character " + name + " is moving to " + target_dest.name + ".");
            this.target_dest = target_dest;
            this.pos_offset = offset;
            status = CharacterStatus.MOVING;
        }
    }

    public void InvokeSpell()
    {
        if(status == CharacterStatus.IDLE)
        {
            // Create a power ball, place it in front of the player and destroy it
            // after some time.
            power_ball = Instantiate(power_ball_model, transform.position, Quaternion.identity);
            power_ball.transform.SetParent(transform);
            power_ball.transform.position += power_ball.transform.forward * 1f;

            spell_anim_timer = power_ball_duration; // reset timer
            status = CharacterStatus.INVOKE_SPELL;
        }
        else if(status == CharacterStatus.INVOKE_SPELL)
        {
            spell_anim_timer -= Time.deltaTime;
            if(spell_anim_timer < 0)
            {
                status = CharacterStatus.IDLE;
            }
            Destroy(power_ball);
        }
    }
}


public enum PowerType
{
    FIRE,
    WATER,
    ELECTRICITY
}

public enum CharacterStatus
{
    IDLE,
    MOVING,
    INVOKE_SPELL // Show power ball in the hand of the player
        // Short and Long rang attack are managed asynchronously
}












