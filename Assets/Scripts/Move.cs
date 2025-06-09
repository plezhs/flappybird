using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Move : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; //(-1,0,0)
    }
    public void des()
    {
        Destroy(this.gameObject);
    }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if(other.gameObject.CompareTag("Back"))
    //     {
    //         des();
    //     }
    // }
}
