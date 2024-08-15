using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameController;

    private Rigidbody2D meuRB;
   
    // Start is called before the first frame update
    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        //Pegando os componentes do Rigidbody2D;
        meuRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movimento();

        limites();
    }

    void movimento()
    {
        //Pegando os movimentos na horizontal e na vertical
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Aplicando os valores nos eixos x e y no componente de velocidade do RigidBody2D;
        meuRB.velocity = new Vector2(horizontal * _GameController.velocidade, vertical * _GameController.velocidade);
    }

    void limites()
    {
        //Pegando a posição X e Y;
        float posX = transform.position.x;
        float posY = transform.position.y;

        //Se eu utrapassar os limites maximos e minimos, eu limito até os limites max e min;
        if (posX > _GameController.limiteMaxX)
        {
            posX = _GameController.limiteMaxX;
        }
        else if (posX < _GameController.limiteMinX)
        {
            posX = _GameController.limiteMinX;
        }

        if (posY > _GameController.limiteMaxY)
        {
            posY = _GameController.limiteMaxY;
        }
        else if (posY < _GameController.limiteMinY)
        {
            posY = _GameController.limiteMinY;
        }

        //Aplicando a nova condição para minhas posições vertical e horizontal;
        transform.position = new Vector3(posX, posY, 0);
    }

    //Função para colisão do player com os barris;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Tocando o som da colisão
        _GameController.fxSource.PlayOneShot(_GameController.fxBatida);

        //Mudando de cena e dando game over;
        _GameController.mudarCena("gameOver");
    }

}
