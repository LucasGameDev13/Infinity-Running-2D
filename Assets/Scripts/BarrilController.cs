using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilController : MonoBehaviour
{
    //Pegando as vari�veis de acesso do GameController
    private GameController _GameController;

    //Pegando as vari�veis do Rigidbody do barr�l
    private Rigidbody2D barrilRb;

    //Vari�vel para coletar os pontos do jogo;
    private bool pontuado;

    // Start is called before the first frame update
    void Start()
    {

        //Acessando os componentes do barr�l e do GameController;
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        barrilRb = GetComponent<Rigidbody2D>();

        //Pegando a velocidade e dando velocidade pro barril;
        barrilRb.velocity = new Vector2(_GameController.velocidadeObjeto, 0);

    }

    // Update is called once per frame
    void Update()
    {
       
        //Destruindo o barril ao sair da room;
        if(transform.position.x < _GameController.distanciaDestruir)
        {
            Destroy(this.gameObject);
        }

    }

    //Fun��o que ocorrer� ap�s o Update. Controlando pontos
    void LateUpdate()
    {
        //Se vari�vel pontuado for igual a false;
        if(pontuado == false)
        {
            //Se o x do barril for menor que o x do player, se o barril passar pelo player, ent�o
            if(transform.position.x < _GameController.posXPlayer)
            {
                //Pontuado � true, para parar a contagem;
                pontuado = true;

                //Metodo n�merar ganha 10 pontos.
                _GameController.pontuar(10); 
            }

        }
    }
}
