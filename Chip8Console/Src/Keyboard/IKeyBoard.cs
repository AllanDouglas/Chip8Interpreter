namespace Chip8Console.Keyboard
{
    public interface IKeyboard
    {
        byte[] KeyBinds { get; }
        byte LastPressedKey { get; }
        bool HasKeyPressed { get; }
        void Update();
    }
}