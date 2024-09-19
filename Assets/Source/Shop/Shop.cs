using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _snippetField;
    [SerializeField] private GameObject _shop;
    private bool _inTrigger;

    private void Start() {
        _snippetField.gameObject.SetActive(false);
        _shop.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
      _snippetField.gameObject.SetActive(true); 
      _inTrigger = true;     
    }
     private void OnTriggerExit2D(Collider2D other)
    {
      _snippetField.gameObject.SetActive(false);  
      _inTrigger = false;    
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&_inTrigger) {
            _shop.SetActive(!_shop.activeSelf);
        }

        if (_shop.activeSelf)
        {
          Time.timeScale = 0;
        }
        else
        {
          Time.timeScale = 1;
        }
    }
}
