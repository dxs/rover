## Module Sensors

class Sensors:

    def __init__(self):
        self.left_speed = 0
        self.right_speed = 0
        self.left_brake = 0
        self.right_brake = 0
        
    def update(self, data):
        print(data)
