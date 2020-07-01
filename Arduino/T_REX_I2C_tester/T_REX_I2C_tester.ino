#include <Wire.h>

#define startbyte 0x0F
#define I2Caddress 0x07
#define FAILSAFE_TIMEOUT 100 //500ms
int sv[6]={1500,1500,1500,1500,0,0};                 // servo positions: 0 = Not Used
int sd[6]={5,10,-5,-15,20,-20};                      // servo sweep speed/direction
int lmspeed = 0;
int rmspeed = 0;                                 // left and right motor speed from -255 to +255 (negative value = reverse)
int ldir=5;                                          // how much to change left  motor speed each loop (use for motor testing)
int rdir=5;                                          // how much to change right motor speed each loop (use for motor testing)
byte lmbrake=0;
byte rmbrake=0;                                // left and right motor brake (non zero value = brake)
byte devibrate=50;                                   // time delay after impact to prevent false re-triggering due to chassis vibration
int sensitivity=50;                                  // threshold of acceleration / deceleration required to register as an impact
int lowbat=550;                                      // adjust to suit your battery: 700 = 7.00V
byte i2caddr=7;                                      // default I2C address of T'REX is 7. If this is changed, the T'REX will automatically store new address in EEPROM
byte i2cfreq=0;                                      // I2C clock frequency. Default is 0=100kHz. Set to 1 for 400kHz
unsigned int failsafe = 0;    //used to cancel motion in case of serial break out

void setup()
{
  Serial.begin(9600);
  Wire.begin();                                      // no address - join the bus as master
}


void convert_order(String order)//format LEFT[0-255],RIGHT[0-255];
{
  if (order == "SENSORS")
  {
    MasterReceive();                                   // receive data packet from T'REX controller
    return;
  }
  
  char str_array[order.length()];
  order.toCharArray(str_array, order.length());
  char *token;
  const char delim[2] = ",";
  token = strtok(str_array, delim);
  lmspeed = token;
  token = strtok(NULL, delim);
  rmspeed = token;
}

void loop()
{
                                                     // send data packet to T'REX controller 
  MasterSend(startbyte,2,lmspeed,lmbrake,rmspeed,rmbrake,sv[0],sv[1],sv[2],sv[3],sv[4],sv[5],devibrate,sensitivity,lowbat,i2caddr,i2cfreq);
  delay(50);
  MasterReceive();      
  delay(50);
  failsafe++;
  
  if(failsafe >= FAILSAFE_TIMEOUT && false)
  {
    lmspeed = 0;
    rmspeed = 0;
  }
  while (Serial.available() > 0) {
    failsafe = 0;
    // read the incoming byte:
    String incoming;
    incoming = Serial.readString();
    convert_order(incoming);
  }
}
