using System.Text.Json.Serialization;

namespace HarpenTech.Models.Container;

public class ShipInfo
{
    public int ShipId { get; set; }
    public string ShipName { get; set; }

    public List<ContainerInfo> containerInfos  { get; set; }
}
// ContainerInfo class represents information about a container
public class ContainerInfo
{
    // Gets or sets the container number
    public string ContainerNumber { get; set; }

    // Gets or sets the customer associated with the container
    public string Customer { get; set; }

    // Gets or sets the registration number of the container
    public string RegNo { get; set; }

    // Gets or sets the transporter of the container
    public string Transporter { get; set; }

    // Gets or sets any additional remarks about the container
    public string Remarks { get; set; }

    // Gets or sets the ISO code of the container
    public string Iso { get; set; }

    // Gets or sets the status of the container
    public string Status { get; set; }

    // Gets or sets the grading information of the container
    public string Grading { get; set; }

    // Gets or sets the information about any damage to the container
    public string Damage { get; set; }

    // Gets or sets the image data associated with the container
    public byte[] Image { get; set; }
}
