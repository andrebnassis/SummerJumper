using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float velocidade;
    public float decreaseSizeRate = 8.0f;
    private Vector3 finalSize;
    private bool grewUp = false;
    private float originVelocidade;
    public float RollingWave = 1.0f;

    private bool reduce = false;

    // Use this for initialization
    void Start () {
        originVelocidade = velocidade;
        finalSize = transform.localScale;
        transform.localScale = new Vector3(0.0f, transform.localScale.y, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 textureOffset = GetComponentInChildren<MeshRenderer>().material.GetTextureOffset("_NoiseTex");
        var texture2D = new Vector2(textureOffset.x, textureOffset.y + RollingWave * Time.fixedDeltaTime);
        GetComponentInChildren<MeshRenderer>().material.SetTextureOffset("_NoiseTex", texture2D);

        transform.Translate(Vector3.back * velocidade*Time.fixedDeltaTime);

        if ( transform.localScale.x < finalSize.x && !grewUp)
        {
            transform.localScale = new Vector3(transform.localScale.x + finalSize.x * Time.deltaTime, transform.localScale.y, transform.localScale.z + finalSize.z * Time.deltaTime);
        }
        else
            if(transform.localScale.z >= finalSize.z && !grewUp)
        {
            grewUp = true;
        }
        else
        if( grewUp )
        {
            transform.localScale = new Vector3(transform.localScale.x - (finalSize.x / decreaseSizeRate) * Time.deltaTime, transform.localScale.y, transform.localScale.z - (finalSize.z / decreaseSizeRate) * Time.deltaTime);
        }

        if( transform.localScale.x < 0f )
        {
            Destroy(this.gameObject);
            return;
        }

        if( reduce )
        {
            velocidade -= velocidade * Time.deltaTime;
            transform.Translate(Vector3.left * (velocidade/4) * Time.fixedDeltaTime);
        }
    }

    public void Reduce()
    {
        decreaseSizeRate = 2;
        reduce = true;
    }

}
