using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController _PlayerController;

    //Cabeçalho
    [Header("Config. Personagem")]

    //Variáveis de movimento 
    public float        velocidade;

    // Variáveis para limitar o personagem na room;
    public float        limiteMaxX;
    public float        limiteMaxY;
    public float        limiteMinX;
    public float        limiteMinY;

    //Variável velocidade para controlar os objetos;
    [Header("Config. Objetos")]
    public float        velocidadeObjeto;

    //Variável distancia para destruir a ponte após sair da room;
    public float        distanciaDestruir;

    //Variável para pegar o tamanho da ponte e poder instanciar uma nova; 
    public float        tamanhoPonte;

    //Variável para criar um novo objeto
    public GameObject   pontePrefab;

    //Variáveis para controlar as informações dos barris
    [Header("Config. Barril")]

    //Variáveis pra determinar as posições dos barris na room
    public float posYTop;
    public float posYDown;

    //Profundidade Cima e baixo;
    public int orderTop;
    public int orderDown;

    //Objeto do barril
    public GameObject barrilPrefab;

    //Variável para criar um Delay na criação dos objetos;
    public float tempoSpawn;


    //Variável para controlar a posição do player em relação aos barris;
    [Header("Globals")]
    //Variável para desenhar pontos;
    public TextMeshProUGUI txtScore;
    public float posXPlayer;
    //Variável pontos;
    public int score;

    //Variáveis para controlar o som da pontuação
    [Header("FX Sound")]
    public AudioSource fxSource;
    public AudioClip fxPontos;
    public AudioClip fxBatida;

   


    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {

        //Acessando os componentes do player
        _PlayerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        //Chamando o alarme;
        StartCoroutine("spawnBarril");

    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    // Update is called once per frame
    void LateUpdate()
    {
        //Pegando a posição x do player;
        posXPlayer = _PlayerController.transform.position.x;
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    //Criando uma corrotina, função para controlar o tempo de uso da função, podendo retorna-la de acordo com o tempo dado;
    IEnumerator spawnBarril()
    {
        //Estabelecendo o tempo de espera;
        yield return new WaitForSeconds(tempoSpawn);


        //Posição Y do barril e posição da layer;
        float posiY = 0;
        int order = 0;

        //Criando seleção para criação dos barris
        int rand = Random.Range(0, 100);

        //Definindo a criação dos barris;
        if(rand < 50)
        {
            //Criando o objeto na posição superior
            posiY = posYTop;
            order = orderTop;
        }
        else 
        {
            //Criando o objeto na posição Inferior
            posiY = posYDown;
            order = orderDown;
        }

        //Criando o objeto
        GameObject temp = Instantiate(barrilPrefab);

        //Mudando a posição
        temp.transform.position = new Vector3(temp.transform.position.x, posiY, 0);

        //Acessando a ordem da layer diretamente do objeto
        temp.GetComponent<SpriteRenderer>().sortingOrder = order;

        //Resetando o alarme;
        StartCoroutine("spawnBarril");
    }

    //Função para contabilizar a pontuação
    public void pontuar(int qtdPontos)
    {
        score += qtdPontos;
        txtScore.text = "Score : " + score.ToString();
        
        //Colocando o som da pontuação
        fxSource.PlayOneShot(fxPontos);
    }

    //Função para mudar cena pro game over;
    public void mudarCena(string _cenaDestino)
    {
        //Carregando a sena para qual eu selecionei ir
        SceneManager.LoadScene(_cenaDestino);
    }


}
