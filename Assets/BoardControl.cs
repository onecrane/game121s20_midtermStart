using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardControl : MonoBehaviour
{

    /* Instructions 
     * 1. Introduction
     * 
     * Your task is to help finish a chess game by implementing the functions
     * that determine what moves are valid. See section 4, Utility Functions,
     * for how to do that.
     * 
     * Each type of piece has its own method, and the method for pawn movement
     * is provided for you. The other five (knight, bishop, rook, queen, king)
     * are each worth 20 points.
     * 
     * 
     * 
     * 2. Rules of chess you need to know
     * 
     * Generally, a moving piece is obstructed by pieces of its same color,
     * meaning it cannot move through them.
     * 
     * Generally, a moving piece may capture a piece of its opposite color
     * by moving onto its space, but it may not move beyond that piece,
     * or capture a piece behind it.
     * 
     * No piece may ever move off the board on its player's turn.
     * 
     * See each piece's function for their specific rules. (For those
     * familiar with chess, rules like castling and "en passant" pawn
     * captures are omitted.)
     * 
     * 
     * 
     * 3. Extra credit 
     *
     * For extra credit, you may implement movement restrictions based on a 
     * king piece being in check. See the RefreshCheckedSquares method below.
     * 
     * 
     * 
     * 4. Utility functions you'll probably want (see MarkValidPawnSquares for an example of each)
     * 
     * bool IsValidGridPosition(Vector2Int candidate) - returns true only if the candidate location is on the grid.
     *  Useful for shortcutting out-of-bounds checks.
     *  
     * bool IsGridSpaceEmpty(Vector2Int gridSpace) - returns true only if there is no piece at the given grid location.
     * 
     * PieceGridLock PieceAtLocation(Vector2Int gridSpace) - returns the piece at the given grid location.
     *  Useful in tandem with IsGridSpaceEmpty to find out information about a piece, particularly
     *  a piece's color, for determining if you can capture it, or if it obstructs you.
     *  
     * void MarkGridSpaceValid(Vector2Int gridSpace) - marks a given grid space as valid.
     *  This is the easiest way to mark a grid space as valid. Just call this function on each valid
     *  grid space, and you're good to go.
     *  
     *  
     *  
     *  5. How To Test
     *  
     *  You can test if your functions are working by playing the scene.
     *  At first, you should be able to click on any white pawn 
     *  and see some green tiles indicating where it can move. Clicking on a green
     *  tile will cause the piece to move there. After that, it's black's turn.
     *  
     *  When you can click on a given piece type of either color and see all
     *  the valid places where it can move, the function for that piece type is complete.
     *  
     *  You may ask for confirmation of correctness at any time, if taking the test in person.
     *  
     *  
     *  
     *  6. Permissible resources
     *  
     *  You may use any non-sentient aid at your disposal. The only exception to this
     *  is that if you are taking the test in person, you may ask me to confirm
     *  if your output is correct, as stated under How To Test.
    */



    /// Pawns have the most complex movement rules;
    /// their implementation is provided as an example of how to check the board, and mark valid spaces.
    /// 
    /// Pawns may advance to empty spaces. Normally they advance only one space,
    /// but a pawn in its starting row may advance two spaces (if both spaces are empty).
    /// Pawns may capture one space ahead on a diagonal.
    private void MarkValidPawnSquares(Vector2Int startPosition, PieceGridLock.PieceColor movingPieceColor)
    {

        // Advancing means something different for white pawns and black pawns.
        Vector2Int advancementVector = new Vector2Int(0, 1);
        if (movingPieceColor == PieceGridLock.PieceColor.Black)
        {
            advancementVector = advancementVector * -1;
        }


        // A pawn may advance one space if it is not obstructed by anything.
        bool oneSpaceAheadFree = false;
        Vector2Int oneSpaceAhead = startPosition + advancementVector;
        if (IsValidGridPosition(oneSpaceAhead) && IsGridSpaceEmpty(oneSpaceAhead))
        {
            oneSpaceAheadFree = true;
            MarkGridSpaceValid(oneSpaceAhead);  // This is the "important" line - this is all you need to do to mark a space as valid.
        }


        // A pawn may advance two spaces if it is not obstructed by anything,
        // and the pawn is at its starting row. Note that it's a different row
        // for white and black.
        int startingRow = (movingPieceColor == PieceGridLock.PieceColor.Black) ? 6 : 1;
        Vector2Int twoSpacesAhead = startPosition + advancementVector + advancementVector;
        if (oneSpaceAheadFree && startPosition.y == startingRow && IsValidGridPosition(twoSpacesAhead) && IsGridSpaceEmpty(twoSpacesAhead))
        {
            MarkGridSpaceValid(twoSpacesAhead);
        }


        // A pawn may capture one space forward-diagonal if that space
        // contains a piece of opposite color.
        Vector2Int leftDiagonal = startPosition + advancementVector + Vector2Int.left;
        if (IsValidGridPosition(leftDiagonal) && !IsGridSpaceEmpty(leftDiagonal) && PieceAtLocation(leftDiagonal).pieceColor != movingPieceColor)
        {
            MarkGridSpaceValid(leftDiagonal);
        }

        Vector2Int rightDiagonal = startPosition + advancementVector + Vector2Int.right;
        if (IsValidGridPosition(rightDiagonal) && !IsGridSpaceEmpty(rightDiagonal) && PieceAtLocation(rightDiagonal).pieceColor != movingPieceColor)
        {
            MarkGridSpaceValid(rightDiagonal);
        }

    }





    /// A knight may move to any space in an L-delta:
    /// Either 2 spaces horizontally and one space vertically,
    /// or 2 spaces vertically and one space horizontally.
    /// A knight may not move onto a space occupied by a piece of its own color.
    /// 20 pts
    private void MarkValidKnightSquares(Vector2Int gridPosition, PieceGridLock.PieceColor pieceColor)
    {
        // How many places must you check?
    }





    /// A bishop may move any number of spaces diagonally.
    /// The general rules apply: It is obstructed by pieces of its own color.
    /// It may capture pieces of opposite color, but may not move beyond them.
    /// 20 pts
    private void MarkValidBishopSquares(Vector2Int gridPosition, PieceGridLock.PieceColor pieceColor)
    {
        // You have to check in four directions - how do you make sure you stop at the right place in each direction?
    }





    /// A rook may move any number of spaces horizontally or vertically.
    /// The general rules apply: It is obstructed by pieces of its own color.
    /// It may capture pieces of opposite color, but may not move beyond them.
    /// 20 pts
    private void MarkValidRookSquares(Vector2Int gridPosition, PieceGridLock.PieceColor pieceColor)
    {
        // You have to check in four directions - how do you make sure you stop at the right place in each direction?
    }





    /// A queen may move any number of spaces horizontally, vertically, or diagonally.
    /// The general rules apply: It is obstructed by pieces of its own color.
    /// It may capture pieces of opposite color, but may not move beyond them.
    /// 20 pts
    private void MarkValidQueenSquares(Vector2Int gridPosition, PieceGridLock.PieceColor pieceColor)
    {
        // The queen can move either like a bishop or a rook...
        // how can you use this to reduce the amount of work you need to do?
    }





    /// A king may move one space horizontally, vertically, or diagonally.
    /// The general rules apply: It is obstructed by pieces of its own color.
    /// It may capture pieces of opposite color.
    /// 
    /// In order to earn the extra credit points, you must restrict the king's
    /// movement by preventing it from moving into a checked square.
    /// See RefreshCheckedSquares below.
    /// 20 pts
    private void MarkValidKingSquares(Vector2Int gridPosition, PieceGridLock.PieceColor pieceColor)
    {
        // A king can move one space in any direction.
        // Is there any way to make it less tedious than checking all eight directions explicitly?
    }





    // Extra credit  (+10 points)
    // This method is called each time a piece finishes moving.
    // In order for it to work, 
    // the two List<Vector2Int> member variables must be filled out correctly.
    // 
    // A square is checked by black if:
    //      there is at least one black piece that can capture a piece on it.
    // 
    // Similarly, a square is checked by white if:
    //      there is at least one white piece that can capture a piece on it.
    //
    // To get the extra credit, this method must be implemented correctly,
    // and an additional movement restriction must be implemented for the king:
    // A king may not move into a square in which it is checked.
    // The black king may not move into a square checked by white,
    // and the white king may not move into a square checked by black.
    //
    // Pawns and kings make this method harder than it sounds.
    // A king cannot move into a threatened square,
    // but still checks that square. 
    // And pawns capture on the diagonal, but move only on the vertical.
    private void RefreshCheckedSquares()
    {
        squaresCheckedByBlack.Clear();
        squaresCheckedByWhite.Clear();
    }




    /**************************************************************************** Hadrian's Wall ****************************************************************************/
    // Do not modify anything below this line. You may look at it, but do not change it or you 
    // will risk destabilizing the game, for which you will be severely penalized.

    #region Look, but don't touch

    public float xGrid = 1.75625f;
    public float yGrid = 1.76125f;

    public Transform pieceRoot;

    private PieceGridLock[,] boardPieces = new PieceGridLock[8, 8];

    public PieceGridLock.PieceColor playerTurn { get; private set; }

    private PieceGridLock selectedPiece;

    private InteractiveGridSquare[,] gridSquares = new InteractiveGridSquare[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        playerTurn = PieceGridLock.PieceColor.White;

        GameObject[] gridSquareObjects = GameObject.FindGameObjectsWithTag("GridSquare");
        for (int i = 0; i < gridSquareObjects.Length; i++)
        {
            InteractiveGridSquare igs = gridSquareObjects[i].GetComponent<InteractiveGridSquare>();

            gridSquares[igs.gridPosition.x, igs.gridPosition.y] = igs;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) gridSquares[x, y].pieceOnSquare = boardPieces[x, y];
    }

    public Vector3 GridToWorld(Vector2Int grid)
    {
        return pieceRoot.position + new Vector3(grid.x * xGrid, 0, grid.y * yGrid);
    }

    public Vector2Int WorldToGrid(Vector3 world)
    {
        return new Vector2Int(Mathf.RoundToInt((world.x - pieceRoot.position.x) / xGrid), Mathf.RoundToInt((world.z - pieceRoot.position.z) / yGrid));
    }

    public void UpdateGridPosition(PieceGridLock piece, Vector2Int gridLocation)
    {
        for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) if (boardPieces[x, y] == piece) boardPieces[x, y] = null;
        boardPieces[gridLocation.x, gridLocation.y] = piece;
    }

    public void SelectPiece(PieceGridLock piece)
    {
        for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) gridSquares[x, y].displayState = InteractiveGridSquare.DisplayState.None;

        // Find the moving king
        Vector2Int kingLoc = new Vector2Int(-1, -1);
        List<Vector2Int> checkedSquares = (playerTurn == PieceGridLock.PieceColor.White) ? squaresCheckedByBlack : squaresCheckedByWhite;
        for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
            {
                Vector2Int l = new Vector2Int(x, y);
                if (!IsGridSpaceEmpty(l) && PieceAtLocation(l).pieceColor == playerTurn && PieceAtLocation(l).pieceType == PieceGridLock.PieceType.King)
                {
                    kingLoc = l;
                }
            }
        // Is the king in check?
        if (checkedSquares.Contains(kingLoc))
        {
            // Then we may only move the king. If we've selected anything else, return.
            if (piece != null && piece.pieceType != PieceGridLock.PieceType.King) return;
        }

        selectedPiece = piece;
        if (selectedPiece != null)
        {
            gridSquares[selectedPiece.gridPosition.x, selectedPiece.gridPosition.y].displayState = InteractiveGridSquare.DisplayState.Selected;

            switch (selectedPiece.pieceType)
            {
                case PieceGridLock.PieceType.Pawn:
                    MarkValidPawnSquares(piece.gridPosition, piece.pieceColor);
                    break;
                case PieceGridLock.PieceType.Knight:
                    MarkValidKnightSquares(piece.gridPosition, piece.pieceColor);
                    break;
                case PieceGridLock.PieceType.Bishop:
                    MarkValidBishopSquares(piece.gridPosition, piece.pieceColor);
                    break;
                case PieceGridLock.PieceType.Rook:
                    MarkValidRookSquares(piece.gridPosition, piece.pieceColor);
                    break;
                case PieceGridLock.PieceType.Queen:
                    MarkValidQueenSquares(piece.gridPosition, piece.pieceColor);
                    break;
                case PieceGridLock.PieceType.King:
                    MarkValidKingSquares(piece.gridPosition, piece.pieceColor);
                    break;
            }

        }

    }

    public void MoveSelectedPiece(Vector2Int gridLocation)
    {
        if (selectedPiece == null) return;

        if (boardPieces[gridLocation.x, gridLocation.y] != null)
        {
            // Disable it
            boardPieces[gridLocation.x, gridLocation.y].gameObject.SetActive(false);
            Destroy(boardPieces[gridLocation.x, gridLocation.y].gameObject);
        }

        boardPieces[selectedPiece.gridPosition.x, selectedPiece.gridPosition.y] = null;
        boardPieces[gridLocation.x, gridLocation.y] = selectedPiece;
        selectedPiece.transform.position = GridToWorld(gridLocation);

        for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) gridSquares[x, y].displayState = InteractiveGridSquare.DisplayState.None;

        if (playerTurn == PieceGridLock.PieceColor.Black)
            playerTurn = PieceGridLock.PieceColor.White;
        else
            playerTurn = PieceGridLock.PieceColor.Black;


        RefreshCheckedSquares();

    }

    private bool IsValidGridPosition(Vector2Int candidate)
    {
        return candidate.x >= 0 && candidate.x <= 7 && candidate.y >= 0 && candidate.y <= 7;
    }

    private bool IsGridSpaceEmpty(Vector2Int gridSpace)
    {
        return gridSquares[gridSpace.x, gridSpace.y].pieceOnSquare == null;
    }

    private PieceGridLock PieceAtLocation(Vector2Int gridSpace)
    {
        return gridSquares[gridSpace.x, gridSpace.y].pieceOnSquare;
    }

    private void MarkGridSpaceValid(Vector2Int gridSpace)
    {
        gridSquares[gridSpace.x, gridSpace.y].displayState = InteractiveGridSquare.DisplayState.Valid;
    }

    private List<Vector2Int> squaresCheckedByWhite = new List<Vector2Int>();
    private List<Vector2Int> squaresCheckedByBlack = new List<Vector2Int>();

    #endregion


}
