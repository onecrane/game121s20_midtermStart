using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGridSquare : MonoBehaviour
{

    public Vector2Int gridPosition;

    public Material matNone, matSelected, matValid;

    public enum DisplayState { None, Selected, Valid };

    public DisplayState displayState = DisplayState.None;

    private MeshRenderer meshRenderer;
    private BoardControl boardControl;

    public PieceGridLock pieceOnSquare = null;

    public BoxCollider addedCollider;

    private void Awake()
    {
        boardControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<BoardControl>();
        gridPosition = boardControl.WorldToGrid(transform.position);
        transform.position = boardControl.GridToWorld(gridPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pieceOnSquare == null)
            addedCollider.enabled = false;
        else
            addedCollider.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            // lazy raycast
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
            {
                if (info.transform.GetComponent<InteractiveGridSquare>() == this)
                {
                    OnClick();
                }
            }
        }

        switch (displayState)
        {
            case DisplayState.None:
                meshRenderer.enabled = false;
                break;
            case DisplayState.Selected:
                meshRenderer.enabled = true;
                meshRenderer.sharedMaterial = matSelected;
                break;
            case DisplayState.Valid:
                meshRenderer.enabled = true;
                meshRenderer.sharedMaterial = matValid;
                break;
        }

    }

    void OnClick()
    {
        // mr?
        // Well - what's here? What's the selection state?
        if (pieceOnSquare != null)
        {
            if (pieceOnSquare.pieceColor == boardControl.playerTurn)
            {
                // select for movement
                boardControl.SelectPiece(pieceOnSquare);
                return;
            }
        }

        if (displayState == DisplayState.Valid)
        {
            boardControl.MoveSelectedPiece(gridPosition);
        }
        else
        {
            boardControl.SelectPiece(null);
        }
    }
}
