<?php
	$con = mysqli_connect("localhost","csy7792","nel4756!","csy7792");
	
	$userID = $_POST["userID"];

	$statement = mysqli_prepare($con,"insert into pillow_data(userid) values(?)");
	mysqli_stmt_bind_param($statement,"s",$userID);
	mysqli_stmt_execute($statement);
	
	$response = array();
	$response["success"] = true;
	
	echo json_encode($response);
?>