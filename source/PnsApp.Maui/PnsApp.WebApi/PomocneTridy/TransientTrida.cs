namespace PnsApp.WebApi.PomocneTridy
{
    public class SpolecnyPredek
    {
        private Guid guid = Guid.NewGuid();
        private DateTime _dateTime = DateTime.Now;
    }

    public class TransientTrida: SpolecnyPredek
    {
        private ScopeTrida _scope;
        public TransientTrida(ScopeTrida scope)
        {
            this._scope = scope;
        }
    }

    public class ScopeTrida : SpolecnyPredek
    {
        //private TransientTrida trida;
        public ScopeTrida(/*TransientTrida trida*/)
        {
            //this.trida = trida;
        }
    }

    public class SingletonTrida : SpolecnyPredek
    {
        //private ScopeTrida _scope;

        public SingletonTrida(/*ScopeTrida scope*/)
        {
            //this._scope = scope;
        }
    }
}
