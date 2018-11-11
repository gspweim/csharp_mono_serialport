using System;
using System.IO.Ports;
namespace hello_console
{
    public class IOClass
    {
		SerialPort serialPort = null;
		String error = null;
        public IOClass()
        {
			//setup serial port
			serialPort = new SerialPort(); 
			serialPort.PortName = "/dev/ttyUSB0";
			serialPort.BaudRate = 115200;              // Baudrate = 115200bps
			serialPort.Parity = Parity.None;          // Parity bits = none  
			serialPort.DataBits = 8;                  // No of Data bits = 8
			serialPort.StopBits = StopBits.One;       // No of Stop bits = 1
			serialPort.ReadTimeout = 500;
            //open serial port
			try
              {
				serialPort.Open();//Open the Port
				Console.WriteLine("Serial Port {0} Opened",this.serialPort.PortName);
              }    
              catch
              {
				error  += "ERROR in Opening Serial Port";
                Console.WriteLine("ERROR in Opening Serial Port");
              }  
        }

		~IOClass(){
			serialPort.Close();
		}
		public void Send(String data) {
			serialPort.Write(data);  
		}

		public String Receive(){
			String sReturn = "";
			String readLine = "";
			Boolean doMe = true;
			while (doMe)
            {
				try
				{
					readLine = serialPort.ReadLine();
					sReturn += readLine;
				}
				catch (TimeoutException) { doMe = false; }
            }

			return sReturn;
		}

		public String Send_and_Receive(String data){
			Send(data);
			return serialPort.ReadLine();
		}
    }
}
//[ 8698.504302] usbserial: USB Serial support registered for generic
//[8698.513613] usbcore: registered new interface driver pl2303
//[8698.513651] usbserial: USB Serial support registered for pl2303
//[8698.513695] pl2303 5-1:1.0: pl2303 converter detected
//[8698.529534] usb 5-1: pl2303 converter now attached to ttyUSB0
///dev/ttyUSB0, UART: 16654, Port: 0x0000, IRQ: 0
//M105 gets temparateure report