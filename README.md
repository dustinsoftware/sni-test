# SNITCPHandle checker

This verifies the behavior of clients connecting to SQL Server using MultiSubnetFailover. It uses reflection to access SNITCPHandle, an internal class used by SqlConnection, which is responsible for enumerating addresses returned by a DNS lookup and attempting to connect to each of them. 
