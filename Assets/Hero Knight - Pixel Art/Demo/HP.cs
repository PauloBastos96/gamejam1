using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Sprite[] array_sprite;
    public Image canvas_image;
    public int HP = 4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            HP--;
            switch (HP)
            {
                case 3:
                    canvas_image.sprite = array_sprite[array_sprite.Length - 1];
                    break;
                case 2:
                    canvas_image.sprite = array_sprite[array_sprite.Length - 2];
                    break;
                case 1:
                    canvas_image.sprite = array_sprite[array_sprite.Length - 3];
                    break;
                default:
                    canvas_image.sprite = array_sprite[array_sprite.Length - 4];
                    break;
            }
        }
    }
}
