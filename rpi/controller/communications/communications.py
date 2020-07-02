## Module communication
import serial
import time

class Communications:

    def __init__(self):
        self.ser = serial.Serial(
            port='/dev/ttyACM0',
            baudrate = 115200,
            timeout = 0.1
        )
        self.ser.flush()

    def send(self, data):
        self.ser.write(data.encode())

    def read(self):
        data = self.ser.readlines()
        return data

