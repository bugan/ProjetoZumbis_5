using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaExtensivel : ReservaBase
{
    
    override public void DevolverObjeto(IReservavel objeto)
    {
        objeto.AoEntrarNaReserva();
        this.reservaDeObjetos.Push(objeto);
    }

    override public IReservavel PegarObjeto()
    {
        if(this.reservaDeObjetos.Count <= 0)
        {
            this.CriarObjeto();
        }
        
        var objeto = this.reservaDeObjetos.Pop();
        objeto.AoSairDaReserva();
        return objeto;
    }

    override public bool TemObjeto()
    {
        return true;
    }
}
