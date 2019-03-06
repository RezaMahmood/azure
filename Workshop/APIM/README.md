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
	- Download Postman
	- SOAP WSDL: http://www.dneonline.com/calculator.asmx?wsdl

# Workshop Challenges
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
	4. Secure your APIM instance
		a. Deploy into a virtual network and prevent access from public internet
		b. Configure a per port flow for 3 endpoints using ports 8000 to 8003
			i. Deploy Application Gateway into the vnet
				1) Configure Application Gateway to be the only way to talk to APIM via public IP
				2) Configure AppGateway to map ports
			
		
		
	
