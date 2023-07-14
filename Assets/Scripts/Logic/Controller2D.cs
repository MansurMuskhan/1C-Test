using UnityEngine;
public class Controller2D : MonoBehaviour
{
	public LayerMask collisionMask;
	//[SerializeField]
	float skinWidth = 0.5f;
	float skinBold => skinWidth - .015f;
	//[SerializeField]
	int
		horizontalRayCount = 3,
		verticalRayCount = 3;
	[SerializeField]
	float
		rayBoxWidth = 0.5f,
		rayBoxHeight = 0.5f;
	protected float horizontalRaySpacing => (rayBoxHeight * 2) / (horizontalRayCount - 1);
	protected float verticalRaySpacing => (rayBoxWidth * 2) / (verticalRayCount - 1);
	public CollisionInfo collisions;
	public void Move(Vector2 velocity)
	{
		collisions.Reset();

		if (velocity.x == 0 && velocity.y == 0)
		{
			return;
		}
		HCollisions(ref velocity);
		VCollisions(ref velocity);
		transform.Translate(velocity);
	}
	void HCollisions(ref Vector2 velocity)
	{
		float _direction = Mathf.Sign(velocity.x);
		float _rayLength = Mathf.Abs(velocity.x) + skinWidth;
		Vector2 _rayOrigin = transform.position;
		_rayOrigin.x += (rayBoxWidth - skinBold) * _direction;
		_rayOrigin.y -= horizontalRaySpacing * (horizontalRayCount + 1) / 2;

		for (int i = 0; i < horizontalRayCount; i++)
		{
			_rayOrigin.y += horizontalRaySpacing;
			RaycastHit2D hit = Physics2D.Raycast(_rayOrigin, Vector2.right * _direction, _rayLength, collisionMask);
			Debug.DrawRay(_rayOrigin, Vector2.right * _direction * _rayLength, Color.yellow);

			if (hit)
			{
				velocity.x = (hit.distance - skinWidth) * _direction;
				_rayLength = hit.distance;
				collisions.left = _direction == -1;
				collisions.right = _direction == 1;
			}
		}
	}
	void VCollisions(ref Vector2 velocity)
	{
		float _direction = Mathf.Sign(velocity.y);
		float _rayLength = Mathf.Abs(velocity.y) + skinWidth;
		Vector2 _rayOrigin = transform.position;
		_rayOrigin.x -= verticalRaySpacing * (verticalRayCount + 1) / 2;
		_rayOrigin.y += (rayBoxHeight - skinBold) * _direction;

		for (int i = 0; i < verticalRayCount; i++)
		{
			_rayOrigin.x += verticalRaySpacing;
			RaycastHit2D hit = Physics2D.Raycast(_rayOrigin, Vector2.up * _direction, _rayLength, collisionMask);
			Debug.DrawRay(_rayOrigin, Vector2.up * _direction * _rayLength, Color.yellow);

			if (hit)
			{
				velocity.y = (hit.distance - skinWidth) * _direction;
				_rayLength = hit.distance;
				collisions.below = _direction == -1;
				collisions.above = _direction == 1;
			}
		}
	}
	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;
		public void Reset()
		{
			above = below = false;
			left = right = false;
		}
	}
	private void OnDrawGizmos()
	{
		Vector3 _scale = Vector3.zero;
		_scale.x = rayBoxWidth;
		_scale.y = rayBoxHeight;
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(transform.position, _scale * 2);
	}
}
