using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    public float Patience = 30f;
    public PatienceBar PatienceBar;

    public Image OrderImage;
    public GameObject OrderCanvas;
    public Item Order;

    public GameObject[] ReactionsPrefabs;
    public float CurrentPatience
    {
        set
        {
            currentPatience = value;
            PatienceBar.SetPatience(currentPatience);
        }
        get
        {
            return currentPatience;
        }
    }

    public int QueuePosition
    {
        get { return queuePosition; }
        set
        {
            queuePosition = value;
            if(queuePosition == -1){
                GetComponent<NavMeshAgent>().SetDestination(CustomerManager.Instance.ExitPoint.position);
                return;
            }
            GetComponent<NavMeshAgent>().SetDestination(QueueManager.Instance.Queue[queuePosition].position);
        }
    }

    [SerializeField] private int queuePosition = -1;
    private float currentPatience;
    private NavMeshAgent agent;
    private bool IsDone;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CurrentPatience = Patience;
        PatienceBar.SetMaxPatience(Patience);
        PatienceBar.gameObject.SetActive(false);
        OrderCanvas.SetActive(false);
        

        // choose food
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(CustomerManager.Instance.ExitPoint.position, transform.position) < 1f)
        {
            Destroy(gameObject);
        }

        if (IsDone) return;

        if (QueuePosition == 0)
        {
            PatienceBar.gameObject.SetActive(true);
            OrderCanvas.SetActive(true);
            OrderImage.sprite = Order.icon;
            CurrentPatience -= Time.deltaTime;
        }

        if(CurrentPatience <= 0){
            Instantiate(ReactionsPrefabs[1], transform.position, Quaternion.identity, transform);
            PatienceBar.gameObject.SetActive(false);
            OrderCanvas.SetActive(false);
            GameplayManager.Instance.StarUI.StarCount--;
            CustomerManager.Instance.NextInLine();
            IsDone = true;
        }


    }

    public void EvaluateOrder(Item item){
        PatienceBar.gameObject.SetActive(false);
        OrderCanvas.SetActive(false);
        IsDone = true;
        if(item.itemName == "Raw_Beef" || item.itemName == "Burnt_Salad"){
            GameplayManager.Instance.StarUI.StarCount--;
            Instantiate(ReactionsPrefabs[2], transform.position, transform.rotation, transform);
              
        }
        else if(item.itemName == Order.itemName){

            Instantiate(ReactionsPrefabs[0], transform.position, transform.rotation, transform);
            
        }else{
            GameplayManager.Instance.StarUI.StarCount--;
            Instantiate(ReactionsPrefabs[1], transform.position, transform.rotation, transform);
        }
        CustomerManager.Instance.NextInLine();

    }
}
