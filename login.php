<?php 
  $error = false;
  if(isset($_POST['login'])){
    $username=preg_replace('/[^A-Za-z]/', '',  $_POST['username']);
    $password=md5($_POST['password']);
    if(file_exists('korisnici/' . $username . '.xml')){
      $xml = new SimpleXMLElement('korisnici/' . $username . '.xml', 0, true);
      if($password == $xml->password){
        session_start();
        $_SESSION['username']=$username;
        header('Location:home.php');
        die;
      }
    }
    $error = true;
  }

?>



<!DOCTYPE html>
<html lang="en">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href="style.css">
  <title>Login</title>
</head>
<body>
<div class="header">
  	<h2>Login</h2>
  </div>
    <form class="formatic" action="" method="post">
    
      <p class="p">Username: <input type="text" name="username" size="20" style="text-align:center;"></p>
      <p class="p">Password: <input type="password" name="password" size="20" style="text-align:center;"></p>
      <?php 
      if($error){
        echo '<p>Invalid username and password</p>';
      }
      ?>
      <p><input type="submit" value="Login" name="login"></p>
	<a href="register.php">Register</a>
    </form>

  </div>

</body>
</html>