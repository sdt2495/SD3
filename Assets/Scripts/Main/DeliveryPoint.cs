using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public int colorIndex;

    [Header("SE")]
    public AudioClip deliverySE;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player == null)
            return;

        if (player.currentPackage == null)
            return;

        if (player.currentPackage.colorIndex == colorIndex)
        {
            // 配達成功SE
            AudioSource.PlayClipAtPoint(
                deliverySE,
                transform.position);

            Debug.Log("配達成功！");

            GameManager.Instance.AddScore(100);

            player.currentPackage = null;

            GameManager.Instance.DeliveryCompleted();
        }
    }
}