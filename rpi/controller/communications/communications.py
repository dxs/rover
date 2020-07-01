## Module communication
import serial
import time

class Communications:

    def __init__(self):
        self.ser = serial.Serial(
            port='/dev/ttyACM0',
            baudrate = 9600,
            timeout = 3
        )
        self.ser.flush()

    def send(self, data):
        self.ser.write(data.encode())

    def read(self):
        data = self.ser.readlines()
        return data

