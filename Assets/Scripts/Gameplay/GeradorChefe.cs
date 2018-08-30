using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{

    public float tempoEntreGeracoes = 60;
    public Transform[] PosicoesPossiveisDeGeracao;

    [SerializeField]
    private ReservaBase reserva;

    private float tempoParaProximaGeracao = 0;
    private Transform jogador;
    private ControlaInterface scriptControlaInteface;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInteface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            if (this.reserva.TemObjeto())
            {
                var chefe = this.reserva.PegarObjeto();
                chefe.GameObject.transform.position = posicaoDeCriacao;
                scriptControlaInteface.AparecerTextoChefeCriado();
                tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
            }
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador ()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}
