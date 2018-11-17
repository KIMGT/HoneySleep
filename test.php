<?php
	$con = mysqli_connect("localhost","csy7792","nel4756!","csy7792");
	
    //$userID = "$_POST["userID"]";
    $userID = "test";
    
    $check_query = "SELECT substring(time_,1,7) as date_,count(*) as su  FROM pillow_data WHERE userID='".$userID."' group by date_";
    
    $result = mysqli_query($con,$check_query);
    $response = array();
    
    while($row = mysqli_fetch_array($result))
    {
        array_push($response,array("date_"=>$row[0],"su"=>$row[1]));

    }
    
    echo json_encode($response);
    mysqli_close($con)
?>