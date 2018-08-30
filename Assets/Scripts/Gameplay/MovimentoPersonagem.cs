using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovimentoPersonagem : MonoBehaviour
{
    [SerializeField]
    protected float velocidade;
    public Vector3 Direcao {get; protected set;}

    private Rigidbody meuRigidbody;
    

    void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    public void SetDirecao(Vector2 novadirecao)
    {
        this.Direcao = new Vector3(novadirecao.x, 0, novadirecao.y);
    }

    public void SetDirecao(Vector3 novadirecao)
    {
        this.Direcao = novadirecao;
    }


    public void Movimentar()
    {
        if (this.Direcao.magnitude > 0)
        {
            meuRigidbody.MovePosition(
                meuRigidbody.position +
                this.Direcao * this.velocidade * Time.deltaTime);
        }
    }

    public void Rotacionar(Vector3 direcao)
    {
        if (direcao.magnitude > 0)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            meuRigidbody.MoveRotation(novaRotacao);
        }
    }

    public void Morrer()
    {
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
