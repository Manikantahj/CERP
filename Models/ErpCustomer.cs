using System;
using System.Collections.Generic;

namespace CERP.Models;

public partial class ErpCustomer
{
    public int CustomerId { get; set; }

    public bool? CustomerIsEndUser { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerLogo { get; set; }

    public string? CustomerSignature { get; set; }

    public string? CustomerCountryCode { get; set; }

    public string? CustomerContactNumber { get; set; }

    public string? CustomerGstn { get; set; }

    public string? CustomerVat { get; set; }

    public string? CustomerEmailAddress { get; set; }

    public string? CustomerAddressLine1 { get; set; }

    public string? CustomerAddressLine2 { get; set; }

    public int? CustomerStateId { get; set; }

    public string? CustomerPostalCode { get; set; }

    public int? CustomerCountryId { get; set; }

    public string? CustomerCompanyWebsite { get; set; }

    public string? CustomerCurrency { get; set; }

    public string? CustomerNote { get; set; }

    public int? CustomerCreatedBy { get; set; }

    public DateTime? CustomerCreatedAt { get; set; }

    public int? CustomerUpdatedBy { get; set; }

    public DateTime? CustomerUpdatedAt { get; set; }

    public byte CustomerStatus { get; set; }
}
