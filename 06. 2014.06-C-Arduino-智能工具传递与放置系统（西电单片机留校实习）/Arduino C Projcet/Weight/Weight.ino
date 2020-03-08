#include <HX711.h>

HX711 hx(6, 7);//定义 sck、dout 接脚

void setup() {
  Serial.begin(9600);
}

void loop()
{
  double sum = 0;
  for (int i = 0; i < 10; i++)
    sum += hx.read();
  Serial.println(sum/10);
}
