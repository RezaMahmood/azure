# Basic Set up

  API's used in this workshop:

  https://reza-stubs.***.net
- /onprem/noauth
- /onprem/basicauth
- /supplier/noauth
-  /supplier/basicauth
- /EchoHeaders

All endpoints support POST and GET methods.  POST will save to blob storage. GET will echo message in response

Sample API message:			
>            {
>			    "TargetName": "TargetAPIName",
>			    "Payload": {
>			        "MessageContent": "Hello World",
>			        "MessageName": "Hello World Message Name"
>			    }
>
>			}

Change the TargetName to something that identifies you

## Authorization
Use following header for Basic Authentication
> Authorization Basic cmV6YTpvaHJlYWxseSE/IQ==

## Additional stuff
	- Download Postman to test REST endpoints - http://www.getpostman.com
	- Download SoapUI to test SOAP endpoints - https://www.soapui.org/downloads/soapui.html
	- SOAP WSDL: http://www.dneonline.com/calculator.asmx?wsdl

# Workshop Challenges

## Setup
1. Create an instance of API Management
2. Configure an API endpoint (choose from one above or create one using Logic App)
	a. Create a POST operation with simple passthrough
	b. Create a POST operation
		i. Rewrite the URI to target a different backend based on querystring
c. Create a GET operation to an endpoint that doesn't exist.  
	i. Configure a custom response
d. Create a GET operation to an endpoint. 
	i. Change the value of TargetName as it flows through API Management. 
	ii. Observe the change in the response saved to blob storage
e. Create a GET operation to /EchoHeaders
	i. Update the policy to change the header response
	ii. Create a Postman request
	iii. Verify response contains custom header
3. Enable source control for the configuration of APIM

## Security
1. Secure your APIM instance
a. Deploy into a virtual network and prevent access from public internet [Note this can take up to 45 minutes]
b. Configure a per port flow for 3 endpoints using ports 8000 to 8003
i. Deploy Application Gateway into the vnet
				- Configure Application Gateway to be the only way to talk to APIM via public IP
	- Configure AppGateway to map ports
5. Inject Basic Auth credentials as part of the flow.  e.g. If the target endpoint requires Basic Authentication but source endpoint does not have those credentials
6. You want to host your APIM instance using your own custom domain and secure using SSL.
7. Apply rate limiting to an API endpoint
			
## Monitoring
1. Monitor your APIM instance
a. Understand the metrics available
b. Add a Notification to warn of Quota constraints
c. Set up a Log Analytics workspace and send APIM diagnostics to it
	
## Availability and Backup
1. Make your APIM instance highly available across multiple regions. Note this will require the Premium tier of APIM
2. Backup your APIM instance configuration and restore it to another region.