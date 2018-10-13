<?php
  class Throws
  {
	  public $conditionName="";
	  public $errMessage="";
	  public static function exception($errMsg="")
	  {
		  $throwsObject=new Throws();
		  $throwsObject->conditionName="exception";
		  $throwsObject->errMessage=$errMsg;
          return $throwsObject;
	  }
	  public static function noException($errMsg="")
	  {
		  $throwsObject=new Throws();
		  $throwsObject->conditionName="noException";
		  $throwsObject->errMessage=$errMsg;
		  
          return $throwsObject;
	  }
	  
	  
  }
  class Refl
  {
	  public static $className;
	  public static $methodName;
	  public static $parameters;
	  public static function inClass($class)
	  {
		  Refl::$className=$class;
		  return new Refl();
	  }
	  public static function withParameters(...$p)
	  {
		  Refl::$parameters=$p;
		  //return $this;
		  return new Refl();
	  }
	  
	  
  }
  class Method
  {
	  private $_className="";
	  private $_methodName="";
	  static $reflection=null;
	  public static function name($methodName)
	  {
		 Method::$reflection=new Refl(); 
		 Method::$reflection::$methodName=$methodName;
		 
		 return Method::$reflection;
	  }
	  
	  
	  
	  
	  
  }
  class Is
  {
    public $conditionName="";
    public $conditionOperandValue="";

	
    public static function equalTo($operandValue)
    {
	  $isObject=new Is();
      $isObject->conditionName="equalTo";
      $isObject->conditionOperandValue=$operandValue;

      return $isObject;
    }
	public static function notEqualTo($operandValue)
    {
	  $isObject=new Is();
      $isObject->conditionName="notEqualTo";
      $isObject->conditionOperandValue=$operandValue;

      return $isObject;
    }
	
	
	public static function True()
    {
	  $isObject=new Is();
      $isObject->conditionName="true";
      

      return $isObject;
    }
	public static function False()
    {
	  $isObject=new Is();
      $isObject->conditionName="false";
      

      return $isObject;
    }
	
	public static function Null()
	{
	  $isObject=new Is();
      $isObject->conditionName="null";
      

      return $isObject;
	}
	
	public static function notNull()
	{
	  $isObject=new Is();
      $isObject->conditionName="notNull";
      

      return $isObject;
	}
	
	public static function same($operandValue)
	{
		
      $isObject=new Is();
      $isObject->conditionName="same";
      $isObject->conditionOperandValue=$operandValue;

      return $isObject;
	}
	
	public static function notSame($operandValue)
	{
		
      $isObject=new Is();
      $isObject->conditionName="notSame";
      $isObject->conditionOperandValue=$operandValue;

      return $isObject;
	}
	
  }
  class Assert
  {
    static $assertMessage="";
    static $testPassed=false;
	static $testName="";
	
	public static function that($operandOne,$condition=null,$message=null,$testName=null)
    {
      
      Assert::$assertMessage=$message;
	  Assert::$testName=$testName;
	  
	  $operandOneValue=$operandOne;
	  	  
	  if(is_object($operandOneValue) && (get_class($operandOneValue)=="Refl" && get_class($condition)=="Is"))
	  {
		  $operandOneValue=Assert::getReturnValue($operandOne);
		  
	  }
	  
      if($condition->conditionName=="equalTo")
      {
        Assert::$testPassed=$operandOneValue==$condition->conditionOperandValue;

      }
	  else if($condition->conditionName=="notEqualTo")
	  {
		  Assert::$testPassed=$operandOneValue!=$condition->conditionOperandValue;
	  }
	  else if($condition->conditionName=="true")
	  {
		  Assert::$testPassed=$operandOneValue;
	  }
	  else if($condition->conditionName=="false")
	  {
		  Assert::$testPassed=!$operandOneValue;
	  }
	  else if($condition->conditionName=="null")
	  {
		  Assert::$testPassed=$operandOneValue==null;
	  }
	  else if($condition->conditionName=="notNull")
	  {
		  Assert::$testPassed=$operandOneValue!=null;
	  }
	  else if($condition->conditionName=="same")
	  {
		  Assert::$testPassed=$operandOneValue===$condition->conditionOperandValue;
	  }
	  else if($condition->conditionName=="notSame")
	  {
		  Assert::$testPassed=$operandOneValue!==$condition->conditionOperandValue;
	  }
	  else if($condition->conditionName=="exception")
	  {
		  try
		  {
			  console.log('called');
			 Assert::getReturnValue($operandOne);
			 
			 
		  }
		  catch(Exception $x)
		  {
			if(strlen($condition->errMessage)>0)
			{
				if($condition->errMessage == $x->getMessage())
				{
					Assert::$testPassed=true;
				}
				else
				{
					Assert::$testPassed=false;
				}
			}
			else
			{
				Assert::$testPassed=true;
			}
		  }
	  }
	  else if($condition->conditionName=="noException")
	  {
		  try
		  {
			  
			  Assert::getReturnValue($operandOne);
			  Assert::$testPassed=true;
			 
		  }
		  catch(Exception $x)
		  {
			if(strlen($condition->errMessage)>0)
			{
				if($condition->errMessage == $x->getMessage())
				{
					Assert::$testPassed=false;
				}
				else
				{
					Assert::$testPassed=true;
				}
			}
			else
			{
				Assert::$testPassed=false;
			}
		  }
	  }
	  
      Assert::showOnScreen();

    }
	
	public static function getReturnValue($operandOne)
	{
		try
		{
			  $reflection=$operandOne;
			  $reflectionMethod = new ReflectionMethod($reflection::$className, $reflection::$methodName);
			  
			  $class = new \ReflectionClass($reflectionMethod->class);
			  $instance = $class->newInstanceArgs();
			  if($reflection::$parameters!=null && count($reflection::$parameters)>0)
			  {
				$parameters=$reflection::$parameters;
				$result=$reflectionMethod->invoke($instance, ...$parameters);
			  }
			  else
			  {
				   $result=$reflectionMethod->invoke($instance,null);
			  }
			  
			  return $result;
		}
		catch(Exception $ex)
		{
			throw $ex;
		}
	}

    private static function showOnScreen()
    {
	  $color="";
      if(Assert::$testPassed)
      {
        echo "<div style=\"background-color:green;color:white;\">".Assert::$testName." -Passed</div>";
      }
      else 
	  {
        echo "<div style=\"background-color:red;color:white;\">".Assert::$testName." -Failed - ".Assert::$assertMessage."</div>";
      }
    }
  }

  
 ?>
