using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life
{
    internal class Program
    {
        //[STAThread]
        public static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //GUIforLife form2 = new GUIforLife();

            //Application.Run(form2);
            Console.WriteLine(Life.GetLiveInStepsForTask);
            
            var a = new ConsoleLife(30,60,0.1);
        }
    }
}
    
