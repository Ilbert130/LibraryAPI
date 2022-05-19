namespace PruebeVC.Servicios
{
    public interface IServicio
    {
        public Guid ObtenerTransient();
        public Guid ObtenerScoped();
        public Guid ObtenerSingleton();
    }

    public class ServicioA: IServicio
    {
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;
        public ServicioA(ServicioTransient servicioTransient, ServicioScoped servicioScoped, ServicioSingleton servicioSingleton)
        {
            this.servicioSingleton = servicioSingleton;
            this.servicioScoped = servicioScoped;
            this.servicioTransient = servicioTransient;
        }

        public Guid ObtenerTransient() {return servicioTransient.Guid;}
        public Guid ObtenerScoped() {return servicioScoped.Guid;}
        public Guid ObtenerSingleton() {return servicioSingleton.Guid;}
    }

    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioTransient
    {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}