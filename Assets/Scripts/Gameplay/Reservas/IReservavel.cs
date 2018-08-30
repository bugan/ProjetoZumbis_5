using UnityEngine;

public interface IReservavel {
    void AoSairDaReserva();
    void AoEntrarNaReserva();
    GameObject GameObject { get; }
    IReservaDeObjetos Reserva { set; }
}
