provider "azurerm" {
    version = "~>2.0"
    features {}
}

data "azurerm_client_config" "current" {}

locals{
    my_resource_group_name = "apim-workshop-rg"
    my_apim_name = "[YourNameHere]-apim"
    my_location = "southeastasia"
    my_apim_vnet_name = "apim-vnet"
    my_apim_vnet_subnet_name = "apim"
    my_publisher_name = "PublisherName"
    my_publisher_email = "example@example.com"    
    my_sku = "Developer"
    my_sku_count = "1"
    my_tag_environment = "Hothouse"
}

resource "azurerm_resource_group" "apim_rg"{
    name = local.my_resource_group_name
    location = local.my_location
}

resource "azurerm_virtual_network" "apim-vnet"{
    name = local.my_apim_vnet_name
    location = azurerm_resource_group.apim_rg.location
    resource_group_name = azurerm_resource_group.apim_rg.name
    
    address_space = ["10.0.0.0/16"]

    subnet {
        name = "default"
        address_prefix = "10.0.1.0/24"
    }

    subnet {
        name = local.my_apim_vnet_subnet_name
        address_prefix = "10.0.2.0/24"
    }

    subnet {
        name = "appgw"
        address_prefix = "10.0.3.0/24"
    }

    tags = {
        environment = local.my_tag_environment
    }
}

resource "azurerm_api_management" "apim"{
    name = local.my_apim_name
    location = azurerm_resource_group.apim_rg.location
    resource_group_name = azurerm_resource_group.apim_rg.name
    publisher_name = local.my_publisher_name
    publisher_email = local.my_publisher_email
    depends_on = [azurerm_virtual_network.apim-vnet]

    sku_name = "${local.my_sku}_${local.my_sku_count}"

    identity{
        type = "SystemAssigned"
    }
}


