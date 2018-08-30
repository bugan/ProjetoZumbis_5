using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CaixaDeSom : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clipesDeAudio;

    private AudioSource saidaDeAudio;

    private void Awake()
    {
        this.saidaDeAudio = this.GetComponent<AudioSource>();
    }

    public void Tocar()
    {
        var sorteado = Mathf.FloorToInt(Random.Range(0, this.clipesDeAudio.Length));
        this.saidaDeAudio.PlayOneShot(this.clipesDeAudio[sorteado]);
    }
}
