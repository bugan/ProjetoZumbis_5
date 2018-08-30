using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaFixa : ReservaBase
{
  
    [SerializeField]
    private int quantidade;

    override protected void Awake()
    {
        base.Awake();
        this.CriarTodosOsObjetos();
    }

    private void CriarTodosOsObjetos()
    {
        for (var i = 0; i < this.quantidade; i++)
        {
            this.CriarObjeto();
        }
    }

    override public bool TemObjeto()
    {
        return this.reservaDeObjetos.Count > 0;
    }

    override public void DevolverObjeto(IReservavel objeto)
    {
        objeto.AoEntrarNaReserva();
        this.reservaDeObjetos.Push(objeto);
    }

    override public IReservavel PegarObjeto()
    {
        var objeto = this.reservaDeObjetos.Pop();
        objeto.AoSairDaReserva();
        return objeto;
    }
}
