using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float waitDuration;
    [SerializeField] float stateDuration;

    private Transform myTransform;
    private float stateTimer;
    private float waitTimer;
    private Vector3 moveDir = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        stateTimer = stateDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateTimer > 0)
        {
            waitTimer = waitDuration;
            stateTimer -= Time.deltaTime;
            myTransform.position += (moveDir * moveSpeed);
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                stateTimer = stateDuration;
                moveDir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            }
        }
    }
}