namespace PredictorTP.Models
{
    public class EstadisticasAdminViewModel
    {
        public List<string> Labels { get; set; } = new();
        public List<int> Cantidades { get; set; } = new();

        public List<string> LabelsUsuariosAdmin { get; set; } = new();
        public List<int> CantidadesUsuariosAdmin { get; set; } = new();

        public List<string> LabelsPersonasEmociones { get; set; } = new();
        public List<int> CantidadesPersonasEmociones { get; set; } = new();

        public List<string> LabelsPolaridad { get; set; } = new();
        public List<int> CantidadesPolaridad { get; set; } = new();

        public List<string> LabelsIdioma { get; set; } = new();
        public List<int> CantidadesIdioma { get; set; } = new();

        public List<string> LabelsEmociones { get; set; } = new();
        public List<int> CantidadesEmociones { get; set; } = new();
    }
}
