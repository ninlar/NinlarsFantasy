using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class CharacterBody3d : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public Camera3D _camera;
	public Node3D _neck;

	public void OnRead()
	{
		_camera = GetNode<Camera3D>("Neck/Camera3D");
		_neck = GetNode<Node3D>("Neck");
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

		if (@event is InputEventMouseButton)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		else if (@event.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		if (Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			if (@event is InputEventMouseMotion)
			{
				InputEventMouseMotion mouseEvent = @event as InputEventMouseMotion;
				_neck.RotateY(-mouseEvent.Relative.X * 0.01f);
				_camera.RotateX(-mouseEvent.Relative.Y * 0.01f);
				Vector3 clamped = new Vector3(
					Mathf.Clamp(_camera.Rotation.X, Godot.Mathf.DegToRad(-30), Mathf.DegToRad(60)),
					_camera.Rotation.Y,
					_camera.Rotation.Z
				);
				_camera.Rotation = clamped;
			}
		}
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "back");
		Vector3 direction = (_camera.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
