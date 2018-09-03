using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour, IReservavel
{

    public float Velocidade = 20;
    public AudioClip SomDeMorte;

    public GameObject GameObject
    {
        get
        {
            return this.gameObject;
        }
    }

    public IReservaDeObjetos Reserva
    {
        set
        {
            this.reserva = value;
        }
    }

    private IReservaDeObjetos reserva;
    private Rigidbody rigidbodyBala;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position +
            transform.forward * Velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        
        var inimigo = objetoDeColisao.GetComponent<IMatavel>();
        if(inimigo != null)
        {
            inimigo.TomarDano(1);
        }

        this.reserva.DevolverObjeto(this);
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }

    public void AoEntrarNaReserva()
    {
        this.gameObject.SetActive(false);
    }
}
