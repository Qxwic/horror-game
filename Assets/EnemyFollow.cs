using UnityEngine;

public class EnemyFollow3D : MonoBehaviour
{
    public Transform player;      // �����, �� ������� ����� ��������� ����
    public float speed = 5f;      // �������� �������� �����
    public float rotationSpeed = 5f;  // �������� �������� �����
    private Animator animator;    // ������ �� ��������� Animator

    void Start()
    {
        animator = GetComponent<Animator>();  // �������� ��������� Animator
    }

    void Update()
    {
        // ���������, ��� ����� ����������
        if (player == null) return;

        // ��������� ����������� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // ������� ����� � ������
        MoveTowardsPlayer(direction);

        // ��������� ��������
        AnimateMovement(direction);
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
        // ��������� ����� ���������
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // ��������� ��������� �����
        transform.position = newPosition;

        // ������ ������������ ����� � ������
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AnimateMovement(Vector3 direction)
    {
        // ���� ���� ��������, �������� ��������
        bool isMoving = direction.magnitude > 0.1f;
        animator.SetBool("IsWalking", isMoving);
    }
}
