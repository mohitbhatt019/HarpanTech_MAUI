using SQLite;

namespace HarpenTech.Models.Dispatch
{
    // DispatchModel class represents a model for dispatch information
    public class DispatchModel
    {
        // Gets or sets the unique identifier for the dispatch
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Gets or sets the customer associated with the dispatch
        public string Customer { get; set; }

        // Gets or sets the booking reference for the dispatch
        public string BookingReference { get; set; }

        // Gets or sets the container number for the dispatch
        public int ContainerNo { get; set; }

        // Gets or sets the ISO code for the dispatch
        public int ISO { get; set; }

        // Gets or sets the temperature instruction for the dispatch
        public string TempInstruction { get; set; }

        // Gets or sets the temperature information for the dispatch
        public string Temp { get; set; }

        // Gets or sets the first seal information for the dispatch
        public string Seal1 { get; set; }

        // Gets or sets the second seal information for the dispatch
        public string Seal2 { get; set; }

        public DispatchModel Clone() => MemberwiseClone() as DispatchModel;


        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Customer))
            {
                return (false, $"{nameof(Customer)} is required.");
            }
            else if (ContainerNo <= 0)
            {
                return (false, $"{nameof(ContainerNo)} should be greater than 0.");
            }
            else if (string.IsNullOrWhiteSpace(BookingReference))
            {
                return (false, $"{nameof(BookingReference)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(TempInstruction))
            {
                return (false, $"{nameof(TempInstruction)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Temp))
            {
                return (false, $"{nameof(Temp)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Seal1))
            {
                return (false, $"{nameof(Seal1)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Seal2))
            {
                return (false, $"{nameof(Seal2)} is required.");
            }
            else if (ISO <= 0)
            {
                return (false, $"{nameof(ISO)} should be greater than 0.");
            }
            return (true, null);
        }
    }
}
