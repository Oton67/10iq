<?php 
  $errors=array();
  if(isset($_POST['login'])){
    $username=preg_replace('/[^A-Za-z]/', '',  $_POST['username']);
    $email=$_POST['email'];
    $password=$_POST['password'];
    $c_password=$_POST['c_password'];

    if(file_exists('korisnici/' . $username . '.xml')){
      $errors[]= 'Username already exists';
    }

    if($username==''){
      $errors[]='Username is blank';
      
    }

    if($email==''){
      $errors[]='Email is blank';
    }
    
    if($password == '' || $c_password == ''){
      $errors[]='Passwords are blank';
    }

    if($password!=$c_password){
      $errors[]='Passwords do not match';
    }

    if(count($errors)==0){
      $xml = new SimpleXMLElement('<user></user>');
      $xml->addChild('password', md5($password));
      $xml->addChild('email', $email);
      $xml->asXML('korisnici/' . $username . '.xml');
      header('Location: login.php');
      die;
    }
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
  <title>Register</title>
</head>
<body>
  <div class="header">
  	<h2>Register</h2>
  </div>
    <form action="" method="post">
      <?php 
      if(count($errors)>0){
        echo '<ul>';
        foreach($errors as $e){
          echo '<li>' . $e . '</li>';
        }
      }
      ?>
      <p class="p">Username <input type="text" name="username" size="20" style="text-align:center;"></p>
      <p class="p">Email <input type="text" name="email" size="20" style="text-align:center;"></p>
      <p class="p">Password <input type="password" name="password" size="20" style="text-align:center;"></p>
      <p class="p">Confirm Password <input type="password" name="c_password" size="20" style="text-align:center;"></p>
      <p><input type="submit" name="login"></p>
    </div>
  
  </form>

</body>
</html>