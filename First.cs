using System;
using WildSlevin;

namespace WildSlevinText
{
    public class WildSlevinX
    {
        public static void Main  ( string [ ] args )
        {
            var mmm = new WildSlevinX();
            mmm.ClsMain();
        }
        internal void ClsMain ( )
        {
            using ( var xbase = new AppBase())
            {
                Console.WriteLine(@" Run Base AppName   := {0} ", xbase.AppName);
                Console.WriteLine(@" Run Base AppModule := {0} ", xbase.AppModule);
                Console.WriteLine(@" Run Base AppLibray := {0} ", xbase.AppLibray);
                Console.WriteLine(@" Run Base AppConfig := {0} ", xbase.AppConfig);
                Func1(xbase.AppConfig);
            }
        }
        internal void Func1( string confBase )
        {
            var iniFiles = @"T-Star_DB2XLSX.ini";
            var x = string.Format(@"{0}\{1}",confBase,iniFiles);
            Console.WriteLine( @" {0}",x);
        }
    }
}
