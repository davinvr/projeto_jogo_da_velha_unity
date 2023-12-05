using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public int jogadorSelecionado; // determina de quem é a vez da jogada, 0 = x e 1 = o 
    public int countJogada; // conta a quantidade de jogadas 
    public GameObject[] iconeJogada; // mostra de quem é a vez
    public Sprite[] iconePlay; // 0 = x e 1 = y
    public Button[] espacosJogoDaVelha; // espaços para apertar
    public int[] espacosMarcados; // identifica quais espaços foram marcados pelos respectivos jogadores
    public Text textoGanhador; // guarda o componente de texto do ganhador
    public GameObject[] linhaGanhador; // guarda as linhas para mostrar como o jogador ganhou
    public GameObject painelGanhador;
    public int jogadorXScore;
    public int jogadorOScore;
    public Text jogadorXScoreText;
    public Text jogadorOScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup(){
        jogadorSelecionado = 0;
        countJogada = 0;
        iconeJogada[0].SetActive(true);
        iconeJogada[1].SetActive(false);

        for(int i = 0; i < espacosJogoDaVelha.Length; i++){
            espacosJogoDaVelha[i].interactable = true;
            espacosJogoDaVelha[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0; i < espacosMarcados.Length; i++){
            espacosMarcados[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotaoJogoDaVelha(int NumeroGrid){
        espacosJogoDaVelha[NumeroGrid].image.sprite = iconePlay[jogadorSelecionado];
        espacosJogoDaVelha[NumeroGrid].interactable = false;

        espacosMarcados[NumeroGrid] = jogadorSelecionado + 1;
        countJogada++;
        if(countJogada > 4){
            bool ganhador = CheckGanhador();
            if(countJogada == 9 && ganhador == false){
                Empate();
            }
        }

        if(jogadorSelecionado == 0){
            jogadorSelecionado = 1;
            iconeJogada[0].SetActive(false);
            iconeJogada[1].SetActive(true);
        }else if(jogadorSelecionado == 1){
            jogadorSelecionado = 0;
            iconeJogada[0].SetActive(true);
            iconeJogada[1].SetActive(false);
        }
    }

    bool CheckGanhador(){

        int s1 = espacosMarcados[0] + espacosMarcados[1] + espacosMarcados[2];
        int s2 = espacosMarcados[3] + espacosMarcados[4] + espacosMarcados[5];
        int s3 = espacosMarcados[6] + espacosMarcados[7] + espacosMarcados[8];

        int s4 = espacosMarcados[0] + espacosMarcados[3] + espacosMarcados[6];
        int s5 = espacosMarcados[1] + espacosMarcados[4] + espacosMarcados[7];
        int s6 = espacosMarcados[2] + espacosMarcados[5] + espacosMarcados[8];

        int s7 = espacosMarcados[0] + espacosMarcados[4] + espacosMarcados[8];
        int s8 = espacosMarcados[2] + espacosMarcados[4] + espacosMarcados[6];
        
        var solucoes = new int[] {s1, s2, s3, s4, s5, s6, s7, s8};

        for(int i = 0; i < solucoes.Length; i++){
            if(solucoes[i] == 3*(jogadorSelecionado+1)){
                MostrarGanhador(i);
                return true;    
            }
        }
        return false;
    }

    void MostrarGanhador(int index){
        painelGanhador.gameObject.SetActive(true);
        if(jogadorSelecionado == 0){
            jogadorXScore++;
            jogadorXScoreText.text = jogadorXScore.ToString();
            textoGanhador.text = "Jogador X ganhou!";
        }else if(jogadorSelecionado == 1){
            jogadorOScore++;
            jogadorOScoreText.text = jogadorOScore.ToString();
            textoGanhador.text = "Jogador O ganhou!";
        }
        linhaGanhador[index].SetActive(true);
    }

    public void Revanche(){
        GameSetup();

        for(int i = 0; i < linhaGanhador.Length; i++){
            linhaGanhador[i].SetActive(false);
        }

        painelGanhador.SetActive(false);
    } 

    public void Restart(){
        Revanche();
        jogadorXScore = 0;
        jogadorOScore = 0;
        jogadorXScoreText.text = "0";
        jogadorOScoreText.text = "0";
    }

    void Empate(){
        painelGanhador.SetActive(true);
        textoGanhador.text = "Empate!";
    }
}
