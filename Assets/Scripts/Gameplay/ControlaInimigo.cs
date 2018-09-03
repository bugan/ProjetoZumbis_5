using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlaInimigo : MonoBehaviour, IMatavel, IReservavel
{
    private const string TAG_JOGADOR = "Jogador";
    public GameObject Jogador;
    public AudioClip SomDeMorte;
    public GameObject KitMedicoPrefab;
    public GameObject ParticulaSangueZumbi;

    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    private ControlaInterface scriptControlaInterface;

    [SerializeField]
    private UnityEvent aoMorrer;
    [SerializeField]
    private UnityEvent aoAtacar;
    [SerializeField]
    private UnityEvent aoSofrerDano;

    public GameObject GameObject { get { return this.gameObject; } }

    public IReservaDeObjetos Reserva { set { this.reserva = value; } }

    private IReservaDeObjetos reserva;

    private void Awake()
    {
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<Status>();

        AleatorizarZumbi();
    }

    private void Start()
    {
        Jogador = GameObject.FindWithTag(TAG_JOGADOR);
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
        else
        {
            AndarAteOJogador(distancia);
        }
    }
    
    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }

    public void AoEntrarNaReserva()
    {
        this.gameObject.SetActive(false);
    }

    public void TomarDano(int dano)
    {
        this.aoSofrerDano.Invoke();
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
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
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
    }

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar();
        }
    }

    private void AndarAteOJogador(float distancia)
    {
        direcao = Jogador.transform.position - transform.position;
        var podeAtacar = distancia <= 2.5;

        animacaoInimigo.Atacar(podeAtacar);

        if (!podeAtacar)
        {
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar();
        }
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    private void AtacaJogador()
    {
        this.aoAtacar.Invoke();
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    private void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    private void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
