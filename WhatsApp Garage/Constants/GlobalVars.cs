namespace WhatsApp_Garage.Constants
{
    public class GlobalVars
    {
        public static GlobalVars Instance = new GlobalVars();
        public Dictionary<string, DevNet.WhatsApp> Engines;
        public GlobalVars()
        {
            this.Engines = new Dictionary<string, DevNet.WhatsApp>();
        }

    }
}
