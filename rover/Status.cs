﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Devices.Gpio;

namespace rover
{
	class Status
	{
		#region var
		/*Status sendable [27 bytes]*/
		private byte _StartSend;
		private byte _PWM;
		private Int16 _leftSpeed;
		private byte _leftBrake;
		private Int16 _rightSpeed;
		private byte _rightBrake;
		private UInt16 _servo1;
		private UInt16 _servo2;
		private UInt16 _servo3;
		private UInt16 _servo4;
		private UInt16 _servo5;
		private UInt16 _servo6;
		private byte _devibrate;
		private UInt16 _sensitivity;
		private UInt16 _lowBattery;
		private byte _I2CAddress;
		private byte _I2CClock;

		/*Status readable [24 bytes]*/
		private byte _StartRecv;
		private byte _errorFlag;
		private UInt16 _batteryVoltage;
		private Int16 _leftCurrent;
		private UInt16 _leftEncoder;
		private Int16 _rightCurrent;
		private UInt16 _rightEncoder;
		private Int16 _Xaxis;
		private Int16 _Yaxis;
		private Int16 _Zaxis;
		private Int16 _deltaX;
		private Int16 _deltaY;
		private Int16 _deltaZ;

		#endregion

		#region getSet
		/*Status sendable [27 bytes]*/
		public Int16 leftSpeed { get { return _leftSpeed; } set { _leftSpeed = value; } }
		public byte leftBrake { get { return _leftBrake; } set { _leftBrake = value; } }

		public Int16 rightSpeed { get { return _rightSpeed; } set { _rightSpeed = value; } }
		public byte rightBrake { get { return _rightBrake; } set { _rightBrake = value; } }

		public UInt16 servo1 { get { return _servo1; } set { _servo1 = value; } }
		public UInt16 servo2 { get { return _servo2; } set { _servo2 = value; } }
		public UInt16 servo3 { get { return _servo3; } set { _servo3 = value; } }
		public UInt16 servo4 { get { return _servo4; } set { _servo4 = value; } }
		public UInt16 servo5 { get { return _servo5; } set { _servo5 = value; } }
		public UInt16 servo6 { get { return _servo6; } set { _servo6 = value; } }

		public byte devibrate   { get { return _devibrate; } set { _devibrate = value; } }
		public UInt16 sensitivity { get { return _sensitivity; } set { _sensitivity = value; } }
		public UInt16 lowBattery  { get { return _lowBattery; } set { _lowBattery = value; } }

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
		public int deltaZ { get { return _deltaZ; } }

		#endregion

		I2cDevice device;
		public Status()
		{
			SetupVar();
			SetupI2C();
		}

		private async void SetupI2C()
		{
			var settings = new I2cConnectionSettings(0x07);
			settings.BusSpeed = I2cBusSpeed.StandardMode;

			var controller = await Windows.Devices.I2c.I2cController.GetDefaultAsync();
			device = controller.GetDevice(settings);
		}

		public async void update()
		{
			try
			{
				byte[] writeBuf = { 0x01, 0x02, 0x03, 0x04 };
				byte[] readBuf = new byte[24];
				device.Write(writeBuf);
				device.Read(readBuf);
				ReadBuffer(readBuf);
			}
			catch (ArgumentOutOfRangeException e) { e.ToString(); }
		}

		private void ReadBuffer(byte[] buffer)
		{
			_StartRecv = buffer[0];
			_errorFlag = buffer[1];

			_batteryVoltage = 0;
			_batteryVoltage += (UInt16)(buffer[2] << 8);
			_batteryVoltage += (UInt16)buffer[3];

			_leftCurrent = 0;
			_leftCurrent += (Int16)(buffer[4] << 8);
			_leftCurrent += (Int16)buffer[5];

			_leftEncoder = 0;
			_leftEncoder += (UInt16)(buffer[6] << 8);
			_leftEncoder += (UInt16)buffer[7];

			_rightCurrent = 0;
			_rightCurrent += (Int16)(buffer[8] << 8);
			_rightCurrent += (Int16)buffer[9];

			_rightEncoder = 0;
			_rightEncoder += (UInt16)(buffer[10] << 8);
			_rightEncoder += (UInt16)buffer[11];

			_Xaxis = 0;
			_Xaxis += (Int16)(buffer[12] << 8);
			_Xaxis += (Int16)buffer[13];

			_Yaxis = 0;
			_Yaxis += (Int16)(buffer[14] << 8);
			_Yaxis += (Int16)buffer[15];

			_Zaxis = 0;
			_Zaxis += (Int16)(buffer[16] << 8);
			_Zaxis += (Int16)buffer[17];

			_deltaX = 0;
			_deltaX += (Int16)(buffer[18] << 8);
			_deltaX += (Int16)buffer[19];

			_deltaY = 0;
			_deltaY += (Int16)(buffer[20] << 8);
			_deltaY += (Int16)buffer[21];

			_deltaZ = 0;
			_deltaZ += (Int16)(buffer[22] << 8);
			_deltaZ += (Int16)buffer[23];
		}

		private byte[] BuildBuffer()
		{
			byte[] buffer = new byte[27];
			buffer[0] = _StartSend;
			buffer[1] = _PWM;
			/*Motor left*/
			buffer[2] = (byte)(_leftSpeed >> 8);	buffer[3] = (byte)_leftSpeed;
			buffer[4] = _leftBrake;
			/*Motor right*/
			buffer[5] = (byte)(_rightSpeed >> 8);	buffer[6] = (byte)_rightSpeed;
			buffer[7] = _rightBrake;
			/*Servo*/
			buffer[8] =  (byte)(_servo1 >> 8);	buffer[9] = (byte)_servo1;
			buffer[10] = (byte)(_servo2 >> 8);	buffer[11] = (byte)_servo2;
			buffer[12] = (byte)(_servo3 >> 8);	buffer[13] = (byte)_servo3;
			buffer[14] = (byte)(_servo4 >> 8);	buffer[15] = (byte)_servo4;
			buffer[16] = (byte)(_servo5 >> 8);	buffer[17] = (byte)_servo5;
			buffer[18] = (byte)(_servo6 >> 8);	buffer[19] = (byte)_servo6;
			/*devibrate - sensitivity - battery*/
			buffer[20] = _devibrate;
			buffer[21] = (byte)(_sensitivity >> 8);		buffer[22] = (byte)_sensitivity;
			buffer[23] = (byte)(_batteryVoltage >> 8);	buffer[24] = (byte)_batteryVoltage;
			/*I2C*/
			buffer[25] = _I2CAddress;
			buffer[26] = _I2CClock;

			return buffer;
		}

		private void SetupVar()
		{
			/*First set*/
			_StartSend = 0x0F;
			_PWM = 0x07;

			_leftSpeed = 0;		_leftBrake = 0;
			_rightSpeed = 0;	_rightBrake = 0;

			_servo1 = 0;	_servo2 = 0;
			_servo3 = 0;	_servo4 = 0;
			_servo5 = 0;	_servo6 = 0;

			_devibrate = 50;
			_sensitivity = 50;
			_lowBattery = 550;

			_I2CAddress = 0x07;
			_I2CClock = 0; //100kHz

			_StartRecv = 0x0F;

			_leftCurrent = 0;	_leftEncoder = 0;
			_rightCurrent = 0;	_rightEncoder = 0;

			_Xaxis = 0;		_Yaxis = 0;		_Zaxis = 0;
			_deltaX = 0;	_deltaY = 0;	_deltaZ = 0;
		}
	}
}