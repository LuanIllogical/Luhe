using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class MouseStateWrapper
{
    MouseState mouseState;
    Vector2 scaledMousePosition;
    Rectangle renderTargetDestination;
    float screenScale;

    public bool Scaled { get; set; } = true;

    public Vector2 Position => Scaled ? scaledMousePosition : mouseState.Position.ToVector2();

    public MouseState MouseState => mouseState;

    public MouseStateWrapper(bool scaled) => Scaled = scaled;

    internal void SetMouseState(MouseState mouseState)
    {
        this.mouseState = mouseState;
        scaledMousePosition = mouseState.Position.ToVector2();
        scaledMousePosition.X -= renderTargetDestination.X;
        scaledMousePosition.Y -= renderTargetDestination.Y;
        scaledMousePosition /= screenScale;
    }

    internal void SetRenderTargetDestination(Rectangle renderTargetDestination) => this.renderTargetDestination = renderTargetDestination;

    internal void SetScreenScale(float scale) => screenScale = scale;

    internal void SetMouseLocation(Point location) => SetMouseState(new MouseState(location.X, location.Y, mouseState.ScrollWheelValue,
        mouseState.LeftButton, mouseState.MiddleButton, mouseState.RightButton, mouseState.XButton1, mouseState.XButton2));

    internal void SetMouseLocation(int x, int y) => SetMouseState(new MouseState(x, y, mouseState.ScrollWheelValue,
        mouseState.LeftButton, mouseState.MiddleButton, mouseState.RightButton, mouseState.XButton1, mouseState.XButton2));

    public Point ScalePositionUp(Vector2 position)
    {
        Vector2 unscaledMousePosition = position;
        unscaledMousePosition *= screenScale;
        unscaledMousePosition.X += renderTargetDestination.X;
        unscaledMousePosition.Y += renderTargetDestination.Y;
        return unscaledMousePosition.ToPoint();
    }
}