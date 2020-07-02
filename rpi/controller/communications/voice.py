## Module Voice

from tuning import Tuning
import usb.core
import usb.util
import time
from pixel_ring import pixel_ring

class Voice:

    def __init__(self):
        self.dev = usb.core.find(idVendor=0x2886, idProduct=0x0018)
        if self.dev:
            print("[MIC]: SUCCESS")
            self.mic_tuning = Tuning(dev)
        else:
            print("[MIC]: FAIL")


    def get_direction(self):
        return self.mic_tuning.direction