using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private const string TAG_JOGADOR = "Jogador";
    [SerializeField]
    private float tempoEntreGeracoes = 60;
    [SerializeField]
    private ReservaBase reserva;
    [SerializeField]
    private Transform[] posicoesPossiveisDeGeracao;
    
    
    private Transform jogador;
    private ControlaInterface scriptControlaInteface;

    private void Start()
    {
        scriptControlaInteface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag(TAG_JOGADOR).transform;
        InvokeRepeating("GerarChefe", 0, this.tempoEntreGeracoes);
    }
    
    private IEnumerator GerarChefe()
    {
        Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
        if (this.reserva.TemObjeto())
        {
            var chefe = this.reserva.PegarObjeto();
            chefe.GameObject.transform.position = posicaoDeCriacao;
            scriptControlaInteface.AparecerTextoChefeCriado();
        }
        return null;
    }

    private Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in posicoesPossiveisDeGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}
