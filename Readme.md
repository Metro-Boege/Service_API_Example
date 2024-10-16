
# SERVICE API EXAMPLE  

This project is an example to show how to communicate with the service METRO Data Server to retreive data from device (M400 for example).  

Getting datas from service is available only in MetroDataServer version 1.8.0.0 and higher.  

## Introduction  

METRO Data Server is a service able to receive data from MBNet and convert them into various file format (.csv, .xlsx...). However, it can also send these datas by HTTP to an API or any other application that can accept HTTP request.  

## Version 1.8  

### Configuration  

METRO Data Server can be configured to send data to API in the Configuration.xml file located in the service install folder (generally C:\MetroDataServer).  
These lines activate API sending (you can add them if they don't already exist) :

```xml
<ApiMetro>
    <Enabled>True</Enabled> <!-- Activate API --> 
    <HostName>http://localhost:8080</HostName> <!-- Base address to create Url --> 
    <PartChangeUrl>part/change</PartChangeUrl> <!-- Service notify when part change by a POST request on this Url -->
    <NewDataUrl>new/data</NewDataUrl> <!-- Service POST new mesure on this Url -->
</ApiMetro>
```

### Requests  

1. Data sending  

    When you make a mesure on a device, service gets it and make a POST request on the Url corresponding to the `<NewDataUrl>` tag. Mesure is send in Json format with the following model (PostMesureBody.cs contain the class to deserialize it) :  

    ```json
    {
        "PartName":"test_qdas",
        "IpAddress":"192.168.1.105",
        "IpPort":4001,
        "StepStatus":"GO",
        "Date":
        {
            "Year":2024,
            "Month":10,
            "Day":16,
            "Hour":10,
            "Minute":6,
            "Second":26
        },
        "CustomFields":
        [
            {
                "Number":1,
                "Name":"machine",
                "Value":"002"
            }
        ],
        "Characteristics":
        [
            {
                "Name":"Char 1",
                "Value":0.001,
                "Master":0.0,
                "InferiorTolerance":-1.0,
                "Nominal":0.0,
                "SuperiorTolerance":1.0,
                "CalibrationEnabled":false,
                "CalibrationControlEnabled":false
            },
            {
                "Name":"Char 2",
                "Value":0.001,
                "Master":0.0,
                "InferiorTolerance":-1.0,
                "Nominal":0.0,
                "SuperiorTolerance":1.0,
                "CalibrationEnabled":false,
                "CalibrationControlEnabled":false
            }
        ],
        "PresetFrame":false,
        "PresetControlFrame":false
    }
    ```

2. Part changing  

    Service also notify when part configuration could have change (i.e. each time a device goes in mesure mode) and make a POST request on the Url corresponding to the `<PartChangeUrl>` tag. The request body is empty and device IP address is add at Url end for example the request Url `http://localhost:8080/part/change/192.168.1.112` indicates that configuration of the device with IP 192.168.1.112 may have been changed. Knowing that, you could for example connect directly on device IP, port 4001 and use gm4 protcole to get device configuration.  
