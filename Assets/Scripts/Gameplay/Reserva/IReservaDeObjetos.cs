using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReservaDeObjetos {

    void DevolverObjeto(IReservavel objeto);
    IReservavel PegarObjeto();
    bool TemObjeto();
}
