using UnityEngine;
using DG.Tweening;


public class SequenceAnimationDOTween : MonoBehaviour
{
    public Vector3 movePosition = new Vector3(0, 10, 0);
    public Vector3 rotateAngle = new Vector3(0, 180, 0);
    public Vector3 scaleSize = new Vector3(2, 2, 2);
    public float duration = 2.0f;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();

        // Add a move tween to the sequence
        sequence.Append(transform.DOMove(movePosition, duration));

        // Add a rotate tween to the sequence
        sequence.Append(transform.DORotate(rotateAngle, duration));

        // Add a scale tween to the sequence
        sequence.Append(transform.DOScale(scaleSize, duration));

        // Start the sequence
        sequence.Play();
    }
}
