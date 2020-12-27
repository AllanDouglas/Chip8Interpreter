namespace Chip8Console.Keyboard
{
    public interface IKeyboard
    {
        byte[] Keys { get; }
        byte LastPressedKey { get; }
        bool HasKeyPressed { get; }
        void Update();
    }
}