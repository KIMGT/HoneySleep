<?php
	$con = mysqli_connect("localhost","csy7792","nel4756!","csy7792");
	
    $userID = $_POST["userID"];
    $method = $_POST["method"];
    $yearTime = $_POST["yearTime"]; 
    $monthTime= $_POST["monthTime"]; 
    $yearTime = "2018"; 
    $monthTime= "11"; 
    switch($method)
    {
        case 'month':
            $check_query = "SELECT substring(time_,1,7) as datatype,count(*) as data_  FROM pillow_data WHERE userID='".$userID."' group by datatype";
        
            $result = mysqli_query($con,$check_query);
            $response = array();
        
            while($row = mysqli_fetch_array($result))
            {
                array_push($response,array("datatype"=>$row[0],"data_"=>$row[1]));
            }
        break;
        case 'day':
            $check_query = "SELECT substring(time_,1,10) as datatype,count(*) as data_ FROM pillow_data 
            WHERE substring(time_,1,4) ='".$yearTime."' AND substring(time_,6,2) ='".$monthTime."' AND userID='".$userID."' group by datatype";
            $result = mysqli_query($con,$check_query);
            $response = array();
    
            while($row = mysqli_fetch_array($result))
            {
                array_push($response,array("datatype"=>$row[0],"data_"=>$row[1]));

            }
    
        break;

        default:
        break;
    }

    echo json_encode(array("response"=>$response));
    mysqli_close($con)
?>