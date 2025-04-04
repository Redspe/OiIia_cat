using UnityEditor.Rendering;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Componentes
    private BoxCollider2D bodyCollider;   // Collider do corpo
    private BoxCollider2D groundCollider; // Collider de detecção do chão
    #endregion

    #region Variáveis Públicas e Serializadas
    [SerializeField]
    private LayerMask groundLayer;  // Camada do chão
    [SerializeField]
    private Animator anim;  // Camada do chão

    public Rigidbody2D myRigidBody; // Rigidbody para movimentação

    [Header("Movimentação")]
    public float flapStrength = 10f; // Força do pulo
    public float walkStrength = 7f;  // Força da movimentação
    public bool isGrounded_Test;     // Indica se está no chão

    #endregion

    #region Métodos Unity
    void Start()
    {   
        setValuesBegin();
    }

    void Update()
    {   
        getMovinX();
        getMovinY();
        MoveAnim();
    }

    void getMovinY()
    {
        // Atualiza o vetor de velocidade atual
        Vector2 newVelocity = myRigidBody.linearVelocity;

                // Processa movimentação vertical (pulo)
        if (isGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            newVelocity.y = flapStrength * 10;
            Debug.Log("Pulou!");
        }

        // Aplica a nova velocidade ao Rigidbody
        myRigidBody.linearVelocity = newVelocity;
    }

    void getMovinX()
    {
        Vector2 newVelocity = myRigidBody.linearVelocity;

        if (Input.GetKey(KeyCode.A))
        {
            newVelocity.x = -walkStrength;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            newVelocity.x = walkStrength;

        }
        else
        {
            newVelocity.x *= 0.0000f;
            
        }

        myRigidBody.linearVelocity = newVelocity;
    }

    void setValuesBegin()
    {
        gameObject.name = "Bob";

        // Obtém os BoxColliders
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        if (colliders.Length >= 2)
        {
            bodyCollider = colliders[0];    // Primeiro componente para o corpo
            groundCollider = colliders[1];  // Segundo componente para detecção do chão
        }
        else
        {
            Debug.LogError("Os componentes BoxCollider2D não estão configurados corretamente.");
        }
    }

    void MoveAnim()
    {
        anim.SetFloat("HorizontalX", Mathf.Abs(myRigidBody.linearVelocity.x));

        if(myRigidBody.linearVelocityX == 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // Direita
        }
        else if (myRigidBody.linearVelocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // Esquerda
        }
        else if (myRigidBody.linearVelocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // Direita
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded_Test = true;
            Debug.Log("Entrou em contato com o chão!");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded_Test = false;
            Debug.Log("Saiu do chão!");
        }
    }
    #endregion

    #region Métodos Auxiliares
    private bool isGrounded()
    {
        // Desenha o BoxCast para visualização no editor
        Debug.DrawRay(groundCollider.bounds.center, Vector2.down * 0.1f, Color.red);

        // Verifica se o chão está em contato com o groundCollider
        RaycastHit2D groundHit = Physics2D.BoxCast(
            groundCollider.bounds.center,
            groundCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return groundHit.collider != null;
    }
    #endregion
}