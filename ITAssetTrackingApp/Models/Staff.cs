using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ITAssetTrackingApp.Models;

public partial class Staff
{
    public int StaffId { get; set; }
    [DisplayName("National ID Card Number")]

    public string NationalIdcardNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Department { get; set; } = null!;

    public virtual ICollection<AssetAssignmentHistory> AssetAssignmentHistories { get; set; } = new List<AssetAssignmentHistory>();

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
