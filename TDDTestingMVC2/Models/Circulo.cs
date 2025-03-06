namespace TDDTestingMVC2.Models
{
    public class Circulo
    {
        public int radio { get; set; }

        public double calcularArea() 
        {
            return Math.Round(radio*radio*Math.PI,4);
        }
    }
}
