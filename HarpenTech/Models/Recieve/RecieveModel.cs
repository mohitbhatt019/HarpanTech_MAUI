using Microsoft.Datasync.Client;


namespace HarpenTech.Models.Recieve
{
    // RecieveModel class represents a model for received items
    public class RecieveModel: DatasyncClientData, IEquatable<RecieveModel>
    {
    

        // Gets or sets the unique identifier for the received item
        //public int Id { get; set; }

        // Gets or sets the container number associated with the received item
        public string ContainerNo { get; set; }

        // Gets or sets the customer associated with the received item
        public string Customer { get; set; }

        // Gets or sets any additional remarks about the received item
        public string Remarks { get; set; }

        public string Image { get; set; }

     
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public bool Equals(RecieveModel other)
            => other != null && other.Id == Id && other.Title == Title && other.IsComplete == IsComplete && other.Image == Image;
    }
}
