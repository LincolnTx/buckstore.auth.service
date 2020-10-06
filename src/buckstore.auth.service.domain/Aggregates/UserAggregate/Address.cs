using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class Address : ValueObject
    {
        private string _street;
        private string _zipCode;
        private string _district;
        private string _city;
        private string _state;

        public Address() { }

        public Address(string street, string zipCode, string district, string city, string state)
        {
            _street = street;
            _zipCode = zipCode;
            _district = district;
            _city = city;
            _state = state;
        }
    }
}