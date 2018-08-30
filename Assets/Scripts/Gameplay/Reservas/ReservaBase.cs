using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaBase : MonoBehaviour, IReservaDeObjetos
{

    [SerializeField]
    protected GameObject prefab;

    protected Stack<IReservavel> reservaDeObjetos;

    protected virtual void Awake()
    {
        this.reservaDeObjetos = new Stack<IReservavel>();
    }

    protected void OnValidate()
    {
        var reservavel = this.prefab.GetComponent<IReservavel>();
        if (reservavel == null)
        {
            this.prefab = null;
            throw new Exception("Atributo [prefab] requer um objeto que contenha a implementação da interface [IReservavel]");
        }
    }

    protected void CriarObjeto()
    {
        var instancia = GameObject.Instantiate(this.prefab, this.transform);
        var objetoDaReserva = instancia.GetComponent<IReservavel>();
        objetoDaReserva.Reserva = this;
        this.DevolverObjeto(objetoDaReserva);
    }

    public virtual void DevolverObjeto(IReservavel objeto)
    {
        throw new System.NotImplementedException();
    }

    public virtual IReservavel PegarObjeto()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool TemObjeto()
    {
        throw new System.NotImplementedException();
    }
}
