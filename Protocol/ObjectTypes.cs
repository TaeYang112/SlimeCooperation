namespace MultiGameModule
{
    public static class ObjectTypes
    {
        public const byte GAME_OBJECT = 0;
        public const byte BUTTON = 1;
        public const byte FLOOR = 2;
        public const byte DOOR = 3;
        public const byte KEY_OBJECT = 4;
        public const byte STONE = 5;
    }

    public static class DoorEvent
    {
        public const byte OPEN = 0;
        public const byte ENTER = 1;
        public const byte LEAVE = 2;
    }
}
