namespace C3_Controls.Models
{
    /// <summary>
    ///     This resemebles all pieces of a control. It has fields
    ///     to store specific information about each part of a control.
    /// </summary>
    public class PricedItem
    {
        public static string BASE_IMG = "../../Content/assets/";
        private string _Img;
        public string Name { get; set; }
        public string Sku { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }

        public string Img
        {
            get { return _Img; }
            set
            {
                // Sets all images to the path ../../Content/assets/ folder
                _Img = BASE_IMG + value;
            }
        }


        public override string ToString()
        {
            return Name.ToLower().Replace(" ", "_");
        }
    }
}