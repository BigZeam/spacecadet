using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectable : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f;
    ScalePunchAwake psm;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        psm = GetComponent<ScalePunchAwake>();
        if(psm!=null)
            psm.PunchScaleMe();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
