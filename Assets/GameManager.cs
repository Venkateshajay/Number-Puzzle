using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] squares = new GameObject[16];
    private int[] orderOfNumbers = new int[16];
    private int emptySquareIndex=0;
    private Collider2D touchedSquareCollider;
    private int touchedSquareIndex;
    private Sprite temporarySprite;
    //private bool moveAllowed = false;
    Vector2 touchPosition;
    void Start()
    {
        squares = GameObject.FindGameObjectsWithTag("Numbers");
    }
    void Update()
    {
        Move();
    }

    private void AssignValues()
    {

    }
    private bool CheckVictory()
    {
        return true;
    }

    private void Move()
    {
        ReadTouch();
        AssignValues();
        CheckVictory();
    }

    private void swap()
    {
        temporarySprite = touchedSquareCollider.gameObject.GetComponent<SpriteRenderer>().sprite;
        touchedSquareCollider.gameObject.GetComponent<SpriteRenderer>().sprite = squares[emptySquareIndex].GetComponent<SpriteRenderer>().sprite;
        squares[emptySquareIndex].GetComponent<SpriteRenderer>().sprite = temporarySprite;
        Debug.Log("Swap successfull");
    }

    private void ReadTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchedSquareCollider = Physics2D.OverlapPoint(touchPosition);
            if(touchedSquareCollider == null)
            {
                return;
            }
            touchedSquareIndex = FindTheTouchedSquare(touchedSquareCollider);
            Debug.Log(touchedSquareCollider.gameObject.name);
            IsTheSquareNearTheEmpty();
        }
    }

    private void IsTheSquareNearTheEmpty()
    {
        if (squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x - 1f, touchPosition.y)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x + 1f, touchPosition.y)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y-1f)) ||
            squares[emptySquareIndex].GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y +1f)))
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
        for(int i = 0; i < 16; i++)
        {
            if(collider == squares[i].GetComponent<Collider2D>())
            {
                return i;
            }
        }
        return -1;
    }
}
