using UnityEngine;
using UnityEngine.EventSystems;

// https://bonnate.tistory.com/304
public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("이동을 할 UI 요소")]
    [SerializeField] private Transform mMoveUiTarget;

    private Vector2 mOriginPos; // 이동을 시작할 당시의 위치
    private Vector2 mOriginMousePos; // 이동을 시작할 당시의 마우스 위치
    private Vector2 mMovedPos; // 이동을 한 거리

    private CursorLockMode mPrevCursorLockMode; // 이동 직전의 커서 모드

    // 마우스 드래그 중단
    private void StopDrag()
    {
        Cursor.lockState = mPrevCursorLockMode;
    }

    // 마우스가 이곳에 클릭되었다면 현재 위치를 저장
    public void OnPointerDown(PointerEventData eventData)
    {
        // 커서 옵션을 Confined 설정하여 화면 밖을 나가지 못하게 함
        mPrevCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;

        // 위치를 저장
        mOriginPos = mMoveUiTarget.position;
        mOriginMousePos = eventData.position;

        // 트랜스폼을 하이어라키 위치상 맨 아래(최상단)으로 위치
        mMoveUiTarget.SetAsLastSibling();
    }

    // 드래그 이동
    public void OnDrag(PointerEventData eventData)
    {
        // 이벤트시스템의 위치와 첫 마우스 위치를 뺀 델타 값을 움직여야하는 위치로
        mMovedPos = eventData.position - mOriginMousePos;

        // 트랜스폼의 위치는 초기 위치 + 델타값
        mMoveUiTarget.position = mOriginPos + mMovedPos;

        // 움직이는 도중에 마우스를 떼거나, 해당 창이 비활성화 되는경우?
        if (Input.GetMouseButtonUp(0) || !gameObject.activeInHierarchy)
            StopDrag();
    }
}