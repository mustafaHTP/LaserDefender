using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float _spriteHeight;

    private void Awake()
    {
        _spriteHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
    }

    private void Update()
    {
        float deltaMove = -1f * moveSpeed * Time.deltaTime;
        transform.Translate(transform.up * deltaMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float deltaMoveY = _spriteHeight * 2f;
        transform.localPosition += new Vector3(0f, deltaMoveY);
    }
}
