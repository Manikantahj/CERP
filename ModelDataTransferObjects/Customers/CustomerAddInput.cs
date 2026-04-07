namespace CERP.ModelDataTransferObjects.Customers
{
    public record CustomerAddInput
    {
        public bool? customer_is_end_user { get; init; }
        public string customer_name { get; init; }
        public string customer_logo { get; init; }
        public string customer_signature { get; init; }
        public string customer_country_code { get; init; }
        public string customer_contact_number { get; init; }
        public string customer_gstn { get; init; }
        public string customer_vat { get; init; }
        public string customer_email_address { get; init; }
        public string customer_address_line1 { get; init; }
        public string customer_address_line2 { get; init; }
        public int? customer_state_id { get; init; }
        public string customer_postal_code { get; init; }
        public int? customer_country_id { get; init; }
        public string customer_company_website { get; init; }
        public string customer_currency { get; init; }
        public string customer_note { get; init; }
        public int? customer_created_by { get; init; }
        public DateTime? customer_created_at { get; init; }
    }
}
