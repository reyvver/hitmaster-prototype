using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    public GameObject Player;
    public List<Transform> WayPoints; 
    // Start is called before the first frame update

    private NavMeshAgent playerAgent;
    void Start()
    {
        playerAgent = Player.GetComponent<NavMeshAgent>();

        playerAgent.SetDestination(WayPoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
