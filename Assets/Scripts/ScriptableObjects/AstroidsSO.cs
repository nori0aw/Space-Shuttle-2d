using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AstroidsSO : ScriptableObject
{
    // Start is called before the first frame update
     [SerializeField] public GameObject[] Astroids;
}
