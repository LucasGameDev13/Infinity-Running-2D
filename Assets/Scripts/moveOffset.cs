using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour
{
    //Pegando as vari�veis para acessar os componentes do Render e do Material;
    private Renderer meshRender;
    private Material currentMaterial;

    //Criando as vari�veis para incrementar a velocidade dos materiais(background);
    public float    incrementoOffset;
    public float    speed;
    private float   offSet;

    //Vari�veis para atualizar a posi��o dos backgrounds na room;
    public string   sortingLayer;
    public int      orderInLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Acessando os componentes do Renderer
        meshRender = GetComponent<Renderer>();

        //Acessando os componentes sorting in layer e sorting order do Render e dando valores a eles;
        meshRender.sortingLayerName = sortingLayer;
        meshRender.sortingOrder = orderInLayer;

        //Dando a minha vari�vel current Material o valor do componente do meshRender.material;
        currentMaterial = meshRender.material;
    }

    // Update is called once per frame
    void Update()
    {
        //Incrementando o valor do offSet;
        offSet += incrementoOffset;

        //Aplicando o valor no incremento no componente offset do material;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet * speed, 0));
    }
}
