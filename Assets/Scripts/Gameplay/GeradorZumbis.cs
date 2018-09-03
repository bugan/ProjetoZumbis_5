using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    private const string TAG_JOGADOR = "Jogador";
    [SerializeField]
    private ReservaBase reserva;
    [SerializeField]
    private float TempoGerarZumbi = 1;
    [SerializeField]
    private LayerMask LayerZumbi;

    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorParaGeracao = 20;
    
    private GameObject jogador;

    private void Start()
    {
        jogador = GameObject.FindWithTag(TAG_JOGADOR);
        
        InvokeRepeating("GerarZumbi", 0, this.TempoGerarZumbi);
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }


    private bool EstaLongeOsuficiente()
    {
        return Vector3.Distance(transform.position,
            jogador.transform.position) >
            DistanciaDoJogadorParaGeracao;
    }

    private IEnumerator GerarZumbi()
    {
        if (!EstaLongeOsuficiente()) return null;
        
        Vector3 posicaoDeCriacao = ProcurarPosicaoValida();

        if (this.reserva.TemObjeto())
        {
            CriarZumbi(posicaoDeCriacao);
        }
        return null;
    }

    private void CriarZumbi(Vector3 posicaoDeCriacao)
    {
        var zumbi = this.reserva.PegarObjeto();
        zumbi.GameObject.transform.position = posicaoDeCriacao;
    }

    private Vector3 ProcurarPosicaoValida()
    {
        Collider[] colisores;
        Vector3 posicaoDeCriacao;
        do
        {
            posicaoDeCriacao = SortearPosicaoDentroDoGerador();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
        } while (colisores.Length > 0);

        return posicaoDeCriacao;
    }

    private Vector3 SortearPosicaoDentroDoGerador()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
