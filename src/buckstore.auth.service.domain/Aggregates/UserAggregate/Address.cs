using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class Address : ValueObject
    {
        public  string _street {get; private set;}
        public  string _zipCode {get; private set;}
        public  string _district {get; private set;}
        public  string _city {get; private set;}
        public  string _state {get; private set;}

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