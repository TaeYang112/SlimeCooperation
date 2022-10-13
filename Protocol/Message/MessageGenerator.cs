using System;
using System.Collections.Generic;
using System.Text;

namespace MultiGameModule
{
    // 메시지를 생성해주는 클래스
    // list[0] ~ list[3]까지 메시지의 길이를 포함
    // list[4] 에는 프로토콜이 있음
    public class MessageGenerator
    {
        private byte _protocol;
        public byte Protocol
        {
            get { return _protocol; }
            set
            {
                _protocol = value;
                list[0] = value;
            }
        }
        private List<byte> list;

        public MessageGenerator()
        {
            list = new List<byte>(new byte[5]);
            Protocol = Protocols.S_PING;
        }
        public MessageGenerator(byte protocol) : this()
        {
            this.Protocol = protocol;
        }

        public MessageGenerator AddInt(int value)
        {
            list.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        public MessageGenerator AddByte(byte value)
        {
            list.Add(value);
            return this;
        }

        public MessageGenerator AddString(string value)
        {
            // string타입은 몇바이트인지 붙여서 보냄
            list.AddRange(BitConverter.GetBytes(Encoding.Default.GetByteCount(value)));
            list.AddRange(Encoding.UTF8.GetBytes(value));
            return this;
        }

        public MessageGenerator AddFloat(float value)
        {
            list.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        public MessageGenerator AddBool(bool value)
        {
            list.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        public byte[] Generate()
        {
            // 메시지의 앞에 메시지의 크기를 붙여서 보냄
            byte[] bytes = BitConverter.GetBytes(list.Count);
            list[1] = bytes[0];
            list[2] = bytes[1];
            list[3] = bytes[2];
            list[4] = bytes[3];

            return list.ToArray();
        }

        public void Clear()
        {
            list.Clear();
            list = new List<byte>(new byte[5]);
            list[0] = Protocol;
        }


    }
}
