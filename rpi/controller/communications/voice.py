## Module Voice

from tuning import Tuning
import usb.core
import usb.util
import time
from pixel_ring import pixel_ring
from precise_runner import PreciseEngine, PreciseRunner



class Voice:

    def __init__(self):
        self.dev = usb.core.find(idVendor=0x2886, idProduct=0x0018)
        self.engine = PreciseEngine('precise-engine/precise-engine', 'model.pb')
        self.runner = PreciseRunner(self.engine, on_activation=self.detection)

        if self.dev:
            print("[MIC]: SUCCESS")
            self.mic_tuning = Tuning(dev)
        else:
            print("[MIC]: FAIL")
        self.runner.start()

    def detection(self):
        print('hi')

    def get_direction(self):
        return self.mic_tuning.direction
