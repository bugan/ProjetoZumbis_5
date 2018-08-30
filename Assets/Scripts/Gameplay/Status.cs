using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    
    public int VidaInicial;
    [HideInInspector]
    public int Vida;
    
    void Awake ()
    {
        Vida = VidaInicial;
    }
}
