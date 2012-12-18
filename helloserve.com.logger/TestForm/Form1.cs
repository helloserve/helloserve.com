using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace TestForm
{
    public partial class Form1 : Form
    {
        private ALog.Logger _logger;
        private ALog.Logger _perfLogger;

        private bool _buttonOn = false;
        private int _key = 0;

        public Form1()
        {
            _logger = ALog.Logger.GetLogger( "TestConfig", ConfigurationManager.ConnectionStrings["ALogContext"].ConnectionString, "Log");
            _logger.Stop();
            _perfLogger = ALog.Logger.GetLogger("PerfConfig", ConfigurationManager.ConnectionStrings["ALogContext"].ConnectionString, "PerfLog");
            _perfLogger.Stop();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _logger.Log(new LogEntry() { Timestamp = DateTime.Now, Category = "Button1", Message = "Pressed Button 1" });
            _buttonOn = !_buttonOn;

            if (_buttonOn)
                _key = _perfLogger.StartElapsedPerfLog("Starting monitoring from Button1");
            else if (_key != 0)
                _perfLogger.LogPerfLog(_key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _logger.Log(new LogEntry() { Timestamp = DateTime.Now, Category = "Button2", Message = "Pressed Button 2" });
            _buttonOn = !_buttonOn;

            if (_buttonOn)
                _key = _perfLogger.StartElapsedPerfLog("Starting monitoring from Button2");
            else if (_key != 0)
                _perfLogger.LogPerfLog(_key);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _logger.Log(new LogEntry() { Timestamp = DateTime.Now, Category = "Button3", Message = "Pressed Button 3" });
            _buttonOn = !_buttonOn;

            if (_buttonOn)
                _key = _perfLogger.StartElapsedPerfLog("Starting monitoring from Button3");
            else if (_key != 0)
                _perfLogger.LogPerfLog(_key);
        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            _logger.Dump();
            _perfLogger.Dump();
        }
    }
}
