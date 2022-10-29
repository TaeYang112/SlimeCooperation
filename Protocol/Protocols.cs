namespace MultiGameModule
{


    public static class Protocols
    {
        // 클라이언트 -> 서버 

        // REQ(Request)는 클라이언트가 서버로 요청을 보낸 후 RES(Response)를 기다림
        public const byte REQ_ROOM_LIST = 1;
        public const byte REQ_CREATE_ROOM = 2;
        public const byte REQ_ENTER_ROOM = 3;

        // 서버로 자신의 변경사항을 알려줌
        public const byte C_READY = 30;
        public const byte C_EXIT_ROOM = 31;
        public const byte C_LOOK_DIRECTION = 32;
        public const byte C_OBJECT_EVENT = 33;			// 오브젝트 키(int), 타입 (byte)
        public const byte C_LOCATION = 34;			    // x(int), y(int), MoveNum(int)
        public const byte C_KEY_INPUT = 35;


        // 서버 -> 클라이언트 

        // 클라이언트의 REQ(Request)에 응답(Response)
        public const byte RES_ADD_ROOM_LIST = 101;
        public const byte RES_DEL_ROOM_LIST = 102;
        public const byte RES_UPDATE_ROOM_LIST = 103;
        public const byte RES_ENTER_ROOM = 104;			// 방 key(int), 방 제목(str), 클라 스킨 번호(int)

        // 클라이언트에게 정보를 알려주거나 명령
        public const byte S_OBJECT_EVENT = 132;         // 오브젝트 키(int), 타입 (byte), 건드린 클라이언트키(int)
                                                        // Object			x(int), y(int), width(int), height(int), skin(int),isVisible(bool), Collision(bool), BlockAble(bool)
                                                        //STONE                        + x(int), y(int), 무게(int)
                                                        //DOOR						 + DoorEvent(byte)

                                                        //STONEDOOR                    StoneDoorMode.MOVE :  StoneDoorMode(byte), x(int), y(int) 
                                                        //                              StoneDoorMode.BEGONE : StoneDoorMode(byte)
                                                        //Portal						 + x(int), y(int)

        public const byte S_MAP_START = 134;			// 시작 x(int), 시작 y(int)
        public const byte S_MOVE = 135;				    // 움직일 x(int), 움직일 y(int), MoveNum(int)
        public const byte S_NEW_OBJECT = 136;			// 오브젝트 키(int), 오브젝트 타입(byte), X(int), Y(int), width(int), height(int), 스킨 번호(int)
        public const byte S_ERROR = 137;				// ErrorCode(int)
        public const byte S_PING = 138;				    //
        public const byte S_ALLDIE = 139;

        // 다른 클라이언트의 변경사항을 알려줌
        public const byte S_EXIT_ROOM_OTHER = 160;
        public const byte S_ENTER_ROOM_OTHER = 161;		// 클라key(int), 레디 여부(bool), 클라 스킨 번호(int)
        public const byte S_LOCATION_OTHER = 162;		// 클라key(int), 좌표 x(int), 좌표 y(int)
        public const byte S_LOOK_DIRECTION_OTHER = 163;
        public const byte S_READY_OTHER = 164;
        public const byte S_RESTART_OTHER = 165;			// 클라key(int), 누름여부 (bool)
    }

}
