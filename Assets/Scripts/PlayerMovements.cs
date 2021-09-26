using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    public GameObject Player;
    public Animator Animator;

    public List<Transform> WayPoints; 
    // Start is called before the first frame update

    private NavMeshAgent playerAgent;
    void Start()
    {
        playerAgent = Player.GetComponent<NavMeshAgent>();
        Animator = Player.GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator.SetBool("isWalking", true);
            playerAgent.SetDestination(WayPoints[0].position);
        }
    }
}
