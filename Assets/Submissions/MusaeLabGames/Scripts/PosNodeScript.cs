using System;
using UnityEngine;

public class PosNodeScript: MonoBehaviour
{
    // Node connection
    [Header("Node connection")]
    public GameObject[] nodes;
    public GameObject skin_arrow_base;

    protected GameObject[] list_arrows;

    // Arrow flickering
    [Header("SSVEP configuration")]
    static public float emissionMinIntensity = 0f;
    static public float emissionMaxIntensity = 5f;
    static public float minFrequency = 2f;
    static public float maxFrequency = 10f;
    static public Color emissionColor = new Color(1f, 1f, 1f, 1f);

    protected int[] freqs = { 10, 20, 30, 40, 50, 60 };
    protected bool enableEmissionFlickering = false;
    protected float emissionFrequency = 10; // in Hz

    // Event handler
    static public event Action<GameObject> OnHasPlayer;
    static public event Action<GameObject> OnRequestedDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        BuildArrows();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseInput();
        ArrowFlickering();
    }

    public void OnEnable()
    {
        Character.OnReachDestination += CheckNodeActive;
    }

    public void OnDisable()
    {
        Character.OnReachDestination -= CheckNodeActive;
    }

    protected void CheckNodeActive(GameObject destination)
    {
        if(gameObject == destination)
        {
            ActivateNode();
            OnHasPlayer?.Invoke(gameObject);
        }
        else
        {
            DisableArrows();
        }
    }

    public void ActivateNode()
    {
        EnableArrows();
    }

    public void BuildArrows()
    {
        Debug.Log("Building arrows");
        int n_nodes = nodes.Length;
        list_arrows = new GameObject[n_nodes];
        Debug.Log("n_nodes " + n_nodes.ToString());

        Vector3 flag_pos = transform.position;
        
        for(int ix=0; ix < n_nodes; ix++)
        {
            GameObject node = nodes[ix];
            GameObject newArrow = Instantiate(skin_arrow_base, flag_pos, Quaternion.identity);

            // Place arrow and move it forward in the direction of the next node
            newArrow.transform.LookAt(node.transform);
            Vector3 arrow_pos = newArrow.transform.position;
            Vector3 newPos = Vector3.MoveTowards(arrow_pos, node.transform.position, 20);
            newArrow.transform.position = newPos;

            list_arrows[ix] = newArrow;
        }

        // Enable arrows only if player has reached this node
        DisableArrows();
    }

    public void DisableArrows()
    {
        foreach(GameObject arrow in list_arrows)
        {
            arrow.SetActive(false); 
        }
        DesactivateFlickering();
    }

    public void EnableArrows()
    {
        foreach (GameObject arrow in list_arrows)
        {
            arrow.SetActive(true);
        }
        Debug.Log("Activate flickering");
        ActivateFlickering();
    }

    public void CheckMouseInput()
    {
        // Warning: Target object should have a Collider component!
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.transform.gameObject;
                for(int ix=0; ix<list_arrows.Length; ix++)
                {
                    GameObject arrow = list_arrows[ix];
                    if(arrow == target)
                    {
                        Debug.Log("Arrow touched");
                        // Send corresponding node the arrow is pointing to
                        GameObject node = nodes[ix];
                        OnRequestedDisplacement?.Invoke(node);
                    }
                }
            }
        }
    }

    public void ActivateFlickering()
    {
        enableEmissionFlickering = true;
        // Modify material to allow flickering (for SSVEP feedback)
        foreach (GameObject arrow in list_arrows)
        {
            Material material = arrow.GetComponent<Renderer>().material;
            material.EnableKeyword("_EMISSION");
        }
    }

    public void DesactivateFlickering()
    {
        enableEmissionFlickering = false;
        foreach (GameObject arrow in list_arrows)
        {
            Material material = arrow.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", emissionColor * 0);
        }
    }

    public void SetColorHSLEmission(float hue, float sat = 1f, float lum = 1f)
    {
        foreach (GameObject arrow in list_arrows)
        {
            Material material = arrow.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", Color.HSVToRGB(hue, sat, lum));
        }
    }

    public void SetColorEmission(Color color)
    {
        foreach (GameObject arrow in list_arrows)
        {
            Material material = arrow.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", color);
        }
    }

    public void ArrowFlickering()
    {
        // Change emission intensity
        if (enableEmissionFlickering)
        {
            foreach (GameObject arrow in list_arrows)
            {
                Material material = arrow.GetComponent<Renderer>().material;
                float emssionRangeIntensity = emissionMaxIntensity - emissionMinIntensity;
                float intensity = Mathf.PingPong(Time.time * emssionRangeIntensity * emissionFrequency,
                                                 emssionRangeIntensity) + emissionMinIntensity;
                material.SetColor("_EmissionColor", emissionColor * intensity);
            }
        }
    }
}


