using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ITAssetTrackingApp.Models;

public partial class AssetAssignmentHistory
{
    public int AssignmentId { get; set; }
    [DisplayName("Asset")]

    public int AssetId { get; set; }
    [DisplayName("Staff")]

    public int StaffId { get; set; }
    [DisplayName("Assigned Date")]

    public DateOnly AssignedDate { get; set; }
    [DisplayName("Returned Date")]

    public DateOnly? ReturnedDate { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
