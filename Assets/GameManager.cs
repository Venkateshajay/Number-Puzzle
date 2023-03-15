using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] squares = new GameObject[16];
    private int[] orderOfNumbers = new int[16];
    private int emptySquareIndex = 0;
    private Collider2D touchedSquareCollider;
    private int touchedSquareIndex;
    private Sprite temporarySprite;
    private int[] shuffledArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    //private bool moveAllowed = false;
    Vector2 touchPosition;
    [SerializeField] private Sprite[] noSprites = new Sprite[16];
    void Start()
    {
        Shuffle(shuffledArray);
        squares = GameObject.FindGameObjectsWithTag("Numbers");
        SetId();
        AssignValues();
        SpriteAssign();
    }
    void Update()
    {
        Move();
    }

    private void AssignValues()
    {
        for (int i = 0; i < 16; i++)
        {
            //squares[i].GetComponent<Id>().SetCurrentN0(shuffledArray[i]);
            orderOfNumbers[i] = squares[i].GetComponent<Id>().GetCurrentNo();
        }
    }

    private void SpriteAssign()
    {
        for (int i = 0; i < 16; i++)
        {
            squares[i].GetComponent<SpriteRenderer>().sprite = noSprites[orderOfNumbers[i]];
        }
    }
    private bool CheckVictory()
    {
        bool result = true;
        for(int i = 0; i < 16; i++)
        {
            if (!squares[i].GetComponent<Id>().Check())
            {
                result = false;
            }
        }
        return result;
    }

    private void Move()
    {
        ReadTouch();
        //AssignValues();
        CheckVictory();
    }

    private void swap()
    {
        temporarySprite = touchedSquareCollider.gameObject.GetComponent<SpriteRenderer>().sprite;
        touchedSquareCollider.gameObject.GetComponent<SpriteRenderer>().sprite = squares[emptySquareIndex].GetComponent<SpriteRenderer>().sprite;
        squares[emptySquareIndex].GetComponent<SpriteRenderer>().sprite = temporarySprite;
        int temp = touchedSquareCollider.gameObject.GetComponent<Id>().GetCurrentNo();
        touchedSquareCollider.gameObject.GetComponent<Id>().SetCurrentN0(0);
        squares[emptySquareIndex].GetComponent<Id>().SetCurrentN0(temp);
        AssignValues();
        emptySquareIndex = touchedSquareIndex;
        //Debug.Log("Swap successfull");
    }

    private void ReadTouch()
    {
        if (Input.touchCount > 0)
        {
            //Debug.Log("he");
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchedSquareCollider = Physics2D.OverlapPoint(touchPosition);
            if (touchedSquareCollider == null)
            {
                return;
            }
            touchedSquareIndex = FindTheTouchedSquare(touchedSquareCollider);
            //Debug.Log(touchedSquareCollider.gameObject.name);
            IsTheSquareNearTheEmpty();
            AssignValues();
        }
    }

    private void IsTheSquareNearTheEmpty()
    {
        if (squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x - 1f, touchPosition.y)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x + 1f, touchPosition.y)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y - 1f)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y + 1f)))
        {
            swap();
        }
        else
        {
            //moveAllowed = false;
        }
    }

    private int FindTheTouchedSquare(Collider2D collider)
    {
        for (int i = 0; i < 16; i++)
        {
            if (collider == squares[i].GetComponent<Collider2D>())
            {
                return i;
            }
        }
        return -1;
    }

    private void Shuffle(int[] a)
    {
        for (int i = a.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            int temp = a[i];
            a[i] = a[rnd];
            a[rnd] = temp;
        }
    }

    private void SetId()
    {
        for(int i = 0; i < 16; i++)
        {
            if (shuffledArray[i] == 0)
            {
                emptySquareIndex = i;
            }
            squares[i].GetComponent<Id>().SetCurrentN0(shuffledArray[i]);
        }
    }
}
