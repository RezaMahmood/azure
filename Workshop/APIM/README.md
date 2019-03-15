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
* Create an instance of API Management
* Configure an API endpoint (choose from one above or create one using Logic App)
  * Create a POST operation with simple passthrough
  * Create a POST operation
    * Rewrite the URI to target a different backend based on querystring
  * Create a GET operation to an endpoint that doesn't exist.  
    * Configure a custom response
  * Create a GET operation to an endpoint. 
	* Change the value of TargetName as it flows through API Management. 
	* Observe the change in the response saved to blob storage
  * Create a GET operation to /EchoHeaders
    * Update the policy to change the header response
	* Create a Postman request
	* Verify response contains custom header
* Enable source control for the configuration of APIM

## Security
* Secure your APIM instance
  * Deploy into a virtual network and prevent access from public internet [Note this can take up to 45 minutes]
  * Configure a per port flow for 3 endpoints using ports 8000 to 8003
  * Deploy Application Gateway into the vnet
    * Configure Application Gateway to be the only way to talk to APIM via public IP
	* Configure AppGateway to map ports
* Inject Basic Auth credentials as part of the flow.  e.g. If the target endpoint requires Basic Authentication but source endpoint does not have those credentials
* You want to host your APIM instance using your own custom domain and secure using SSL.
* Apply rate limiting to an API endpoint
			
## Monitoring
* Monitor your APIM instance
  * Understand the metrics available
  * Add a Notification to warn of Quota constraints
  * Set up a Log Analytics workspace and send APIM diagnostics to it
	
## Availability and Backup
* Make your APIM instance highly available across multiple regions. Note this will require the Premium tier of APIM
* Backup your APIM instance configuration and restore it to another region.