<?php 
  session_start(); 

  if (!isset($_SESSION['username'])) {
  	$_SESSION['msg'] = "You must login first";
  	header('location: login.php');
  }
  if (isset($_GET['logout'])) {
  	session_destroy();
  	unset($_SESSION['username']);
  	header("Location:login.php");
  }
?>
<html xmlns="http://www.w3.org/1999/xhtml"><head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Pocetna</title>
<link href="default.css" rel="stylesheet" type="text/css" media="all">
</head>
<body>
<div id="wrapper">

	<div id="page" class="container">
		<div id="content">
		<div class="content">
  	<?php if (isset($_SESSION['success'])) : ?>
      <div class="error success" >
      	<h3>
          <?php 
          	echo $_SESSION['success']; 
          	unset($_SESSION['success']);
          ?>
      	</h3>
      </div>
  	<?php endif ?>

    
    <?php  if (isset($_SESSION['username'])) : ?>
    	<h1>Welcome <strong><?php echo $_SESSION['username']; ?></strong></h1>
		<img src="png.png" title="Welcome">
    	<p> <a href="home.php?logout='1'" style="color: blue;">logout</a> </p>
    <?php endif ?>

</body></html>