using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class BotaoAnalogico : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private Image imagemFundo;
    [SerializeField]
    private Image imagemJoystick;

    [SerializeField]
    private MeuEventoDinamicoVec2 aoMudaOValor;

    public void OnDrag(PointerEventData DadosDoClique)
    {
        Vector2 posicaoClique = AcharPosicaoDoclique(DadosDoClique);

        Vector2 posicaoRelativaAAncoraDaImagem = CalcularPosicaoRelativa(posicaoClique);
        posicaoRelativaAAncoraDaImagem = LimitarTamanho(posicaoRelativaAAncoraDaImagem);

        PosicionarStick(posicaoRelativaAAncoraDaImagem);
        
        this.aoMudaOValor.Invoke(posicaoRelativaAAncoraDaImagem);
    }


    public void OnPointerDown(PointerEventData DadosDoClique)
    {
        this.OnDrag(DadosDoClique);
    }

    public void OnPointerUp(PointerEventData DadosDoClique)
    {
        
        PosicionarStick(Vector2.zero);

        this.aoMudaOValor.Invoke(Vector2.zero);
    }


    private void PosicionarStick(Vector2 posicaoClique)
    {
        this.imagemJoystick.rectTransform.position = 
            posicaoClique * this.TamanhoDaImagemDeFundo()
            + (Vector2)this.imagemFundo.rectTransform.position;
    }

    private float TamanhoDaImagemDeFundo()
    {
        var rectTransform = this.imagemFundo.GetComponentInParent<RectTransform>();
        var tamanho = rectTransform.rect.width / 2;
        return tamanho;
    }

    private Vector2 AcharPosicaoDoclique(PointerEventData DadosDoClique)
    {
        Vector2 posicaoClique;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
               this.imagemFundo.rectTransform,
               DadosDoClique.position,
               DadosDoClique.enterEventCamera,
               out posicaoClique
            );
        return posicaoClique;
    }

    private Vector2 CalcularPosicaoRelativa(Vector2 posicaoClique)
    {
        return posicaoClique / TamanhoDaImagemDeFundo();
    }

    private static Vector2 LimitarTamanho(Vector2 posicaoRelativaAAncoraDaImagem)
    {
        if (posicaoRelativaAAncoraDaImagem.magnitude > 1)
        {
            posicaoRelativaAAncoraDaImagem = posicaoRelativaAAncoraDaImagem.normalized;
        }

        return posicaoRelativaAAncoraDaImagem;
    }
}

[Serializable]
public class MeuEventoDinamicoVec2 : UnityEvent<Vector2> { }
