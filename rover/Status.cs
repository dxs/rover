using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rover
{
	class Status
	{
		#region var
		/*Status sendable [27 bytes]*/
		private byte _StartSend;
		private byte _PWM;
		private int _leftSpeed;
		private byte _leftBrake;
		private int _rightSpeed;
		private byte _rightBrake;
		private int _servo1;
		private int _servo2;
		private int _servo3;
		private int _servo4;
		private int _servo5;
		private int _servo6;
		private byte _devibrate;
		private int _sensitivity;
		private int _lowBattery;
		private byte _I2CAddress;
		private byte _I2CClock;

		/*Status readable [24 bytes]*/
		private byte _StartRecv;
		private byte _errorFlag;
		private int _batteryVoltage;
		private int _leftCurrent;
		private int _leftEncoder;
		private int _rightCurrent;
		private int _rightEncoder;
		private int _Xaxis;
		private int _Yaxis;
		private int _Zaxis;
		private int _deltaX;
		private int _deltaY;
		private int _deltaZ;

		#endregion

		#region getSet
		/*Status sendable [27 bytes]*/
		public int leftSpeed { get { return _leftSpeed; } set { _leftSpeed = value; } }
		public byte leftBrake { get { return _leftBrake; } set { _leftBrake = value; } }

		public int rightSpeed { get { return _rightSpeed; } set { _rightSpeed = value; } }
		public byte rightBrake { get { return _rightBrake; } set { _rightBrake = value; } }

		public int servo1 { get { return _servo1; } set { _servo1 = value; } }
		public int servo2 { get { return _servo2; } set { _servo2 = value; } }
		public int servo3 { get { return _servo3; } set { _servo3 = value; } }
		public int servo4 { get { return _servo4; } set { _servo4 = value; } }
		public int servo5 { get { return _servo5; } set { _servo5 = value; } }
		public int servo6 { get { return _servo6; } set { _servo6 = value; } }

		public byte devibrate   { get { return _devibrate; } set { _devibrate = value; } }
		public int sensitivity { get { return _sensitivity; } set { _sensitivity = value; } }
		public int lowBattery  { get { return _lowBattery; } set { _lowBattery = value; } }

		public byte I2CAddress  { get { return _I2CAddress; } set { _I2CAddress = value; } }
		public byte I2cClock    { get { return _I2CClock; } set { _I2CClock = value; } }

		/*Status readable [24 bytes]*/
		public int batteryVoltage { get { return _batteryVoltage; } }
		public int leftCurrent { get { return _leftCurrent; } }
		public int leftEncoder { get { return _leftEncoder; } }
		public int rightCurrent { get { return _rightCurrent; } }
		public int rightEncoder { get { return _rightEncoder; } }
		public int Xaxis { get { return _Xaxis; } }
		public int Yaxis { get { return _Yaxis; } }
		public int Zaxis { get { return _Zaxis; } }
		public int deltaX { get { return _deltaX; } }
		public int deltaY { get { return _deltaY; } }
		public int deltaY { get { return _deltaZ; } }

		#endregion

		public Status()
		{

		}
	}
}
