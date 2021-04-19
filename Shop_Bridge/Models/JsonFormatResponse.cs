
namespace Master_Operations_Proj.Models
{
    public class JsonFormatResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic output_data { get; set; }
    }
}