﻿namespace MultiGameModule
{
    public static class ObjectTypes
    {
        public const byte GAME_OBJECT = 0;
        public const byte BUTTON = 1;
        public const byte FLOOR = 2;
        public const byte DOOR = 3;
        public const byte KEY_OBJECT = 4;
        public const byte STONE = 5;
        public const byte STONE_DOOR = 6;
        public const byte PORTAL = 7;
        public const byte LAVA = 8;
        public const byte COLOR_STONE = 9;
        public const byte PLATFORM = 10;
        public const byte PRESSING_BUTTON = 11;
        public const byte TIMER_BOX = 12;
        public const byte TIMER_BOARD = 13;
        public const byte THORN_BUSH = 14;
        public const byte PROP = 15;
    }

    public static class DoorEvent
    {
        public const byte OPEN = 0;
        public const byte ENTER = 1;
        public const byte LEAVE = 2;
    }

    public static class StoneDoorMode
    {
        public const byte MOVE = 0;
        public const byte BEGONE = 1;
    }
}
