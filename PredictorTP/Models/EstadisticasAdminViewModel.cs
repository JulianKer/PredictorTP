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
    }
}
