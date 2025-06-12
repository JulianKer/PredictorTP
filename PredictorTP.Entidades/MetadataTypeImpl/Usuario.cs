using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorTP.Entidades.EF;
using PredictorTP.Entidades.MetadataType;

namespace PredictorTP.Entidades.EF
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
    }
}
