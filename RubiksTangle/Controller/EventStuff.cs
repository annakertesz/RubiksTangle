namespace RubiksTangle
{
    class EventStuff
    {

        public event EventHandler Stuff; // This dispatches the events
        public delegate void EventHandler(int test); // This describes the event

        public void Start()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                if (Stuff != null)
                {
                    Stuff(8); //dispatch an event
                }
            }
        }
    }
}
