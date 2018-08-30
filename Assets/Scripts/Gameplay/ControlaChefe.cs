using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel, IReservavel
{
    public IReservaDeObjetos Reserva { set { this.reserva = value; } }
    public GameObject GameObject { get { return this.gameObject; } }

    public GameObject KitMedicoPrefab;
    public Slider sliderVidaChefe;
    public Image ImagelSlider;
    public Color CorDaVidaMaxima, CorDaVidaMinima;
    public GameObject ParticulaSangueZumbi;

    [SerializeField]
    private UnityEvent aoMorrer;
    [SerializeField]
    private UnityEvent aoAtacar;
    [SerializeField]
    private UnityEvent aoSofrerDano;

    
    private IReservaDeObjetos reserva;
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
    }
    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true)
        {
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (estouPertoDoJogador)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador ()
    {
        this.aoAtacar.Invoke();
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        this.aoSofrerDano.Invoke();
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if (statusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        this.aoMorrer.Invoke();
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        this.reserva.DevolverObjeto(this);
    }

    void AtualizarInterface ()
    {
        sliderVidaChefe.value = statusChefe.Vida;
        float porcentagemDaVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
        ImagelSlider.color = corDaVida;
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }

    public void AoEntrarNaReserva()
    {
        this.gameObject.SetActive(false);
    }
}
