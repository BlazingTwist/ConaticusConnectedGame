using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GameObjects {

	public class MachineConnectionHandler : MonoBehaviour {

		public Canvas Canvas;

		public GameObject ConnectionPrefab;

		public GameObject ConnectionContainer;

		private bool _isDragging = false;
		private MachineConnectionPoint _dragTarget;
		private MachineConnectionPoint _clickTarget;
		private GameObject _activeConnection;
		private RectTransform _activeConnectionTransform;

		public void DragBegin(BaseEventData data) {
			PointerEventData pointerData = (PointerEventData) data;
			if (pointerData.button != PointerEventData.InputButton.Left) {
				return;
			}

			_isDragging = true;
			_dragTarget = FindConnectionPoint(pointerData.position, 40);
			if (_dragTarget) {
				_activeConnection = Instantiate(ConnectionPrefab, ConnectionContainer.transform);
				_activeConnectionTransform = (RectTransform) _activeConnection.transform;
			}
		}

		public void DragHandler(BaseEventData data) {
			if (_dragTarget) {
				PointerEventData pointerData = (PointerEventData) data;
				UpdateConnectionPosition(_dragTarget, pointerData.position);
			}
		}

		public void DragEnd(BaseEventData data) {
			PointerEventData pointerData = (PointerEventData) data;
			MachineConnectionPoint targetPoint = FindConnectionPoint(pointerData.position, 40);
			SetupConnection(_dragTarget, targetPoint);
			_dragTarget = null;
		}

		public void ClickStart(BaseEventData data) {
			if (_isDragging) {
				_isDragging = false;
				return;
			}

			PointerEventData pointerData = (PointerEventData) data;
			if (pointerData.button != PointerEventData.InputButton.Left) {
				if (_clickTarget) {
					Destroy(_activeConnection);
					_clickTarget = null;
				} else if (pointerData.button == PointerEventData.InputButton.Right) {
					MachineConnectionPoint targetPoint = FindConnectionPoint(pointerData.position, 40);
					if (targetPoint) {
						targetPoint.ReleaseAllConnections();
					}
				}
				return;
			}

			if (_clickTarget) {
				MachineConnectionPoint targetPoint = FindConnectionPoint(pointerData.position, 40);
				SetupConnection(_clickTarget, targetPoint);
				_clickTarget = null;
			} else {
				_clickTarget = FindConnectionPoint(pointerData.position, 40);
				if (_clickTarget) {
					_activeConnection = Instantiate(ConnectionPrefab, ConnectionContainer.transform);
					_activeConnectionTransform = (RectTransform) _activeConnection.transform;
				}
			}
		}

		private void Update() {
			if (_clickTarget) {
				UpdateConnectionPosition(_clickTarget, Input.mousePosition);
			}
		}

		private void UpdateConnectionPosition(MachineConnectionPoint target, Vector2 pointerPosition) {
			Vector3 dragStartPosition = target.transform.position;
			Vector2 dragTotalDelta = pointerPosition - (Vector2) dragStartPosition;
			Vector3 connectionCenter = dragStartPosition + ((Vector3) dragTotalDelta * 0.5f);

			_activeConnectionTransform.position = connectionCenter;
			_activeConnectionTransform.sizeDelta = new Vector2(dragTotalDelta.magnitude, 8) / Canvas.scaleFactor;
			_activeConnectionTransform.right = target.ConnectionType == MachineConnectionPoint.Type.Output
					? dragTotalDelta.normalized
					: -dragTotalDelta.normalized;
		}

		private void SetupConnection(MachineConnectionPoint pointA, MachineConnectionPoint pointB) {
			if (!pointB) {
				Destroy(_activeConnection);
				return;
			}
			if (pointA.ConnectionType == pointB.ConnectionType) {
				AlertController.Instance.AlertSameConnectionType();
				Destroy(_activeConnection);
			} else {
				bool aIsInput = pointA.ConnectionType == MachineConnectionPoint.Type.Input;
				MachineConnectionPoint inputPoint = aIsInput ? pointA : pointB;
				MachineConnectionPoint outputPoint = aIsInput ? pointB : pointA;
				if (outputPoint.HasMachineAsInput(inputPoint.OwningMachine)) {
					AlertController.Instance.AlertNoRecursion();
					Destroy(_activeConnection);
				} else {
					UpdateConnectionPosition(pointA, pointB.transform.position);
					inputPoint.Connect(outputPoint, _activeConnection);
				}
			}
			_activeConnection = null;
		}

		private Vector2 PointToCanvasPosition(Vector2 point) {
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
					(RectTransform) Canvas.transform,
					point,
					Canvas.worldCamera,
					out Vector2 result
			);
			return result;
		}

		private MachineConnectionPoint FindConnectionPoint(Vector2 pointerPosition, float maxDistance) {
			maxDistance *= maxDistance;

			Vector2 canvasPosition = PointToCanvasPosition(pointerPosition);

			MachineConnectionPoint closestPoint = null;
			float closestDistance = float.MaxValue;
			foreach (MachineConnectionPoint connectionPoint in FindObjectsOfType<MachineConnectionPoint>()) {
				Vector2 connectionPosition = PointToCanvasPosition(connectionPoint.transform.position);
				float distance = Vector2.SqrMagnitude(canvasPosition - connectionPosition);
				if (distance < closestDistance) {
					closestDistance = distance;
					closestPoint = connectionPoint;
				}
			}

			return closestDistance <= maxDistance ? closestPoint : null;
		}

	}

}
