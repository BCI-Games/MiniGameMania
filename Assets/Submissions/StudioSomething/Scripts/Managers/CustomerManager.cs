using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    public GameObject CustomerPrefab;

    public Transform SpawnPoint;
    public Transform ExitPoint;

    public bool IsRunning = false;

    public float SpawnTimer;
    public float TimeToSpawn = 15f;

    public Item[] Orders;

    public List<GameObject> Customers;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = TimeToSpawn / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRunning) return;
        if (SpawnTimer <= 0)
        {

            SpawnTimer = TimeToSpawn;
            if (Customers.Count == QueueManager.Instance.Queue.Length) return;
            GameObject customer = GameObject.Instantiate(CustomerPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            CustomerController customerController = customer.GetComponent<CustomerController>();
            customerController.Order = Orders[Random.Range(0, Orders.Length)];
            customerController.QueuePosition = Customers.Count;
            Customers.Add(customer);
        }
        SpawnTimer -= Time.deltaTime;
    }
    
    public void NextInLine(){
        //Remove the first player in line and set Destination to end
        CustomerController customerController = Customers[0].GetComponent<CustomerController>();
        customerController.PatienceBar.gameObject.SetActive(false);
        customerController.QueuePosition = -1;

        Customers.Remove(Customers[0]);
        //Move everyone else up
        for(int i= 0; i < Customers.Count; i++){
            Customers[i].GetComponent<CustomerController>().QueuePosition = i;
        }

    }

    public void Reset()
    {
        foreach (GameObject customer in Customers)
        {
            Destroy(customer);
        }
    }

}
