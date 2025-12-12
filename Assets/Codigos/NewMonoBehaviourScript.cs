using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    public Transform groundCheck;      // O objeto vazio nos pés
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;      // A layer do chão (MUITO IMPORTANTE)

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Visuals")]
    public Transform visual;           // O objeto filho com o Sprite/Animator
    private Animator anim;
    private Vector3 originalScale;     // Para salvar o tamanho original

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Proteção caso esqueça de arrastar o visual
        if (visual == null) visual = transform; 
        
        anim = visual.GetComponent<Animator>();

        // Salva o tamanho que você configurou no Inspector ao dar Play
        originalScale = visual.localScale;
    }

    void Update()
    {
        // 1. Verificar se está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Atualiza a animação
        if (anim != null)
            anim.SetBool("IsGrounded", isGrounded);

        // 2. Movimento Horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Nota: Se der erro em 'linearVelocity', troque por 'velocity' (versões antigas da Unity)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 3. Animação de Correr e Virar o Personagem
        if (anim != null)
            anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0f && isGrounded);

        // Lógica de virar usando o tamanho original salvo
        if (moveInput > 0.01f)
        {
            // Olha para a direita (escala original positiva)
            visual.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if(moveInput < -0.01f)
        {
            // Olha para a esquerda (inverte o X)
            visual.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // 4. Pulo
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            if (anim != null) anim.SetTrigger("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 5. Combate
        bool isFighting = Input.GetKey(KeyCode.LeftShift) && isGrounded;
        if (anim != null) 
            anim.SetBool("isFightPose", isFighting);

        if (isFighting)
        {
            // Para o personagem enquanto luta
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Mantém a gravidade (y), zera o (x)

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (anim != null) anim.SetTrigger("Punch");
            }
        }
    }

    // DESENHA O CÍRCULO VERMELHO NA TELA DE SCENE PARA AJUDAR
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
