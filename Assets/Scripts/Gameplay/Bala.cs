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

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position +
            transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch (objetoDeColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(1);
                inimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            case "ChefeDeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
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
