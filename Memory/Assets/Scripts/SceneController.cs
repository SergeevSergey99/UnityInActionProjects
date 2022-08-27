using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard cardPrefab;
    [SerializeField] private Sprite[] images;

    private MemoryCard first;
    private MemoryCard second;

    private int score = 0;
    [SerializeField]private TextMesh textMesh;

    private bool isAnim = false;
    public bool IsAnim() => isAnim;
    private void Start()
    {
        Vector3 startPos = cardPrefab.transform.position;

        int[] indexes = new int[gridCols * gridRows];
        for (int i = 0; i < images.Length; i++)
        {
            indexes[i * 2] = i;
            indexes[i * 2 + 1] = i;
        }

        indexes = ShuffleArray(indexes);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card = Instantiate(cardPrefab);

                card.GetComponent<SpriteRenderer>().sprite = images[indexes[i + j * gridCols]];
                float posX = offsetX * i + startPos.x;
                float posY = -offsetY * j + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
                card.SetController(this);
                card.setId(indexes[i + j * gridCols]);
            }
        }
        
        SetText();
    }

    private int[] ShuffleArray(int[] indexes)
    {
        int[] newArray = indexes.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(0, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public void CardRevel(MemoryCard card)
    {
        if (first == null)
        {
            first = card;
        }
        else
        {
            second = card;
            if (first.getId() != second.getId())
            {
                isAnim = true;
                StartCoroutine(Wait());
            }
            else
            {
                score++;
                SetText();
                ToNULL();
            }

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        first.UnRevel();
        second.UnRevel();
        isAnim = false;
        ToNULL();
    }

    void ToNULL()
    {
        first = null;
        second = null;
    }

    void SetText()
    {
        textMesh.text = "Score = " + score;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}