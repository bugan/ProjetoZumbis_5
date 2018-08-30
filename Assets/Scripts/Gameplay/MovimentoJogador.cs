using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private CaixaDeSom somPassos;

    public void AudioPasso()
    {
        this.somPassos.Tocar();
    }
    public void RotacaoJogador ()
    {
            Rotacionar(this.Direcao);
    }
}
