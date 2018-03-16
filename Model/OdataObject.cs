using System;
using System.Collections.Generic;
namespace  ODataClient.Model
{
    public class City
{
    public string CountryRegion { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
}

public class AddressInfo
{
    public string Address { get; set; }
    public City City { get; set; }
}

public class OdataObject
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Emails { get; set; }
    public List<AddressInfo> AddressInfo { get; set; }
    public string Gender { get; set; }
    public long Concurrency { get; set; }
}
}