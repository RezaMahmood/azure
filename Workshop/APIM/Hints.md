# APIM Workshop Guidance

This document provides hints and tips to complete the Workshop challenges.  It will not give you any specific answers.

## Setup
1. Create an instance of API Management
    a. Create through the [portal](https://docs.microsoft.com/en-gb/azure/api-management/get-started-create-service-instance) or follow [Powershell](https://docs.microsoft.com/en-gb/azure/api-management/powershell-create-service-instance) instructions.
2. [Configure an API endpoint](https://docs.microsoft.com/en-gb/azure/api-management/import-and-publish) (choose from one above or create one using Logic App)
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
    a. [Deploy into a virtual network and prevent access from public internet](https://docs.microsoft.com/en-gb/azure/api-management/api-management-using-with-internal-vnet) [Note this can take up to 45 minutes]
    b. Configure a per port flow for 3 endpoints using ports 8000 to 8003
      - Deploy Application Gateway into the vnet
	  - Configure Application Gateway to be the only way to talk to APIM via public IP
      - Configure AppGateway to map ports
 	  - Look at https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-integrate-internal-vnet-appgateway
 	  - Be aware of https://docs.microsoft.com/en-us/azure/api-management/api-management-using-with-internal-vnet#apim-dns-configuration
      - Ensure Network is [configured correctly](https://docs.microsoft.com/en-gb/azure/api-management/api-management-using-with-vnet#a-namenetwork-configuration-issues-acommon-network-configuration-issues) to allow access to APIM
      - [Override backend routing](https://docs.microsoft.com/en-us/azure/application-gateway/application-gateway-web-app-overview)
2. [Inject Basic Auth credentials](https://docs.microsoft.com/en-gb/azure/api-management/api-management-authentication-policies#Basic) as part of the flow.  e.g. If the target endpoint requires Basic Authentication but source endpoint does not have those credentials
3. You want to host your APIM instance using your own [custom domain](https://docs.microsoft.com/en-us/azure/api-management/configure-custom-domain) and [secure using SSL](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-ca-certificates).
4. [Apply rate limiting to an API endpoint](https://docs.microsoft.com/en-us/azure/api-management/api-management-sample-flexible-throttling)
			
## Monitoring
8. Monitor your APIM instance
a. [Understand the metrics available](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-use-azure-monitor) [How would you monitor Capacity?](https://docs.microsoft.com/en-us/azure/api-management/api-management-capacity)
b. [Add a Notification to warn of Quota constraints](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-configure-notifications)
c. Set up a Log Analytics workspace and send APIM diagnostics to it
	
## Availability and Backup
9. Make your APIM instance highly available across [multiple regions](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-deploy-multi-region).
10. [Backup your APIM instance configuration](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-disaster-recovery-backup-restore) and restore it to another region.  Note that you may get a failure when trying to get a JWT token for Windows Azure Service Management API depending on your organisation's permissions
    