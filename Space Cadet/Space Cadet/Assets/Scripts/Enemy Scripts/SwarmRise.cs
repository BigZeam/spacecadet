using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmRise : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform body;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D other) {
        body.Translate(Vector2.up * 5 * Time.deltaTime);
    }
}
