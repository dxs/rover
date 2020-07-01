## Module controller
from controller.communications.communications import Communications
from controller.sensors.sensors import Sensors
import time

class Controller:

    def __init__(self):
        self.com = Communications()
        self.sensors = Sensors()

    def __enter__(self):
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):
        return

    def run(self):
        self.com.send('5,5,\n')
        print(self.com.read())
        self.get_sensors()

    def get_sensors(self):
        self.com.send('SENSORS\n')
        data = self.com.read()
        self.sensors.update(data)
