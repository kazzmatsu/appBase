using System;
using System.IO;
using System.Globalization;
using System.Threading;

namespace WildSlevin
{
    public class AppBase : IDisposable
    {
        public string AppName { get; set; }
        public string AppModule { get; set; }
        public string AppLibray { get; set; }
        public string AppConfig { get; set; }
        private Timer m_timer;
        public  AppBase()
        {
            var dDef = new [] {"lib","etc"};
            var timerHandler = new TimerCallback ( NotifyTime );
            m_timer = new Timer ( timerHandler, null , 0, (1 * 1000));
            this.AppModule = Directory.GetCurrentDirectory();
            this.AppName = Directory.GetParent(this.AppModule).FullName;
            this.AppLibray = string.Format(@"{0}\{1}",this.AppName, dDef[0]);
            this.AppConfig = string.Format(@"{0}\{1}",this.AppName, dDef[1]);
        }
        ~AppBase()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            m_timer.Dispose();
            GC.SuppressFinalize(this);
        }
        private void NotifyTime ( object state )
        {
            string timeText = string.Format("(" + DateTime.Now.ToString() + ")");
            Console.WriteLine ( timeText );
        }
        public static void Main ( string [ ] args)
        {
            var mmm = new AppBase();
            mmm.ClsMain( mmm );
        }
        internal void ClsMain ( AppBase xbase )
        {
            Console.WriteLine(@" Debug base    := {0}", xbase.AppName );
            Console.WriteLine(@" Debug Module  := {0}", xbase.AppModule );
            Console.WriteLine(@" Debug Library := {0}", xbase.AppLibray );
            Console.WriteLine(@" Debug Config  := {0}", xbase.AppConfig );
        }
    }
}
