using System;
using System.Windows.Forms;
using System.Drawing;

namespace hello_console
{
	public class MyForm : Form
    {
		// Properties.
        private Label label;
        private TextBox myName;
        private Button btn;
		private Button btnRead;
		private PictureBox pctWait;
		IOClass cr10Printer = null;

        public MyForm ()
        {
            // Call the function to render the objects.
            Text = ".NET on Linux";

            this.Size = new Size(300, 350);
            render();
			cr10Printer = new IOClass();
        }

        private void render() {
            label = new Label { Text = "Command to send: ", Location = new Point(10, 35) };
            myName = new TextBox { Location = new Point(10, 60), Width = 150 };
            btn = new Button { Text = "Send Away", Location = new Point(10, 100) };
			btnRead = new Button { Text = "Read from Printer", Location = new Point(10, 140) };
			pctWait = new PictureBox { Image = Image.FromFile("loading_spinner.gif"),
				                        Location = new Point(10, 180),
				                        Size = new Size(200,200) };
			pctWait.Visible = false;

			btn.Click += Btn_Click; // Handle the event.
			btnRead.Click += Btn_Read_Click;


            // Attach these objects to the graphics window.
            this.Controls.Add(label);
            this.Controls.Add(myName);
            this.Controls.Add(btn);
			this.Controls.Add(btnRead);
			this.Controls.Add(pctWait);
        }

        // Handlers
        void Btn_Click (object sender, EventArgs e)
        {
			String response = null;
			response = cr10Printer.Send_and_Receive(myName.Text+ System.Environment.NewLine );
            MessageBox.Show ("Sent " +  myName.Text + " got back " + response);
        }

		void Btn_Read_Click(object sender, EventArgs e)
        {
            String response = null;
			Cursor.Current = Cursors.WaitCursor;
			pctWait.Visible = true;
			Application.DoEvents();
			Artificial_Delay();
            response = cr10Printer.Receive();
			Cursor.Current = Cursors.Default;
			pctWait.Visible = false;
            MessageBox.Show("Read " + response);
        }

        void Artificial_Delay()
		{
			for (int i = 0; i < 10; i++) {
				System.Threading.Thread.Sleep(500); //pause so we can mimic a slow response
				Application.DoEvents();
			}
		}
    }
}
