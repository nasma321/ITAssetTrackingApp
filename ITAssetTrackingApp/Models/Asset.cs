using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ITAssetTrackingApp.Models;

public partial class Asset
{
    public int AssetId { get; set; }
    [DisplayName("Asset Number")]

    public string AssetNumber { get; set; } = null!;
    [DisplayName("Asset Type")]

    public string AssetType { get; set; } = null!;

    public string Status { get; set; } = null!;
    [DisplayName("Purchase Date")]

    public DateOnly PurchasedDate { get; set; }
    [DisplayName("Discarded Date")]

    public DateOnly? DiscardedDate { get; set; }
    [DisplayName("Assigned Staff")]

    public int? AssignedStaffId { get; set; }
    [DisplayName("Assigned Date")]

    public DateOnly? AssignedDate { get; set; }
    [DisplayName("Last Return Date")]

    public DateOnly? LastReturnedDate { get; set; }

    public virtual ICollection<AssetAssignmentHistory> AssetAssignmentHistories { get; set; } = new List<AssetAssignmentHistory>();
    [DisplayName("Assigned Staff")]

    public virtual Staff? AssignedStaff { get; set; }
}
