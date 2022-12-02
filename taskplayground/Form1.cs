using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace taskplayground
{
    public partial class Form1 : Form
    {

        private delegate void SafeCallDelegate(string text);

        private Thread thread2 = null;

        private delegate void SafeCallDelegate3(object o);
        private Thread thread4 = null;
        int mod2 = 0;


      

        public Form1()
        {
            InitializeComponent();
        }

        static Random ran = new Random();


        public void executetasks()
        { 
        
            // Wait on a single task with no timeout.
        Task taskA = Task.Factory.StartNew(() => Worker(1));
        taskA.Wait();
       Text = "Task A Finished.";

        // Wait on a single task with a timeout.
        Task taskB = Task.Factory.StartNew(() => Worker(1));
        taskB.Wait(2); //Wait for 2 seconds.

        if (taskB.IsCompleted)
        {
            Text = "Task B Finished.";

        }
        else
        {
        Text = "Timed out without Task B finishing.";
            }
        
        }

         void Worker(int waitTime)
        {

        thread4 = new Thread(new ThreadStart(SetText3));
        thread4.Start();
        Thread.Sleep(1);
        Thread.Sleep(waitTime);
        }


         private void WriteTextSafe(string text)
         {
             if (textBox1.InvokeRequired)
             {
                 var d = new SafeCallDelegate(WriteTextSafe);
                 textBox1.Invoke(d, new object[] { text });
             }
             else
             {
                 textBox1.Text = text;
             }
         }

         private void SetText()
         {
             WriteTextSafe("This text was set safely.");
         }


         private void WriteTextSafe3(object oo)
         {
             if (button5.InvokeRequired)
             {
                 var d = new SafeCallDelegate3(WriteTextSafe3);
                 button5.Invoke(d, new object[] { oo });
             }
             else
             {
                 if (mod2 == 0)
                 {
                     while (button5.Top < 600)
                     {
                         button5.Top += 10;
                         Refresh();
                         if (button5.Top >= 600) { mod2 = 1; }
                     };
                 }
                 else
                 {
                     while (button5.Top > 100)
                     {
                         button5.Top -= 10;
                         Refresh();
                         if (button5.Top <= 100) { mod2 = 0; }
                     };
                 }
             }
         }

         private void SetText3()
         {
             WriteTextSafe3(this.button4);

         }

         double Worker1()
        {
            int i = ran.Next(1000000);
            Thread.SpinWait(i);
            return i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            executetasks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            thread2 = new Thread(new ThreadStart(SetText));
            thread2.Start();
            Thread.Sleep(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread4 = new Thread(new ThreadStart(SetText3));
            thread4.Start();
            Thread.Sleep(1);
        }
    }
}
