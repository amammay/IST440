namespace C3_Controls.Models
{
    public class PTTItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string sku { get; set; }
        public double price { get; set; }
        public string progression { get; set; }
        public int[] display_if_voltage_id_is { get; set; }
        public int[] display_if_lens_type_id_is { get; set; }

    }
}