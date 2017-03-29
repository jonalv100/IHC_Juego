﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text loseText;

    private bool flag;
    private Rigidbody rb;
    private int count;
    void Start()
    {
	flag = false;
        rb = GetComponent<Rigidbody>();
        count = 0;
	loseText.text = "";
        winText.text = "";
        SetCountText();
    }

    IEnumerator Grow()
    {
	yield return new WaitForSeconds(0.5f);
        transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
	flag = false;
    }

    void Update()
    {
	if(!flag)
	{
	    if(transform.localScale.x > 1.0f)
	    {
	       loseText.text = "You lost :(";
	    }
	    else
	    {
	       StartCoroutine("Grow");
	    }
	    flag = true;
	    
	}
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 4)
        {
            winText.text = "You won!";
        }
	
    }
}