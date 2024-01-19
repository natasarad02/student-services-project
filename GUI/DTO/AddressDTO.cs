using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentskaSluzba.Model;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class AddressDTO : INotifyPropertyChanged
    {

        private string street;

        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }

        }

        private int number;

        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }

        }

        private string city;

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }

        }

        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }

        }
    
       // public string Error => null;
       // private readonly string[] _validatedProperties = { "Country", "Street", "Number" };

      
        public Address ToAddress()
        {
            Address a = new Address(street, number, city, country);
            return a;
        }

        public AddressDTO() { }
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddressDTO(Address address)
        {
            street = address.Street; 
            number = address.Number;
            city = address.City; 
            country = address.Country;
        }


        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string this[string columnName]
        {
             get
            {
                if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))

                        return "Street is required";

                } else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";
                } else if(columnName == "Number")
                {
                    if (Number == 0)
                        return "Street number is required";
                }



                return null;

                    
                }

            }

        }
    }

