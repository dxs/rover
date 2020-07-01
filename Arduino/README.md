# Arduino instructions

## Wiring
Use SDA SCL from arduino (UNO in our case)

## Serial structure

To send command, we have multiple options. 

### Get sensors

send serial `SENSORS` to get the status of the sensors:

```
Slave Error Message:0
Battery Voltage:	83.
4V
Left  Motor Current:	0mA
Left  Motor Encoder:	-1
Right Motor Current:	0mA
Right Motor Encoder:	-1
X-axis:		710
Y-axis:		710
Z-axis:		710
X-delta:		0
Y-delta:		0
Z-delta:		0
```

### Send command

to set the speed of the motors, need to send `LEFTSPEED,RIGHTSPEED;`

## Failsafe

If a command is not set every 100 cycles, the motors stops by itself