using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orologio : MonoBehaviour
{
    // costants
    const string NOME_LANCETTA_ORE = "LancettaOre";
    const string NOME_LANCETTA_MINUTI = "LancettaMinuti";
    const string NOME_CENTRO_OROLOGIO = "CentroOrologio";


    [SerializeField] Sprite spriteQuadrante;
    [SerializeField] Sprite spriteLancettaOre;
    [SerializeField] Sprite spriteLancettaMinuti;
    [SerializeField] Sprite spriteCentroOrologio;
    [SerializeField] Color coloreLancettaOre = Color.red;
    [SerializeField] Color coloreLancettaMinuti = Color.green;

    // member variables
    Lancetta lancettaOre;
    Lancetta lancettaMinuti;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        lancettaOre = transform.Find(NOME_LANCETTA_ORE).GetComponent<Lancetta>();
        lancettaMinuti = transform.Find(NOME_LANCETTA_MINUTI).GetComponent<Lancetta>();

        SetupAspettoOrologio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupAspettoOrologio()
    {
        if (spriteLancettaOre)
        {
            SpriteRenderer renderer = transform.Find(NOME_LANCETTA_ORE).Find("Lancetta").GetComponent<SpriteRenderer>();
            renderer.sprite = spriteLancettaOre;
            renderer.color = coloreLancettaOre;
        }

        if (spriteLancettaOre)
        {
            SpriteRenderer renderer = transform.Find(NOME_LANCETTA_MINUTI).Find("Lancetta").GetComponent<SpriteRenderer>();
            renderer.sprite = spriteLancettaMinuti;
            renderer.color = coloreLancettaMinuti;
        }

        if (spriteCentroOrologio)
        {
            SpriteRenderer renderer = transform.Find(NOME_CENTRO_OROLOGIO).GetComponent<SpriteRenderer>();
            renderer.sprite = spriteCentroOrologio;
        }
    }
}
