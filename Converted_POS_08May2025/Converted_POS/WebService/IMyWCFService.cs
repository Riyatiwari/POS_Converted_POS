 
using Microsoft.AspNetCore.Mvc;

 
public interface IMyWCFService
{
    
    string SomeMethod(string input);
    string MyMethod();
    [HttpGet("WebLogin")]
    string Login(string authUsername, string authPassword, string user, string pass, string IP_Address, string mac_id, string store_name);
}

