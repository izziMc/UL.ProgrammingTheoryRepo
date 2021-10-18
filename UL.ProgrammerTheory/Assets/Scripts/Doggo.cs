using UnityEngine;
using UnityEngine.AI;

public class Doggo : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Animator animator;

    public float maxDistance;

    public float timeStuck;
    public float stuckLimit;

    private Vector3 _lastPosition = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        ClickToMove();

        // Check for stuck at same point for longer than 3 seconds
        if (Vector3.Distance(transform.position, _lastPosition) <= 0.01f)
        {
            timeStuck += Time.deltaTime;
        }
        _lastPosition = transform.position;

        if (timeStuck >= stuckLimit)
        {
            ToggleWalking(false);
            ChooseRandomDestination();
        }

        // Arrived at destination
        if (Vector3.Distance(transform.position, agent.destination) <= 0.01f)
        {
            ToggleWalking(false);
        }
    }

    void ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move our agent
                agent.SetDestination(hit.point);
                ToggleWalking(true);
            }
        }
    }

    void ChooseRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1)) finalPosition = hit.position;
        agent.SetDestination(finalPosition);
        ToggleWalking(true);
    }

    void SetWalkAnimation(int value) => animator.SetInteger("Walk", value);

    void ToggleWalking(bool state)
    {
        if (state)
        {
            agent.isStopped = false;
            timeStuck = 0;
            SetWalkAnimation(1);
        }
        else
        {
            agent.isStopped = true;
            SetWalkAnimation(0);
        }
    }
}