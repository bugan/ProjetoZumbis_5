using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlaArma : MonoBehaviour
{
    [SerializeField]
    private GameObject bala;
    [SerializeField]
    private Transform canoDaArma;
    [SerializeField]
    private UnityEvent aoAtirar;
    [SerializeField]
    private ReservaBase reserva;

 
    private void Update()
    {
        var toquesNaTela = Input.touches;

        for (int i = 0; i < toquesNaTela.Length; i++)
        {
            var toque = toquesNaTela[i];
            if (toque.phase == TouchPhase.Began)
            {
                this.Atirar();
            }
        }
    }

    private void Atirar()
    {
        if (this.reserva.TemObjeto())
        {
            var bala = this.reserva.PegarObjeto();
            bala.GameObject.transform.position = this.canoDaArma.position;
            bala.GameObject.transform.rotation = this.canoDaArma.rotation;
            this.aoAtirar.Invoke();
        }
    }
}

