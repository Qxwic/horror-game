using UnityEngine;

public class EnemyFollow3D : MonoBehaviour
{
    public Transform player;      // Игрок, за которым будет следовать враг
    public float speed = 5f;      // Скорость движения врага
    public float rotationSpeed = 5f;  // Скорость поворота врага
    private Animator animator;    // Ссылка на компонент Animator

    void Start()
    {
        animator = GetComponent<Animator>();  // Получаем компонент Animator
    }

    void Update()
    {
        // Проверяем, что игрок существует
        if (player == null) return;

        // Вычисляем направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // Двигаем врага к игроку
        MoveTowardsPlayer(direction);

        // Обновляем анимацию
        AnimateMovement(direction);
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
        // Вычисляем новое положение
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Обновляем положение врага
        transform.position = newPosition;

        // Плавно поворачиваем врага к игроку
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AnimateMovement(Vector3 direction)
    {
        // Если враг движется, включаем анимацию
        bool isMoving = direction.magnitude > 0.1f;
        animator.SetBool("IsWalking", isMoving);
    }
}
