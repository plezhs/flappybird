using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Scoreup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Score.score+=1;
    }
}
