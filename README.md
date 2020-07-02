# Rover which brings you beer using voice

## Hardware

### Respeasker Mic Array V2.0

To update the firmware:
```
sudo apt-get update
sudo pip install pyusb click
git clone https://github.com/respeaker/usb_4_mic_array.git
cd usb_4_mic_array
sudo python dfu.py --download 6_channels_firmware.bin  # The 6 channels version 
# if you want to use 1 channel,then the command should be like:
 
sudo python dfu.py --download 1_channel_firmware.bin
```