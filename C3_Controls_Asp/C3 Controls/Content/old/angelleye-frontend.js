jQuery(document).ready(function ($){
    if (angelleye_frontend.is_cart == "yes"){
        $("#paypal_ec_button_product input").click(function(){
            var angelleye_action = $(this).data('action');
            $('form.cart').attr( 'action', angelleye_action );
            $(this).attr('disabled', 'disabled');
            $('form.cart').submit();
            $(".angelleyeOverlay").show();
            return false;
        });
        $(".paypal_checkout_button").click(function(){
            $(".angelleyeOverlay").show();
            return true;
        });
    }
    if (angelleye_frontend.is_checkout == "yes"){
        jQuery("form.checkout").on( 'change', 'select#paypal_pro_card_type', function(){
            var card_type = jQuery("#paypal_pro_card_type").val();
            var csc = jQuery("#paypal_pro_card_csc").parent();
            if (card_type == "Visa" || card_type == "MasterCard" || card_type == "Discover" || card_type == "AmEx" ) {
                csc.fadeIn("fast");
            } else {
                csc.fadeOut("fast");
            }
            if (card_type == "Visa" || card_type == "MasterCard" || card_type == "Discover") {
                jQuery('.paypal_pro_card_csc_description').text(angelleye_frontend.three_digits);
            } else if ( card_type == "AmEx" ) {
                jQuery('.paypal_pro_card_csc_description').text(angelleye_frontend.four_digits);
            } else {
                jQuery('.paypal_pro_card_csc_description').text('');
            }
        });
        jQuery('select#paypal_pro_card_type').change();
    }
});
