using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

namespace JUDOPLAT.API_JUDOPLAT.Data
{
    public enum PdfCategory
    {
        [Display(Name = "Technique")]
        [Description("Technique")]
        Techniques,
        [Display(Name = "Géneral")]
        [Description("Géneral")]

        General,
        [Display(Name = "Techniques de bras")]
        [Description("Techniques de bras")]

        Bras,
        [Display(Name = "Techniques de hanche")]
        [Description("Techniques de hanche")]

        Hanche,
        [Display(Name = "Techniques de Jambe")]
        [Description("Techniques de Jambe")]

        Jambe,


    }


}
