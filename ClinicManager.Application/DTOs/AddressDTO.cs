namespace ClinicManager.Application.DTOs
{
    public class AddressDTO
    {
        public AddressDTO(int number, string city, string state, string cEP, string neighborhood)
        {
            Number = number;
            City = city;
            State = state;
            CEP = cEP;
            Neighborhood = neighborhood;
        }

        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }
        public string Neighborhood { get; set; }
    }
}
