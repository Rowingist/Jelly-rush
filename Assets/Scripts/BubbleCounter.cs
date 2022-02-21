using TMPro;
using UnityEngine;

public class BubbleCounter : MonoBehaviour
{
    [SerializeField] private PlayerBubblesMagnet _playerBubblesMagnet;
    [SerializeField] private TMP_Text _bubblesCount;

    private void Update()
    {
        _bubblesCount.text = _playerBubblesMagnet.GetBubblesCount().ToString();
    }
}
