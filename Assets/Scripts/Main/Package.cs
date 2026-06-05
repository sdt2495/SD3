using UnityEngine;

public class Package : MonoBehaviour
{
    public int colorIndex;

    [Header("SE")]
    public AudioClip pickUpSE;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("‰½‚©‚ªG‚ê‚½: " + other.name);

        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player == null)
            return;

        if (player.currentPackage != null)
            return;

        player.currentPackage = this;

        GameManager.Instance.SetCurrentPackage(gameObject);

        // ‰×•¨æ“¾SE
        AudioSource.PlayClipAtPoint(
            pickUpSE,
            transform.position);

        gameObject.SetActive(false);

        Debug.Log("‰×•¨æ“¾I");
    }
}