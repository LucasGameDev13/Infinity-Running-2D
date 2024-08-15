using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController _PlayerController;

    //Cabe�alho
    [Header("Config. Personagem")]

    //Vari�veis de movimento 
    public float        velocidade;

    // Vari�veis para limitar o personagem na room;
    public float        limiteMaxX;
    public float        limiteMaxY;
    public float        limiteMinX;
    public float        limiteMinY;

    //Vari�vel velocidade para controlar os objetos;
    [Header("Config. Objetos")]
    public float        velocidadeObjeto;

    //Vari�vel distancia para destruir a ponte ap�s sair da room;
    public float        distanciaDestruir;

    //Vari�vel para pegar o tamanho da ponte e poder instanciar uma nova; 
    public float        tamanhoPonte;

    //Vari�vel para criar um novo objeto
    public GameObject   pontePrefab;

    //Vari�veis para controlar as informa��es dos barris
    [Header("Config. Barril")]

    //Vari�veis pra determinar as posi��es dos barris na room
    public float posYTop;
    public float posYDown;

    //Profundidade Cima e baixo;
    public int orderTop;
    public int orderDown;

    //Objeto do barril
    public GameObject barrilPrefab;

    //Vari�vel para criar um Delay na cria��o dos objetos;
    public float tempoSpawn;


    //Vari�vel para controlar a posi��o do player em rela��o aos barris;
    [Header("Globals")]
    //Vari�vel para desenhar pontos;
    public TextMeshProUGUI txtScore;
    public float posXPlayer;
    //Vari�vel pontos;
    public int score;

    //Vari�veis para controlar o som da pontua��o
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
        //Pegando a posi��o x do player;
        posXPlayer = _PlayerController.transform.position.x;
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    //Criando uma corrotina, fun��o para controlar o tempo de uso da fun��o, podendo retorna-la de acordo com o tempo dado;
    IEnumerator spawnBarril()
    {
        //Estabelecendo o tempo de espera;
        yield return new WaitForSeconds(tempoSpawn);


        //Posi��o Y do barril e posi��o da layer;
        float posiY = 0;
        int order = 0;

        //Criando sele��o para cria��o dos barris
        int rand = Random.Range(0, 100);

        //Definindo a cria��o dos barris;
        if(rand < 50)
        {
            //Criando o objeto na posi��o superior
            posiY = posYTop;
            order = orderTop;
        }
        else 
        {
            //Criando o objeto na posi��o Inferior
            posiY = posYDown;
            order = orderDown;
        }

        //Criando o objeto
        GameObject temp = Instantiate(barrilPrefab);

        //Mudando a posi��o
        temp.transform.position = new Vector3(temp.transform.position.x, posiY, 0);

        //Acessando a ordem da layer diretamente do objeto
        temp.GetComponent<SpriteRenderer>().sortingOrder = order;

        //Resetando o alarme;
        StartCoroutine("spawnBarril");
    }

    //Fun��o para contabilizar a pontua��o
    public void pontuar(int qtdPontos)
    {
        score += qtdPontos;
        txtScore.text = "Score : " + score.ToString();
        
        //Colocando o som da pontua��o
        fxSource.PlayOneShot(fxPontos);
    }

    //Fun��o para mudar cena pro game over;
    public void mudarCena(string _cenaDestino)
    {
        //Carregando a sena para qual eu selecionei ir
        SceneManager.LoadScene(_cenaDestino);
    }


}
