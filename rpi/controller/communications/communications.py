## Module communication
import serial
import time

class Communications:

    def __init__(self):
        self.ser = serial.Serial(
            port='/dev/ttyS0',
            baudrate = 9600,
            parity = serial.PARTY_NONE,
            stopbits = serial.STOPBITS_ONE,
            bytesize = serial.EIGHTBITS,
            timeout = 1
        )        

    def send(self, data):
        ser.write(data)

    def read(self, command='SENSORS'):
        self.send(command)
        data = self.realines()
        return data

