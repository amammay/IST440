using System.Collections.Generic;
using System.Web.Mvc;

namespace C3_Controls.Models
{
    /// <summary>
    ///     This is a base product and contains all the
    ///     information that is the same amongst all products.
    /// </summary>
    public abstract class Control
    {
        public static double DEFAULT_PRICE = 0.0;
        public abstract PricedItem OperatorType { get; }
        public abstract List<PricedItem> Voltages { get; }


        // Converts a string list into a select list for drop downs
        public static IEnumerable<SelectListItem> Convert(List<string> items)
        {
            var listItems = new List<SelectListItem>();
            foreach (var item in items)
                listItems.Add(new SelectListItem {Text = item, Value = item});
            return listItems;
        }
    }


    /// <summary>
    ///     This is the Tower Light product. It contains information
    ///     pertaining to only the Tower Light.
    /// </summary>
    public class TowerLight : Control
    {
        public override PricedItem OperatorType
        {
            get { return new PricedItem {Name = "World Tower Lights", Sku = "WTL", Price = DEFAULT_PRICE}; }
        }

        public override List<PricedItem> Voltages
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "24V AC/DC",
                        Sku = "24V",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "24V AC/DC"
                    },
                    new PricedItem
                    {
                        Name = "120V 60Hz / 110V 50Hz",
                        Sku = "120V",
                        Price = 15.00,
                        Img = "img_voltages.png",
                        Desc = "120V 60Hz/ 110V 50Hz"
                    },
                    new PricedItem
                    {
                        Name = "240V 60Hz / 220V 50Hz",
                        Sku = "240V",
                        Price = 20.00,
                        Img = "img_voltages.png",
                        Desc = "240V 60Hz / 220V 50Hz"
                    }
                };
            }
        }

        public PricedItem ModuleDiameter
        {
            get { return new PricedItem {Name = "50mm (131/32 inches)", Sku = "50MM", Price = DEFAULT_PRICE}; }
        }

        public List<PricedItem> Styles
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Short Panel Mount",
                        Sku = "SPM",
                        Price = 5.00,
                        Img = "img_base_short.png",
                        Desc = "Short Panel Mount"
                    },
                    new PricedItem
                    {
                        Name = "Long Panel Mount",
                        Sku = "LPM",
                        Price = 10.00,
                        Img = "img_base_long.png",
                        Desc = "Long Panel Mount"
                    },
                    new PricedItem
                    {
                        Name = "Direct Mount",
                        Sku = "DM",
                        Price = 15.00,
                        Img = "img_base_short.png",
                        Desc = "Direct Mount"
                    }
                };
            }
        }

        public List<PricedItem> Positions
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Amber LED Continuous",
                        Sku = "ALED_C",
                        Price = 5.00,
                        Img = "img_light_amber.png",
                        Desc = "Amber LED Continuous"
                    },
                    new PricedItem
                    {
                        Name = "Amber LED Flashing",
                        Sku = "ALED_F",
                        Price = 5.00,
                        Img = "img_light_amber.png",
                        Desc = "Amber LED Flashing"
                    },
                    new PricedItem
                    {
                        Name = "Amber LED Rotary",
                        Sku = "ALED_R",
                        Price = 5.00,
                        Img = "img_light_amber.png",
                        Desc = "Amber LED Rotary"
                    },
                    new PricedItem
                    {
                        Name = "Blue LED Continuous",
                        Sku = "BLED_C",
                        Price = 5.00,
                        Img = "img_light_blue.png",
                        Desc = "Blue LED Continuous"
                    },
                    new PricedItem
                    {
                        Name = "Blue LED Flashing",
                        Sku = "BLED_F",
                        Price = 5.00,
                        Img = "img_light_blue.png",
                        Desc = "Blue LED Flashing"
                    },
                    new PricedItem
                    {
                        Name = "Blue LED Rotary",
                        Sku = "BLED_R",
                        Price = 5.00,
                        Img = "img_light_blue.png",
                        Desc = "Blue LED Rotary"
                    },
                    new PricedItem
                    {
                        Name = "Green LED Continuous",
                        Sku = "GLED_C",
                        Price = 5.00,
                        Img = "img_light_green.png",
                        Desc = "Green LED Continous"
                    },
                    new PricedItem
                    {
                        Name = "Green LED Flashing",
                        Sku = "GLED_F",
                        Price = 5.00,
                        Img = "img_light_green.png",
                        Desc = "Green LED Flashing"
                    },
                    new PricedItem
                    {
                        Name = "Green LED Rotary",
                        Sku = "GLED_R",
                        Price = 5.00,
                        Img = "img_light_green.png",
                        Desc = "Green LED Rotary"
                    },
                    new PricedItem
                    {
                        Name = "Red LED Continuous",
                        Sku = "RLED_C",
                        Price = 5.00,
                        Img = "img_light_red.png",
                        Desc = "Red LED Continuous"
                    },
                    new PricedItem
                    {
                        Name = "Red LED Flashing",
                        Sku = "RLED_F",
                        Price = 5.00,
                        Img = "img_light_red.png",
                        Desc = "Red LED Flashing"
                    },
                    new PricedItem
                    {
                        Name = "Red LED Rotary",
                        Sku = "RLED_R",
                        Price = 5.00,
                        Img = "img_light_red.png",
                        Desc = "Red LED Rotary"
                    },
                    new PricedItem
                    {
                        Name = "White LED Continuous",
                        Sku = "WLED_C",
                        Price = 5.00,
                        Img = "img_light_white.png",
                        Desc = "White LED Continous"
                    },
                    new PricedItem
                    {
                        Name = "White LED Flashing",
                        Sku = "WLED_F",
                        Price = 5.00,
                        Img = "img_light_white.png",
                        Desc = "White LED Flashing"
                    },
                    new PricedItem
                    {
                        Name = "White LED Rotary",
                        Sku = "WLED_R",
                        Price = 5.00,
                        Img = "img_light_white.png",
                        Desc = "White LED Rotary"
                    },
                    new PricedItem {Name = "Sound Module Continuous (80/100 dB)", Sku = "SM_C", Price = 5.00},
                    new PricedItem {Name = "Sound Module Intermittent (80/100 dB)", Sku = "SM_I", Price = 5.00}
                };
            }
        }
    }


    /// <summary>
    ///     This is the Push-to-Test button product. It contains
    ///     infomation only pertaining to the push-to-test button.
    /// </summary>
    public class PushToTest : Control
    {
        public override PricedItem OperatorType
        {
            get { return new PricedItem {Name = "Push to Test", Sku = "PTT", Price = DEFAULT_PRICE}; }
        }

        // Affects the 'LampColors'
        public override List<PricedItem> Voltages
        {
            get
            {
                return new List<PricedItem>
                {
                    // Full Voltages only
                    new PricedItem
                    {
                        Name = "6V AC/DC",
                        Sku = "6V",
                        Price = 5.00,
                        Img = "img_voltages.png",
                        Desc = "6V AC/DC"
                    },
                    new PricedItem
                    {
                        Name = "12V AC/DC",
                        Sku = "12V",
                        Price = 5.00,
                        Img = "img_voltages.png",
                        Desc = "12V AC/DC"
                    },
                    new PricedItem
                    {
                        Name = "24V AC/DC",
                        Sku = "24V",
                        Price = 5.00,
                        Img = "img_voltages.png",
                        Desc = "24V AC/DC"
                    },
                    new PricedItem
                    {
                        Name = "120V AC/DC",
                        Sku = "120V",
                        Price = 5.00,
                        Img = "img_voltages.png",
                        Desc = "120V AC/DC"
                    },

                    // Transformers only
                    new PricedItem
                    {
                        Name = "120V AC",
                        Sku = "120V_AC",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "120V AC"
                    },
                    new PricedItem
                    {
                        Name = "240V AC",
                        Sku = "240V_AC",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "240V AC"
                    },
                    new PricedItem
                    {
                        Name = "277V AC",
                        Sku = "277V_AC",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "277V AC"
                    },
                    new PricedItem
                    {
                        Name = "480V AC",
                        Sku = "480V_AC",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "480V AC"
                    },

                    // Resisters only
                    // (Also includes 120V AC/DC but it was already added at
                    // the beginning of this list)
                    new PricedItem
                    {
                        Name = "240V AC/DC",
                        Sku = "240V",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "240V AC/DC"
                    },
                    new PricedItem
                    {
                        Name = "480V AC/DC",
                        Sku = "480V",
                        Price = 10.00,
                        Img = "img_voltages.png",
                        Desc = "480V AC/DC"
                    }
                };
            }
        }

        // Affects the 'Voltages'
        public List<PricedItem> BasicOperators
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Full Voltage",
                        Sku = "FV",
                        Price = 5.00,
                        Img = "img_push_to_test.png",
                        Desc = "Full Voltage"
                    },
                    new PricedItem
                    {
                        Name = "Transformer (50/60 Hz)",
                        Sku = "TSF",
                        Price = 5.00,
                        Img = "img_push_to_test.png",
                        Desc = "Transformer (50/60 Hz)"
                    },
                    new PricedItem
                    {
                        Name = "Resister",
                        Sku = "RSTR",
                        Price = 5.00,
                        Img = "img_push_to_test.png",
                        Desc = "Resister"
                    }
                };
            }
        }

        public List<PricedItem> LampColors
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "No Lamp",
                        Sku = "LC_N",
                        Price = 0.00,
                        Img = "img_lens_clear.png",
                        Desc = "No Lamp"
                    },
                    new PricedItem
                    {
                        Name = "Clear Incandescent",
                        Sku = "LC_CI",
                        Price = 5.00,
                        Img = "img_lamp_clear_incandescent.png",
                        Desc = "Clear Incadescent"
                    },
                    new PricedItem
                    {
                        Name = "Amber LED",
                        Sku = "LC_A",
                        Price = 5.00,
                        Img = "img_lamp_amber.png",
                        Desc = "Amber LED"
                    },
                    new PricedItem
                    {
                        Name = "Blue LED",
                        Sku = "LC_B",
                        Price = 5.00,
                        Img = "img_lamp_blue.png",
                        Desc = "Blue LED"
                    },
                    new PricedItem
                    {
                        Name = "Green LED",
                        Sku = "LC_G",
                        Price = 5.00,
                        Img = "img_lamp_green.png",
                        Desc = "Green LED"
                    },
                    new PricedItem
                    {
                        Name = "Red LED",
                        Sku = "LC_R",
                        Price = 5.00,
                        Img = "img_lamp_red.png",
                        Desc = "Red LED"
                    },
                    new PricedItem
                    {
                        Name = "White LED",
                        Sku = "LC_W",
                        Price = 5.00,
                        Img = "img_lamp_white.png",
                        Desc = "White LED"
                    },

                    // 6V AC/DC | 120V AC | 240V AC | 277V AC | 480V AC only
                    new PricedItem
                    {
                        Name = "Clear Flashing Incandescent",
                        Sku = "LC_CLI",
                        Price = 10.00,
                        Img = "img_lamp_clear_flashing_incandescent.png",
                        Desc = "Clear Flashing Incandescent"
                    },

                    // 120V AC/DC | 240V AC/DC | 480V AC/DC only
                    new PricedItem
                    {
                        Name = "Neon Green",
                        Sku = "LC_NG",
                        Price = 10.00,
                        Img = "img_lamp_neon_green.png",
                        Desc = "Neon Green"
                    },
                    new PricedItem
                    {
                        Name = "Neon Red",
                        Sku = "LC_NR",
                        Price = 10.00,
                        Img = "img_lamp_neon_red.png",
                        Desc = "Neon Red"
                    }
                };
            }
        }

        // Affects the 'LensTypes'
        public List<PricedItem> ClampRings
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Black Polyester (Type 4X)",
                        Sku = "BP",
                        Price = 20.00,
                        Img = "img_clamp_ring.png",
                        Desc = "Black Polyester (Type 4X)"
                    },
                    new PricedItem
                    {
                        Name = "Aluminum (Type 4)",
                        Sku = "AL",
                        Price = 20.00,
                        Img = "img_clamp_ring.png",
                        Desc = "Aluminum (Type 4)"
                    }
                };
            }
        }

        public List<PricedItem> LensTypes
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Illuminated Lens",
                        Sku = "IPBL",
                        Price = 5.00,
                        Img = "img_lens_clear.png",
                        Desc = "Illuminated Lens"
                    },
                    new PricedItem
                    {
                        Name = "Illuminated Mushroom Lens",
                        Sku = "IPBML",
                        Price = 5.00,
                        Img = "img_lens_clear.png",
                        Desc = "Illuminated Mushroom Lens"
                    },

                    // Black Polyester only
                    new PricedItem
                    {
                        Name = "Guarded Illuminated Lens",
                        Sku = "GIPBL",
                        Price = 5.00,
                        Img = "img_lens_clear.png",
                        Desc = "Guarded Illuminated Lens"
                    },
                    new PricedItem
                    {
                        Name = "Shrouded Illuminated Mushroom Lens",
                        Sku = "SIPBML",
                        Price = 5.00,
                        Img = "img_lens_clear.png",
                        Desc = "Shrouded Illuminated Mushroom Lens"
                    }
                };
            }
        }

        public List<PricedItem> LensColors
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem
                    {
                        Name = "Amber Lens",
                        Sku = "ALE",
                        Price = 5.00,
                        Img = "img_lens_amber.png",
                        Desc = "Amber Lens"
                    },
                    new PricedItem
                    {
                        Name = "Blue Lens",
                        Sku = "BLE",
                        Price = 5.00,
                        Img = "img_lens_blue.png",
                        Desc = "Blue Lens"
                    },
                    new PricedItem
                    {
                        Name = "Clear Lens",
                        Sku = "CLE",
                        Price = 5.00,
                        Img = "img_lens_clear.png",
                        Desc = "Clear Lens"
                    },
                    new PricedItem
                    {
                        Name = "Green Lens",
                        Sku = "GLE",
                        Price = 5.00,
                        Img = "img_lens_green.png",
                        Desc = "Green Lens"
                    },
                    new PricedItem
                    {
                        Name = "Red Lens",
                        Sku = "RLE",
                        Price = 5.00,
                        Img = "img_lens_red.png",
                        Desc = "Red Lens"
                    },
                    new PricedItem
                    {
                        Name = "White Lens",
                        Sku = "WLE",
                        Price = 5.00,
                        Img = "img_lens_white.png",
                        Desc = "White Lens"
                    },
                    new PricedItem
                    {
                        Name = "Yellow Lens",
                        Sku = "YLE",
                        Price = 5.00,
                        Img = "img_lens_yellow.png",
                        Desc = "Yellow Lens"
                    }
                };
            }
        }

        public List<PricedItem> Options
        {
            get
            {
                return new List<PricedItem>
                {
                    new PricedItem {Name = "No IP20 Guard", Sku = "NIPG", Price = 0.00},
                    new PricedItem {Name = "IP20 Guard", Sku = "IPG", Price = 0.00}
                };
            }
        }
    }


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