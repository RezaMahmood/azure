# APIM Workshop Guidance

This document provides hints and tips to complete the Workshop challenges.  It will not give you any specific answers.

## Setup
* Create an instance of API Management
    a. Create through the [portal](https://docs.microsoft.com/en-gb/azure/api-management/get-started-create-service-instance) or follow [Powershell](https://docs.microsoft.com/en-gb/azure/api-management/powershell-create-service-instance) instructions.
* [Configure an API endpoint](https://docs.microsoft.com/en-gb/azure/api-management/import-and-publish) (choose from one above or create one using Logic App)
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
  * [Deploy into a virtual network and prevent access from public internet](https://docs.microsoft.com/en-gb/azure/api-management/api-management-using-with-internal-vnet) [Note this can take up to 45 minutes]
  * Configure a per port flow for 3 endpoints using ports 8000 to 8003
    * Deploy Application Gateway into the vnet
	  * Configure Application Gateway to be the only way to talk to APIM via public IP
      - Configure AppGateway to map ports
 	  * Look at https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-integrate-internal-vnet-appgateway
 	  * Be aware of https://docs.microsoft.com/en-us/azure/api-management/api-management-using-with-internal-vnet#apim-dns-configuration
    * Ensure Network is [configured correctly](https://docs.microsoft.com/en-gb/azure/api-management/api-management-using-with-vnet#a-namenetwork-configuration-issues-acommon-network-configuration-issues) to allow access to APIM
      - [Override backend routing](https://docs.microsoft.com/en-us/azure/application-gateway/application-gateway-web-app-overview)
* [Inject Basic Auth credentials](https://docs.microsoft.com/en-gb/azure/api-management/api-management-authentication-policies#Basic) as part of the flow.  e.g. If the target endpoint requires Basic Authentication but source endpoint does not have those credentials
* You want to host your APIM instance using your own [custom domain](https://docs.microsoft.com/en-us/azure/api-management/configure-custom-domain) and [secure using SSL](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-ca-certificates).
* [Apply rate limiting to an API endpoint](https://docs.microsoft.com/en-us/azure/api-management/api-management-sample-flexible-throttling)
			
## Monitoring
* Monitor your APIM instance
  * [Understand the metrics available](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-use-azure-monitor) [How would you monitor Capacity?](https://docs.microsoft.com/en-us/azure/api-management/api-management-capacity)
  * [Add a Notification to warn of Quota constraints](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-configure-notifications)
* Set up a Log Analytics workspace and send APIM diagnostics to it
	
## Availability and Backup
* Make your APIM instance highly available across [multiple regions](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-deploy-multi-region).
* [Backup your APIM instance configuration](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-disaster-recovery-backup-restore) and restore it to another region.  Note that you may get a failure when trying to get a JWT token for Windows Azure Service Management API depending on your organisation's permissions
    