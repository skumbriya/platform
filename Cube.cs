using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Cube : MonoBehaviour
{
    Rigidbody rb;
    Transform transf;

    static int score = 0;

    float vertical;
    float horizontal;
    float jumpForce = 10f;

    bool isGrounded = false;

    [SerializeField] TextMeshProUGUI scoretext;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
        scoretext.text = "Монетки: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        rb.AddRelativeForce(0, 0, vertical * 5f);
        transf.Rotate(0, horizontal, 0);

        if(Input.GetKeyDown("space") && isGrounded == true){
            rb.drag = 3;
            rb.angularDrag = 3;
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }
    
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "ground"){
            rb.drag = 0;
            rb.angularDrag = 0;
            isGrounded = true;
        }

        if(col.gameObject.tag == "coin"){
            Destroy(col.gameObject);
            score++;
            scoretext.text = "Монетки: " + score;

            if(score == 9){
                print("Молодец, ты собрал все монетки");
                score  = 0;
                SceneManager.LoadScene("Scenes/2");
            }
        }

        if(col.gameObject.tag == "danger"){
            print("О неи, ты упал");
            SceneManager.LoadScene("Scenes/2");
            score = 0;
            scoretext.text = "Монетки: " + score;
        }
    }

    void OnCollisionExit(Collision col){
        if(col.gameObject.tag == "ground"){
            isGrounded = false;
        }
    }
}
