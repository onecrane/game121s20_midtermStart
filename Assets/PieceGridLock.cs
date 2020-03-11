using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceGridLock : MonoBehaviour
{

    public enum PieceType
    {
        Pawn,
        Bishop,
        Knight,
        Rook,
        Queen,
        King
    }

    public enum PieceColor
    {
        White,
        Black
    }


    private BoardControl boardControl;
    public Vector2Int gridPosition;

    public PieceType pieceType;
    public PieceColor pieceColor;

    // Start is called before the first frame update
    void Start()
    {
        boardControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<BoardControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boardControl == null) Start();

        gridPosition = boardControl.WorldToGrid(transform.position);
        transform.position = boardControl.GridToWorld(gridPosition);

        boardControl.UpdateGridPosition(this, gridPosition);
    }
}
