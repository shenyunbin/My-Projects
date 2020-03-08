#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);	// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7

void draw(void) {
  
  if ( u8g.getMode() == U8G_MODE_HICOLOR || u8g.getMode() == U8G_MODE_R3G3B2) {
    /* draw background (area is 128x128) */
    u8g_uint_t r, g, b;
    for( b = 0; b < 4; b++ )
    {
      for( g = 0; g < 32; g++ )
      {
	for( r = 0; r < 32; r++ )
	{
	  u8g.setRGB(r<<3, g<<3, b<<4 );
	  u8g.drawPixel(g + b*32, r);
	  u8g.setRGB(r<<3, g<<3, (b<<4)+64 );
	  u8g.drawPixel(g + b*32, r+32);
	  u8g.setRGB(r<<3, g<<3, (b<<4)+128 );
	  u8g.drawPixel(g + b*32, r+32+32);
	  u8g.setRGB(r<<3, g<<3, (b<<4)+128+64 );
	  u8g.drawPixel(g + b*32, r+32+32+32);
	}
      }
    }
  }

  // assign default color value
  if ( u8g.getMode() == U8G_MODE_R3G3B2 ) {
    u8g.setColorIndex(255);     // white
  }
  else if ( u8g.getMode() == U8G_MODE_GRAY2BIT ) {
    u8g.setColorIndex(3);         // max intensity
  }
  else if ( u8g.getMode() == U8G_MODE_BW ) {
    u8g.setColorIndex(1);         // pixel on
  }
  else if ( u8g.getMode() == U8G_MODE_HICOLOR ) {
    u8g.setHiColorByRGB(255,255,255);
  }
  u8g.setFont(u8g_font_unifont);
  u8g.drawStr( 0, 22, "Hello World!");
  
  
}

void setup(void) {
  
  // flip screen, if required
  // u8g.setRot180();
  
  // set SPI backup if required
  //u8g.setHardwareBackup(u8g_backup_avr_spi);
  u8g.firstPage();  
}

void loop(void) {
  // picture loop


u8g.firstPage();
 do {
    draw();
       u8g.drawHLine(60,12, 30);
   u8g.drawVLine(10,20, 20);
    //delay(5000);
 } while( u8g.nextPage() );
 delay(2000);
 u8g.firstPage();
 do{
   u8g.drawHLine(50,22,20);
   
 } while(u8g.nextPage());
delay(2000);
  // rebuild the picture after some delay
 // delay(5000);
}

